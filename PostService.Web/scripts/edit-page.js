import { postingApi } from './api.js';
import { formatMoney, initMenu } from './ui.js';

initMenu();

// ----- Режим сторінки -----
const params = new URLSearchParams(location.search);
const id = params.get('id') ? Number(params.get('id')) : null;
const viewOnly = params.get('view') === '1';

const form = document.getElementById('form');
const titleEl = document.getElementById('title');
const alertEl = document.getElementById('alert');
const alertText = document.getElementById('alertText');
const btnSave = document.getElementById('btnSave');
const btnCancel = document.getElementById('btnCancel');
const deliveryType = document.getElementById('deliveryType');
const priceEl = document.getElementById('price');

let current = null;      // завантажене відправлення (для редагування)
let submitted = false;   // після першої спроби збереження перевіряємо поля «наживо»

// ----- Правила перевірки -----
function parseNumber(raw) {
  const t = raw.trim().replace(',', '.');
  if (!/^-?\d+(\.\d+)?$/.test(t)) return NaN;
  return Number(t);
}

function textRule(message) {
  return (raw) => (raw.trim() ? '' : message);
}

// Число, строго більше за 0
function positiveRule({ ask, name, unit, example }) {
  return (raw) => {
    if (!raw.trim()) return `Вкажіть ${ask}, наприклад ${example}`;
    const n = parseNumber(raw);
    if (Number.isNaN(n)) return `Введіть число, наприклад ${example}`;
    if (n <= 0) return `${name} має бути більшою за 0 ${unit}, наприклад ${example}`;
    return '';
  };
}

function valueRule(raw) {
  if (!raw.trim()) return '';                       // поле необов'язкове
  const n = parseNumber(raw);
  if (Number.isNaN(n)) return 'Введіть число, наприклад 100, або залиште поле порожнім';
  if (n < 0) return 'Оголошена цінність не може бути від\'ємною';
  return '';
}

const rules = {
  from: textRule('Вкажіть відправника: ім\'я та прізвище'),
  to: textRule('Вкажіть отримувача: ім\'я та прізвище'),
  content: textRule('Вкажіть, що саме відправляєте, наприклад: Документи'),
  weight: positiveRule({ ask: 'вагу в кілограмах', name: 'Вага', unit: 'кг', example: '0,5' }),
  width: positiveRule({ ask: 'ширину в сантиметрах', name: 'Ширина', unit: 'см', example: '20' }),
  height: positiveRule({ ask: 'висоту в сантиметрах', name: 'Висота', unit: 'см', example: '15' }),
  depth: positiveRule({ ask: 'глибину в сантиметрах', name: 'Глибина', unit: 'см', example: '2' }),
  value: valueRule,
};
const keys = Object.keys(rules);

// ----- Показ помилок -----
function validateField(key) {
  const input = document.getElementById(key);
  const msgEl = document.getElementById(`${key}-msg`);
  const message = rules[key](input.value);

  input.setCustomValidity(message);            // Constraint Validation API
  input.setAttribute('aria-invalid', message ? 'true' : 'false');
  msgEl.textContent = message;
  msgEl.classList.toggle('show', Boolean(message));
  return !message;
}

function fieldsWord(n) {
  if (n % 10 === 1 && n % 100 !== 11) return 'поле';
  if ([2, 3, 4].includes(n % 10) && ![12, 13, 14].includes(n % 100)) return 'поля';
  return 'полів';
}

function showAlert(text) {
  alertText.textContent = text;
  alertEl.classList.add('show');
}

function hideAlert() {
  alertEl.classList.remove('show');
}

function validateAll() {
  let bad = 0;
  let firstBad = null;
  for (const key of keys) {
    if (!validateField(key)) {
      bad++;
      if (!firstBad) firstBad = document.getElementById(key);
    }
  }
  if (bad > 0) {
    showAlert(`Виправте ${bad} ${fieldsWord(bad)}, позначені червоним.`);
    firstBad.focus();
    return false;
  }
  hideAlert();
  return true;
}

function revalidateLive() {
  let bad = 0;
  for (const key of keys) if (!validateField(key)) bad++;
  if (bad > 0) showAlert(`Виправте ${bad} ${fieldsWord(bad)}, позначені червоним.`);
  else hideAlert();
}

for (const key of keys) {
  document.getElementById(key).addEventListener('input', () => {
    if (submitted) revalidateLive();
  });
}

// ----- Заповнення і зчитування форми -----
function toInput(n) {
  return n === null || n === undefined ? '' : String(n).replace('.', ',');
}

function fillForm(p) {
  document.getElementById('from').value = p.from ?? '';
  document.getElementById('to').value = p.to ?? '';
  document.getElementById('content').value = p.content ?? '';
  deliveryType.value = p.deliveryType;
  document.getElementById('weight').value = toInput(p.weight);
  document.getElementById('width').value = toInput(p.width);
  document.getElementById('height').value = toInput(p.height);
  document.getElementById('depth').value = toInput(p.depth);
  document.getElementById('value').value = toInput(p.value);
  priceEl.value = p.price === null || p.price === undefined ? '' : formatMoney(p.price);
}

function readDto() {
  const num = (key) => parseNumber(document.getElementById(key).value);
  return {
    from: document.getElementById('from').value.trim(),
    to: document.getElementById('to').value.trim(),
    content: document.getElementById('content').value.trim(),
    deliveryType: deliveryType.value,
    weight: num('weight'),
    width: num('width'),
    height: num('height'),
    depth: num('depth'),
    value: document.getElementById('value').value.trim() ? num('value') : 0,
  };
}

function setFormEnabled(enabled) {
  for (const el of form.querySelectorAll('input, select, button')) {
    if (el === priceEl) continue;
    el.disabled = !enabled;
  }
}

function setReadOnly() {
  for (const el of form.querySelectorAll('input, select')) el.disabled = true;
  btnSave.style.display = 'none';
  btnCancel.textContent = 'Назад';
  form.querySelector('.req-note').style.display = 'none';
}

// ----- Збереження -----
form.addEventListener('submit', async (e) => {
  e.preventDefault();
  if (viewOnly) return;

  submitted = true;
  if (!validateAll()) return;

  const dto = readDto();
  btnSave.disabled = true;
  try {
    let message;
    if (id === null) {
      const created = await postingApi.create(dto);
      message = `Відправлення № ${created.id} створено.`;
    } else {
      await postingApi.update(id, { ...dto, id, price: current.price });
      message = `Зміни у відправленні № ${id} збережено.`;
    }
    try {
      sessionStorage.setItem('flash', JSON.stringify({ message, type: 'success' }));
    } catch { /* без повідомлення на наступній сторінці */ }
    location.href = 'posting-list.html';
  } catch (err) {
    showAlert(err.message);
    btnSave.disabled = false;
  }
});

// ----- Початок роботи сторінки -----
async function init() {
  if (id === null) {
    titleEl.textContent = 'Нове відправлення';
    return;
  }

  titleEl.textContent = viewOnly ? `Відправлення № ${id}` : `Редагування відправлення № ${id}`;
  setFormEnabled(false);
  try {
    current = await postingApi.get(id);
    fillForm(current);
    if (viewOnly) setReadOnly();
    else setFormEnabled(true);
  } catch (err) {
    showAlert(err.message);
    btnSave.style.display = 'none';
    btnCancel.textContent = 'Назад';
    btnCancel.style.pointerEvents = 'auto';
  }
}

init();