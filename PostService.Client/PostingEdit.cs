using System.Globalization;
using PostService.CommonTypes;
using PostService.Dtos;

namespace PostService.Client;

internal static class DeliveryTitles
{
    public static string Get(DeliveryType type) => type.ToString() switch
    {
        "Department" => "Відділення",
        "Courier" => "Кур'єр",
        var other => other
    };
}

internal sealed record DeliveryItem(DeliveryType Value, string Title);

public partial class PostingEdit : Form
{
    private readonly PostingApiClient _api;
    private readonly PostingGetDto? _editing;
    private readonly ErrorProvider _errors = new() { BlinkStyle = ErrorBlinkStyle.NeverBlink };

    public PostingEdit(PostingApiClient api, PostingGetDto? editing = null)
    {
        InitializeComponent();

        _api = api;
        _editing = editing;

        var items = Enum.GetValues<DeliveryType>()
            .Select(t => new DeliveryItem(t, DeliveryTitles.Get(t)))
            .ToList();
        cmbDeliveryType.DisplayMember = nameof(DeliveryItem.Title);
        cmbDeliveryType.ValueMember = nameof(DeliveryItem.Value);
        cmbDeliveryType.DataSource = items;

        if (editing is null)
        {
            Text = "Нове відправлення";
            lblTitle.Text = "Нове відправлення";
            txtPrice.Text = "розраховується автоматично";
        }
        else
        {
            Text = $"Редагування відправлення № {editing.Id}";
            lblTitle.Text = $"Редагування відправлення № {editing.Id}";
            txtFrom.Text = editing.From;
            txtTo.Text = editing.To;
            txtContent.Text = editing.Content;
            cmbDeliveryType.SelectedValue = editing.DeliveryType;
            txtWeight.Text = FormatNumber(editing.Weight);
            txtWidth.Text = FormatNumber(editing.Width);
            txtHeight.Text = FormatNumber(editing.Height);
            txtDepth.Text = FormatNumber(editing.Depth);
            txtValue.Text = editing.Value is null ? string.Empty : FormatNumber(editing.Value.Value);
            txtPrice.Text = editing.Price.ToString("0.00", CultureInfo.CurrentCulture);
        }

        txtFrom.TextChanged += (_, _) => SetError(txtFrom, lblFromError, null);
        txtTo.TextChanged += (_, _) => SetError(txtTo, lblToError, null);
        txtContent.TextChanged += (_, _) => SetError(txtContent, lblContentError, null);
        txtWeight.TextChanged += (_, _) => SetError(txtWeight, lblWeightError, null);
        txtWidth.TextChanged += (_, _) => SetError(txtWidth, lblWidthError, null);
        txtHeight.TextChanged += (_, _) => SetError(txtHeight, lblHeightError, null);
        txtDepth.TextChanged += (_, _) => SetError(txtDepth, lblDepthError, null);
        txtValue.TextChanged += (_, _) => SetError(txtValue, lblValueError, null);
    }

    private static string FormatNumber(float value) =>
        value.ToString("0.##", CultureInfo.CurrentCulture);

    private static bool TryParseNumber(string text, out float result) =>
        float.TryParse(text.Trim().Replace(',', '.'), NumberStyles.Float,
            CultureInfo.InvariantCulture, out result) && float.IsFinite(result);

    private void SetError(Control control, Label label, string? message)
    {
        _errors.SetError(control, message ?? string.Empty);
        label.Text = message ?? string.Empty;
    }

    private bool CheckText(TextBox box, Label label, string message)
    {
        if (string.IsNullOrWhiteSpace(box.Text))
        {
            SetError(box, label, message);
            return false;
        }

        SetError(box, label, null);
        return true;
    }

    private bool ReadPositive(TextBox box, Label label, string message, out float result)
    {
        if (!TryParseNumber(box.Text, out result) || result <= 0)
        {
            SetError(box, label, message);
            return false;
        }

        SetError(box, label, null);
        return true;
    }

    private bool ReadValue(out float result)
    {
        result = 0;
        if (string.IsNullOrWhiteSpace(txtValue.Text))
        {
            SetError(txtValue, lblValueError, null);
            return true;
        }

        if (!TryParseNumber(txtValue.Text, out result) || result < 0)
        {
            SetError(txtValue, lblValueError, "Введіть число, наприклад 100, або залиште поле порожнім");
            return false;
        }

        SetError(txtValue, lblValueError, null);
        return true;
    }

    private bool ValidateInput(out float weight, out float width, out float height,
        out float depth, out float value)
    {
        Control? firstInvalid = null;

        void Check(bool valid, Control control)
        {
            if (!valid && firstInvalid is null)
            {
                firstInvalid = control;
            }
        }

        Check(CheckText(txtFrom, lblFromError, "Вкажіть відправника: ім'я та прізвище"), txtFrom);
        Check(CheckText(txtTo, lblToError, "Вкажіть отримувача: ім'я та прізвище"), txtTo);
        Check(CheckText(txtContent, lblContentError, "Опишіть вміст, наприклад: Документи"), txtContent);
        Check(ReadPositive(txtWeight, lblWeightError, "Вага має бути більшою за 0 кг, наприклад 0,5", out weight), txtWeight);
        Check(ReadPositive(txtWidth, lblWidthError, "Введіть ширину в см, більшу за 0", out width), txtWidth);
        Check(ReadPositive(txtHeight, lblHeightError, "Введіть висоту в см, більшу за 0", out height), txtHeight);
        Check(ReadPositive(txtDepth, lblDepthError, "Введіть глибину в см, більшу за 0", out depth), txtDepth);
        Check(ReadValue(out value), txtValue);

        firstInvalid?.Focus();
        return firstInvalid is null;
    }

    private async void btnSave_Click(object? sender, EventArgs e)
    {
        if (!ValidateInput(out var weight, out var width, out var height, out var depth, out var value))
        {
            return;
        }

        var deliveryType = (DeliveryType)cmbDeliveryType.SelectedValue!;
        var from = txtFrom.Text.Trim();
        var to = txtTo.Text.Trim();
        var content = txtContent.Text.Trim();

        btnSave.Enabled = false;
        try
        {
            if (_editing is null)
            {
                await _api.CreateAsync(new PostingPostDto
                {
                    From = from,
                    To = to,
                    Content = content,
                    DeliveryType = deliveryType,
                    Weight = weight,
                    Width = width,
                    Height = height,
                    Depth = depth,
                    Value = value
                });
            }
            else
            {
                await _api.UpdateAsync(new PostingPutDto
                {
                    Id = _editing.Id,
                    From = from,
                    To = to,
                    Content = content,
                    DeliveryType = deliveryType,
                    Weight = weight,
                    Width = width,
                    Height = height,
                    Depth = depth,
                    Value = value,
                    Price = _editing.Price
                });
            }

            DialogResult = DialogResult.OK;
        }
        catch (ApiException ex)
        {
            MessageBox.Show(this, $"Не вдалося зберегти відправлення. {ex.Message}",
                "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (HttpRequestException)
        {
            MessageBox.Show(this, "Немає зв'язку із сервером. Перевірте, чи запущено PostService.",
                "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            btnSave.Enabled = true;
        }
    }
}