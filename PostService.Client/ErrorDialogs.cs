namespace PostService.Client;

internal static class ErrorDialogs
{
    // true — користувач вирішив повторити операцію
    public static bool AskRetry(IWin32Window owner)
    {
        var retryButton = new TaskDialogButton("Повторити");
        var cancelButton = new TaskDialogButton("Скасувати");
        var page = new TaskDialogPage
        {
            Caption = "Немає зв'язку із сервером",
            Heading = "Сервер недоступний",
            Text = "Перевірте, чи запущено PostService, і повторіть спробу.",
            Icon = TaskDialogIcon.Warning,
            Buttons = { retryButton, cancelButton },
            DefaultButton = retryButton
        };

        return TaskDialog.ShowDialog(owner, page) == retryButton;
    }
}