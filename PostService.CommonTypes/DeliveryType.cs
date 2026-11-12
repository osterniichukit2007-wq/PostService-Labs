namespace PostService.CommonTypes;

/// <summary>
/// Тип доставки відправлення.
/// </summary>
public enum DeliveryType
{
    /// <summary>Доставка у відділення.</summary>
    Department,

    /// <summary>Доставка кур'єром.</summary>
    Courier,

    /// <summary>Експрес-доставка кур'єром.</summary>
    ExpressCourier
}