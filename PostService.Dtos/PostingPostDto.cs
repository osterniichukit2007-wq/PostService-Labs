using PostService.CommonTypes;

namespace PostService.Dtos;

/// <summary>
/// Дані для створення нового відправлення.
/// </summary>
public class PostingPostDto
{
    /// <summary>Відправник: ім'я та прізвище. Обов'язкове поле.</summary>
    public string From { get; set; } = string.Empty;

    /// <summary>Отримувач: ім'я та прізвище. Обов'язкове поле.</summary>
    public string To { get; set; } = string.Empty;

    /// <summary>Вміст відправлення. Обов'язкове поле.</summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>Тип доставки.</summary>
    public DeliveryType DeliveryType { get; set; }

    /// <summary>Вага відправлення, кг. Має бути більшою за 0.</summary>
    public float Weight { get; set; }

    /// <summary>Ширина відправлення, см. Має бути більшою за 0.</summary>
    public float Width { get; set; }

    /// <summary>Висота відправлення, см. Має бути більшою за 0.</summary>
    public float Height { get; set; }

    /// <summary>Глибина відправлення, см. Має бути більшою за 0.</summary>
    public float Depth { get; set; }

    /// <summary>Оголошена цінність, грн. Не може бути від'ємною.</summary>
    public float Value { get; set; }
}