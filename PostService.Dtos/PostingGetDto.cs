using PostService.CommonTypes;

namespace PostService.Dtos;

/// <summary>
/// Відправлення, яке сервер повертає клієнту.
/// </summary>
public class PostingGetDto
{
    /// <summary>Унікальний номер відправлення.</summary>
    public int Id { get; set; }

    /// <summary>Відправник: ім'я та прізвище.</summary>
    public string From { get; set; } = string.Empty;

    /// <summary>Отримувач: ім'я та прізвище.</summary>
    public string To { get; set; } = string.Empty;

    /// <summary>Вміст відправлення.</summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>Тип доставки.</summary>
    public DeliveryType DeliveryType { get; set; }

    /// <summary>Вага відправлення, кг.</summary>
    public float Weight { get; set; }

    /// <summary>Ширина відправлення, см.</summary>
    public float Width { get; set; }

    /// <summary>Висота відправлення, см.</summary>
    public float Height { get; set; }

    /// <summary>Глибина відправлення, см.</summary>
    public float Depth { get; set; }

    /// <summary>Оголошена цінність, грн. Може бути не вказана.</summary>
    public float? Value { get; set; }

    /// <summary>Вартість доставки, грн. Розраховується сервером.</summary>
    public float Price { get; set; }

    /// <summary>Дата й час створення відправлення.</summary>
    public DateTime CreatedAt { get; set; }
}