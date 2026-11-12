// ===== Робота з REST API PostService =====
// Єдине місце, де сторінки звертаються до сервера.

const API_URL = 'http://localhost:5077';

// Помилка запиту з повідомленням, готовим для показу користувачу
export class ApiError extends Error {
  constructor(status, message) {
    super(message);
    this.name = 'ApiError';
    this.status = status;
  }
}

// Перетворює відповідь сервера з помилкою на читабельний текст
async function buildErrorMessage(response, actionTitle) {
  let details = '';
  try {
    const text = await response.text();
    if (text) {
      try {
        const json = JSON.parse(text);
        if (json.errors) {
          // Стандартна відповідь ASP.NET про помилки валідації
          details = Object.values(json.errors).flat().join(' ');
        } else {
          details = json.detail || json.title || json.message || '';
        }
      } catch {
        details = text.length < 200 ? text : '';
      }
    }
  } catch {
    // тіло відповіді прочитати не вдалося — залишаємо порожнім
  }

  switch (response.status) {
    case 400:
      return `Сервер відхилив дані. ${details || 'Перевірте значення полів.'}`;
    case 404:
      return 'Відправлення не знайдено. Можливо, його вже видалили. Оновіть список.';
    case 500:
      return `Помилка на сервері: не вдалося ${actionTitle}.${details ? ' ' + details : ''}`;
    default:
      return `Не вдалося ${actionTitle} (код ${response.status}).${details ? ' ' + details : ''}`;
  }
}

async function request(path, options, actionTitle) {
  let response;
  try {
    response = await fetch(API_URL + path, options);
  } catch {
    throw new ApiError(
      0,
      'Немає зв\'язку з сервером. Перевірте, що API запущено (dotnet run у папці PostService.App).'
    );
  }

  if (!response.ok) {
    throw new ApiError(response.status, await buildErrorMessage(response, actionTitle));
  }

  if (response.status === 204) return null;
  return await response.json();
}

function jsonOptions(method, body) {
  return {
    method,
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(body),
  };
}

export const postingApi = {
  getAll() {
    return request('/postings', { method: 'GET' }, 'завантажити список відправлень');
  },

  get(id) {
    return request(`/postings/${id}`, { method: 'GET' }, 'завантажити відправлення');
  },

  create(dto) {
    return request('/postings', jsonOptions('POST', dto), 'створити відправлення');
  },

  update(id, dto) {
    return request(`/postings/${id}`, jsonOptions('PUT', dto), 'зберегти зміни');
  },

  remove(id) {
    return request(`/postings/${id}`, { method: 'DELETE' }, 'видалити відправлення');
  },
};