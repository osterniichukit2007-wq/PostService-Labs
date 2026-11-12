namespace PostService.Client;

// Формат відповіді сервера на помилки валідації (лише потрібна клієнту частина)
internal class ValidationErrorResponse
{
    public Dictionary<string, string[]>? Errors { get; set; }
}

// Виняток, що переносить помилки валідації за полями
public class PostingValidationException : Exception
{
    public Dictionary<string, string[]> Errors { get; }

    public PostingValidationException(Dictionary<string, string[]> errors)
        : base("Сервер відхилив дані відправлення.")
    {
        Errors = errors;
    }
}