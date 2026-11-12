import { postingApi } from './api.js';
import {
  deliveryTitle, formatNumber, formatMoney, formatDate,
  showToast, confirmDialog, initMenu,
} from './ui.js';

initMenu();

const rowsEl = document.getElementById('rows');
const cardsEl = document.getElementById('cards');
const infoEl = document.getElementById('info');
const btnEdit = document.getElementById('btnEdit');
const btnDetails = document.getElementById('btnDetails');
const btnDelete = document.getElementById('btnDelete');
const btnRefresh = document.getElementById('btnRefresh');

let postings = [];
let selectedId = null;

// ----- Повідомлення, залишене попередньою сторінкою (після збереження) -----
try {
  const flash = sessionStorage.getItem('flash');
  if (flash) {
    sessionStorage.removeItem('flash');
    const { message, type } = JSON.parse(flash);
    showToast(message, type);
  }
} catch { /* без повідомлення */ }

// ----- Малювання -----
function makeCell(text) {
  const div = document.createElement('div');
  div.textContent = text;
  return div;
}

function select(id) {
  selectedId = id;
  render();
}

function showMessage(text, className) {
  rowsEl.innerHTML = '';
  cardsEl.innerHTML = '';
  for (const parent of [rowsEl, cardsEl]) {
    const div = document.createElement('div');
    div.className = className;
    div.textContent = text;
    parent.appendChild(div);
  }
}

function render() {
  rowsEl.innerHTML = '';
  cardsEl.innerHTML = '';

  if (postings.length === 0) {
    showMessage('Відправлень поки немає. Натисніть «Створити», щоб додати перше.', 'empty');
  }

  for (const p of postings) {
    const isSelected = p.id === selectedId;

    // рядок таблиці
    const row = document.createElement('div');
    row.className = 'row' + (isSelected ? ' selected' : '');
    row.setAttribute('role', 'row');
    row.tabIndex = 0;
    row.append(
      makeCell(p.id),
      makeCell(p.from),
      makeCell(p.to),
      makeCell(p.content),
      makeCell(deliveryTitle(p.deliveryType)),
      makeCell(formatNumber(p.weight)),
      makeCell(formatMoney(p.price)),
      makeCell(formatDate(p.createdAt)),
    );
    row.addEventListener('click', () => select(p.id));
    row.addEventListener('dblclick', () => { location.href = `posting-edit.html?id=${p.id}`; });
    row.addEventListener('keydown', (e) => {
      if (e.key === 'Enter' || e.key === ' ') { e.preventDefault(); select(p.id); }
    });
    rowsEl.appendChild(row);

    // картка (вузький екран)
    const card = document.createElement('div');
    card.className = 'card' + (isSelected ? ' selected' : '');
    card.tabIndex = 0;
    const t = document.createElement('div');
    t.className = 't';
    t.textContent = `№ ${p.id} · ${p.from} → ${p.to}`;
    const s = document.createElement('div');
    s.className = 's';
    s.textContent = `${p.content} · ${deliveryTitle(p.deliveryType)} · ${formatNumber(p.weight)} кг · ${formatMoney(p.price)} грн`;
    const chosen = document.createElement('div');
    chosen.className = 'chosen';
    chosen.textContent = 'Вибрано';
    card.append(t, s, chosen);
    card.addEventListener('click', () => select(p.id));
    card.addEventListener('keydown', (e) => {
      if (e.key === 'Enter' || e.key === ' ') { e.preventDefault(); select(p.id); }
    });
    cardsEl.appendChild(card);
  }

  const hasSelection = selectedId !== null;
  btnEdit.disabled = !hasSelection;
  btnDetails.disabled = !hasSelection;
  btnDelete.disabled = !hasSelection;
  infoEl.textContent =
    `Усього: ${postings.length}` + (hasSelection ? ` · Вибрано: № ${selectedId}` : '');
}

// ----- Завантаження списку -----
async function load() {
  btnRefresh.disabled = true;
  showMessage('Завантаження…', 'loading');
  try {
    postings = await postingApi.getAll();
    if (!postings.some((p) => p.id === selectedId)) selectedId = null;
    render();
  } catch (err) {
    postings = [];
    selectedId = null;
    render();
    showMessage('Не вдалося завантажити список. ' + err.message, 'empty');
    showToast(err.message, 'error');
  } finally {
    btnRefresh.disabled = false;
  }
}

// ----- Дії -----
btnEdit.addEventListener('click', () => {
  if (selectedId !== null) location.href = `posting-edit.html?id=${selectedId}`;
});

btnDetails.addEventListener('click', () => {
  if (selectedId !== null) location.href = `posting-edit.html?id=${selectedId}&view=1`;
});

btnDelete.addEventListener('click', async () => {
  if (selectedId === null) return;
  const id = selectedId;
  const ok = await confirmDialog(`Видалити відправлення № ${id}?`, 'Цю дію неможливо скасувати.');
  if (!ok) return;
  try {
    await postingApi.remove(id);
    selectedId = null;
    showToast(`Відправлення № ${id} видалено.`, 'success');
  } catch (err) {
    showToast(err.message, 'error');
  }
  await load();
});

btnRefresh.addEventListener('click', load);

load();