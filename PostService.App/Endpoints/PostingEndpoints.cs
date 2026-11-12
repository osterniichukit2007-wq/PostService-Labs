using PostService.BusinessLogic;
using PostService.Dtos;
using PostService.Mappings;

namespace PostService.Endpoints;

/// <summary>
/// Ендпоінти для роботи з поштовими відправленнями.
/// </summary>
internal static class PostingEndpoints
{
    /// <summary>
    /// Реєструє всі ендпоінти відправлень у застосунку.
    /// </summary>
    /// <param name="app">Веб-застосунок, до якого додаються ендпоінти.</param>
    public static void MapPostingEndpoints(this WebApplication app)
    {
        app.MapGet("/postings", GetPostings)
            .WithName("GetPostings")
            .WithTags("Відправлення")
            .Produces<List<PostingGetDto>>(StatusCodes.Status200OK);

        app.MapGet("/postings/{id}", GetPosting)
            .WithName("GetPosting")
            .WithTags("Відправлення")
            .Produces<PostingGetDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        app.MapPost("/postings", CreatePosting)
            .WithName("CreatePosting")
            .WithTags("Відправлення")
            .Produces<PostingGetDto>(StatusCodes.Status201Created)
            .ProducesValidationProblem();

        app.MapPut("/postings/{id}", UpdatePosting)
            .WithName("UpdatePosting")
            .WithTags("Відправлення")
            .Produces<PostingGetDto>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status404NotFound);

        app.MapDelete("/postings/{id}", DeletePosting)
            .WithName("DeletePosting")
            .WithTags("Відправлення")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }

    /// <summary>
    /// Отримати список усіх відправлень
    /// </summary>
    /// <remarks>
    /// Повертає всі відправлення, збережені в системі. Якщо відправлень немає, повертає порожній список.
    /// </remarks>
    /// <param name="postingService">Сервіс відправлень (підставляється автоматично).</param>
    /// <returns>Список відправлень.</returns>
    /// <response code="200">Список відправлень успішно отримано.</response>
    private static IResult GetPostings(IPostingService postingService)
    {
        var postings = postingService.GetAll();
        var result = postings.Select(PostingMapper.ToPostingGetDto).ToList();
        return Results.Ok(result);
    }

    /// <summary>
    /// Отримати відправлення за номером
    /// </summary>
    /// <remarks>
    /// Повертає одне відправлення за його унікальним номером.
    /// </remarks>
    /// <param name="id">Унікальний номер відправлення.</param>
    /// <param name="postingService">Сервіс відправлень (підставляється автоматично).</param>
    /// <returns>Знайдене відправлення.</returns>
    /// <response code="200">Відправлення знайдено.</response>
    /// <response code="404">Відправлення з таким номером не існує.</response>
    private static IResult GetPosting(int id, IPostingService postingService)
    {
        var posting = postingService.Find(id);
        if (posting is null)
            return Results.NotFound();

        return Results.Ok(PostingMapper.ToPostingGetDto(posting));
    }

    /// <summary>
    /// Створити нове відправлення
    /// </summary>
    /// <remarks>
    /// Створює відправлення з указаними даними. Вартість доставки розраховується сервером автоматично.
    /// Усі розміри та вага мають бути більшими за 0, відправник, отримувач і вміст — не порожніми.
    /// </remarks>
    /// <param name="postDto">Дані нового відправлення.</param>
    /// <param name="postingService">Сервіс відправлень (підставляється автоматично).</param>
    /// <returns>Створене відправлення з присвоєним номером і розрахованою вартістю.</returns>
    /// <response code="201">Відправлення створено.</response>
    /// <response code="400">Дані не пройшли перевірку. У відповіді вказано, які поля некоректні.</response>
    private static IResult CreatePosting(PostingPostDto postDto, IPostingService postingService)
    {
        var errors = ValidatePosting(
            postDto.From, postDto.To, postDto.Content,
            postDto.Weight, postDto.Width, postDto.Height, postDto.Depth, postDto.Value);
        if (errors.Count > 0)
            return Results.ValidationProblem(errors);

        var newPosting = PostingMapper.ToPosting(postDto);
        var savedObject = postingService.Create(newPosting);
        var resultDto = PostingMapper.ToPostingGetDto(savedObject);
        return Results.Created($"/postings/{resultDto.Id}", resultDto);
    }

    /// <summary>
    /// Оновити відправлення
    /// </summary>
    /// <remarks>
    /// Замінює дані наявного відправлення. Номер береться з адреси запиту, а не з тіла.
    /// Вартість доставки під час оновлення не перераховується.
    /// </remarks>
    /// <param name="id">Унікальний номер відправлення, яке оновлюється.</param>
    /// <param name="putDto">Нові дані відправлення.</param>
    /// <param name="postingService">Сервіс відправлень (підставляється автоматично).</param>
    /// <returns>Оновлене відправлення.</returns>
    /// <response code="200">Відправлення оновлено.</response>
    /// <response code="400">Дані не пройшли перевірку. У відповіді вказано, які поля некоректні.</response>
    /// <response code="404">Відправлення з таким номером не існує.</response>
    private static IResult UpdatePosting(int id, PostingPutDto putDto, IPostingService postingService)
    {
        var errors = ValidatePosting(
            putDto.From, putDto.To, putDto.Content,
            putDto.Weight, putDto.Width, putDto.Height, putDto.Depth, putDto.Value);
        if (errors.Count > 0)
            return Results.ValidationProblem(errors);

        putDto.Id = id;
        var posting = PostingMapper.ToPosting(putDto);
        var updated = postingService.Update(posting);
        if (updated is null)
            return Results.NotFound();

        return Results.Ok(PostingMapper.ToPostingGetDto(updated));
    }

    /// <summary>
    /// Видалити відправлення
    /// </summary>
    /// <remarks>
    /// Видаляє відправлення за його унікальним номером.
    /// </remarks>
    /// <param name="id">Унікальний номер відправлення, яке видаляється.</param>
    /// <param name="postingService">Сервіс відправлень (підставляється автоматично).</param>
    /// <response code="204">Відправлення видалено.</response>
    /// <response code="404">Відправлення з таким номером не існує.</response>
    private static IResult DeletePosting(int id, IPostingService postingService)
    {
        var deletedCount = postingService.Delete(id);
        if (deletedCount == 0)
            return Results.NotFound();

        return Results.NoContent();
    }

    /// <summary>
    /// Перевіряє дані відправлення. Повертає словник «поле → повідомлення»;
    /// порожній словник означає, що дані коректні.
    /// </summary>
    private static Dictionary<string, string[]> ValidatePosting(
        string from, string to, string content,
        double weight, double width, double height, double depth, double value)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(from))
            errors["From"] = new[] { "Вкажіть відправника: ім'я та прізвище." };
        if (string.IsNullOrWhiteSpace(to))
            errors["To"] = new[] { "Вкажіть отримувача: ім'я та прізвище." };
        if (string.IsNullOrWhiteSpace(content))
            errors["Content"] = new[] { "Вкажіть вміст відправлення." };
        if (weight <= 0)
            errors["Weight"] = new[] { "Вага має бути більшою за 0 кг." };
        if (width <= 0)
            errors["Width"] = new[] { "Ширина має бути більшою за 0 см." };
        if (height <= 0)
            errors["Height"] = new[] { "Висота має бути більшою за 0 см." };
        if (depth <= 0)
            errors["Depth"] = new[] { "Глибина має бути більшою за 0 см." };
        if (value < 0)
            errors["Value"] = new[] { "Оголошена цінність не може бути від'ємною." };

        return errors;
    }
}