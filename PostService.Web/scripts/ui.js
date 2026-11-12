// ===== Допоміжні функції інтерфейсу =====

// ----- Дані для показу -----
const DELIVERY_TITLES = {
  Department: 'Відділення',
  Courier: 'Кур\'єр',
};

export function deliveryTitle(type) {
  return DELIVERY_TITLES[type] || String(type);
}

export function formatNumber(value) {
  if (value === null || value === undefined) return '';
  return Number(value).toLocaleString('uk-UA', { maximumFractionDigits: 3 });
}

export function formatMoney(value) {
  if (value === null || value === undefined) return '';
  return Number(value).toLocaleString('uk-UA', {
    minimumFractionDigits: 2,
    maximumFractionDigits: 2,
  });
}

export function formatDate(iso) {
  if (!iso) return '';
  const d = new Date(iso);
  if (isNaN(d)) return '';
  const p = (n) => String(n).padStart(2, '0');
  return `${p(d.getDate())}.${p(d.getMonth() + 1)}.${d.getFullYear()} ${p(d.getHours())}:${p(d.getMinutes())}`;
}

// ----- Спливаючі повідомлення -----
function getToastContainer() {
  let box = document.getElementById('toasts');
  if (!box) {
    box = document.createElement('div');
    box.id = 'toasts';
    box.className = 'toasts';
    box.setAttribute('aria-live', 'polite');
    document.body.appendChild(box);
  }
  return box;
}

export function showToast(message, type = 'success') {
  const toast = document.createElement('div');
  toast.className = `toast ${type}`;
  toast.setAttribute('role', type === 'error' ? 'alert' : 'status');
  toast.textContent = message;
  getToastContainer().appendChild(toast);
  setTimeout(() => toast.remove(), type === 'error' ? 8000 : 4000);
}

// ----- Вікно підтвердження -----
// Повертає Promise: true, якщо користувач підтвердив, false — якщо скасував.
export function confirmDialog(title, text, confirmLabel = 'Видалити') {
  return new Promise((resolve) => {
    const overlay = document.createElement('div');
    overlay.className = 'overlay show';

    const dialog = document.createElement('div');
    dialog.className = 'dialog';
    dialog.setAttribute('role', 'dialog');
    dialog.setAttribute('aria-modal', 'true');

    const h = document.createElement('h2');
    h.textContent = title;
    const p = document.createElement('p');
    p.textContent = text;

    const actions = document.createElement('div');
    actions.className = 'actions';
    const cancel = document.createElement('button');
    cancel.type = 'button';
    cancel.className = 'btn';
    cancel.textContent = 'Скасувати';
    const ok = document.createElement('button');
    ok.type = 'button';
    ok.className = 'btn btn-danger-solid';
    ok.textContent = confirmLabel;
    actions.append(cancel, ok);

    dialog.append(h, p, actions);
    overlay.appendChild(dialog);
    document.body.appendChild(overlay);
    cancel.focus();

    function close(result) {
      document.removeEventListener('keydown', onKey);
      overlay.remove();
      resolve(result);
    }
    function onKey(e) {
      if (e.key === 'Escape') close(false);
    }

    document.addEventListener('keydown', onKey);
    cancel.addEventListener('click', () => close(false));
    ok.addEventListener('click', () => close(true));
    overlay.addEventListener('click', (e) => {
      if (e.target === overlay) close(false);
    });
  });
}

// ----- Меню на вузькому екрані -----
export function initMenu() {
  const btn = document.getElementById('menuBtn');
  const nav = document.getElementById('nav');
  if (!btn || !nav) return;
  btn.addEventListener('click', () => {
    const open = nav.classList.toggle('open');
    btn.setAttribute('aria-expanded', String(open));
  });
}