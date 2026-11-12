using System.Diagnostics;
using System.Reflection;

namespace PostService.Client
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Без цього виняток з потоку інтерфейсу (наприклад, з async void обробника кнопки)
            // показує стандартне вікно Windows Forms і не доходить до нашого обробника.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionEventHandler;

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new PostingList());
        }

        private static void UnhandledExceptionEventHandler(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;

            // Винятки з асинхронних обробників можуть надходити загорнутими
            if (ex is TargetInvocationException && ex.InnerException is not null)
            {
                ex = ex.InnerException;
            }

            // Подробиці для розробника: час, тип, повідомлення, стек викликів
            Debug.WriteLine($"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}] Необроблений виняток");
            Debug.WriteLine($"Тип: {ex?.GetType().FullName ?? e.ExceptionObject?.GetType().FullName}");
            Debug.WriteLine($"Повідомлення: {ex?.Message ?? e.ExceptionObject?.ToString()}");
            Debug.WriteLine($"Стек викликів:{Environment.NewLine}{ex?.StackTrace}");
            if (ex?.InnerException is not null)
            {
                Debug.WriteLine($"Внутрішній виняток: {ex.InnerException}");
            }

            // Користувачу — лише зрозуміле повідомлення без технічних деталей
            MessageBox.Show(
                "Сталась непередбачена помилка, застосунок буде закрито. Спробуйте запустити його ще раз.",
                "Помилка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}