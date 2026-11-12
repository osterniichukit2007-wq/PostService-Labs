using System.Net;
using PostService.CommonTypes;
using PostService.Dtos;

namespace PostService.Client
{
    public partial class PostingList : Form
    {
        private readonly IPostServiceClient _postService = new PostServiceClient();

        public PostingList()
        {
            InitializeComponent();

            gridPostings.AutoGenerateColumns = false;
            gridPostings.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 99, 177);
            gridPostings.DefaultCellStyle.SelectionForeColor = Color.White;

            colWeight.DefaultCellStyle.Format = "0.##";
            colWidth.DefaultCellStyle.Format = "0.##";
            colHeight.DefaultCellStyle.Format = "0.##";
            colDepth.DefaultCellStyle.Format = "0.##";
            colValue.DefaultCellStyle.Format = "0.##";
            colPrice.DefaultCellStyle.Format = "0.00";
            colCreatedAt.DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";

            gridPostings.CellFormatting += gridPostings_CellFormatting;
            gridPostings.CellDoubleClick += gridPostings_CellDoubleClick;
        }

        private void gridPostings_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == colDeliveryType.Index && e.Value is DeliveryType type)
            {
                e.Value = DeliveryTitles.Get(type);
                e.FormattingApplied = true;
            }
        }

        private void gridPostings_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnEdit.PerformClick();
            }
        }

        private async void PostingList_Load(object? sender, EventArgs e)
        {
            await LoadPostingsAsync();
        }

        private async Task LoadPostingsAsync(int? selectId = null)
        {
            while (true)
            {
                try
                {
                    var postings = await _postService.GetPostingsAsync();

                    gridPostings.DataSource = postings;
                    lblCount.Text = $"Усього відправлень: {postings.Length}";

                    if (selectId is not null)
                    {
                        SelectRow(selectId.Value);
                    }

                    return;
                }
                catch (HttpRequestException ex) when (ex.StatusCode == null)
                {
                    // Сервер недоступний: пропонуємо повторити. Інші помилки не перехоплюємо —
                    // їх оброблятиме глобальний обробник.
                    if (!ErrorDialogs.AskRetry(this))
                    {
                        return;
                    }
                }
            }
        }

        private void SelectRow(int id)
        {
            foreach (DataGridViewRow row in gridPostings.Rows)
            {
                if (row.DataBoundItem is PostingGetDto posting && posting.Id == id)
                {
                    gridPostings.CurrentCell = row.Cells[colFrom.Index];
                    row.Selected = true;
                    return;
                }
            }
        }

        private PostingGetDto? GetSelected(string actionText)
        {
            if (gridPostings.CurrentRow?.DataBoundItem is PostingGetDto selected)
            {
                return selected;
            }

            MessageBox.Show(this, $"Виберіть відправлення, яке потрібно {actionText}.",
                "Нічого не вибрано", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return null;
        }

        private async void btnCreate_Click(object? sender, EventArgs e)
        {
            using var dialog = new PostingEdit(_postService);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                await LoadPostingsAsync();
            }
        }

        private async void btnEdit_Click(object? sender, EventArgs e)
        {
            var selected = GetSelected("редагувати");
            if (selected is null)
            {
                return;
            }

            using var dialog = new PostingEdit(_postService, selected);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                await LoadPostingsAsync(selected.Id);
            }
        }

        private async void btnDelete_Click(object? sender, EventArgs e)
        {
            var selected = GetSelected("видалити");
            if (selected is null)
            {
                return;
            }

            var deleteButton = new TaskDialogButton("Видалити");
            var cancelButton = new TaskDialogButton("Скасувати");
            var page = new TaskDialogPage
            {
                Caption = "Підтвердження видалення",
                Heading = $"Видалити відправлення № {selected.Id}?",
                Text = $"{selected.From} → {selected.To}, «{selected.Content}». Цю дію неможливо скасувати.",
                Icon = TaskDialogIcon.Warning,
                Buttons = { deleteButton, cancelButton },
                DefaultButton = cancelButton
            };

            if (TaskDialog.ShowDialog(this, page) != deleteButton)
            {
                return;
            }

            while (true)
            {
                try
                {
                    await _postService.DeletePostingAsync(selected.Id);
                    break;
                }
                catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    // Запис уже видалено (наприклад, іншим користувачем): повідомляємо й оновлюємо список
                    MessageBox.Show(this, "Вибране відправлення вже відсутнє. Список буде оновлено.",
                        "Відправлення не знайдено", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
                catch (HttpRequestException ex) when (ex.StatusCode == null)
                {
                    if (!ErrorDialogs.AskRetry(this))
                    {
                        return;
                    }
                }
            }

            await LoadPostingsAsync();
        }

        private async void btnRefresh_Click(object? sender, EventArgs e)
        {
            await LoadPostingsAsync();
        }
    }
}