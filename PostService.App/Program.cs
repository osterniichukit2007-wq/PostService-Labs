using Microsoft.EntityFrameworkCore;
using PostService.Models;
using PostService.Dtos;
using PostService.Mappings;
using PostService.BusinessLogic;
using PostService.DataAccess;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// CORS: дозволяємо вебсторінкам (Live Server) звертатися до API
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(
                "http://127.0.0.1:5500",
                "http://localhost:5500")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<PostingDbContext>();
builder.Services.AddScoped<IPostingService, PostingService>();
builder.Services.AddScoped<IPostingRepository, PostingRepository>();

var app = builder.Build();

// CORS: має бути до MapGet/MapPost/...
app.UseCors();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PostingDbContext>();
    context.Database.EnsureCreated();
}

// Перевірка даних відправлення. Повертає словник «поле -> повідомлення»,
// порожній словник означає, що дані коректні.
static Dictionary<string, string[]> ValidatePosting(
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

app.MapGet("/postings", (IPostingService postingService) =>
{
    var postings = postingService.GetAll();
    var result = postings.Select(PostingMapper.ToPostingGetDto).ToList();
    return Results.Ok(result);
});

app.MapGet("/postings/{id}", (int id, IPostingService postingService) =>
{
    var posting = postingService.Find(id);
    if (posting is null)
        return Results.NotFound();

    var resultDto = PostingMapper.ToPostingGetDto(posting);
    return Results.Ok(resultDto);
});

app.MapPost("/postings", (PostingPostDto postDto, IPostingService postingService) =>
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
});

app.MapPut("/postings/{id}", (int id, PostingPutDto putDto, IPostingService postingService) =>
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

    var resultDto = PostingMapper.ToPostingGetDto(updated);
    return Results.Ok(resultDto);
});

app.MapDelete("/postings/{id}", (int id, IPostingService postingService) =>
{
    var deletedCount = postingService.Delete(id);
    if (deletedCount == 0)
        return Results.NotFound();

    return Results.NoContent();
});

app.Run();