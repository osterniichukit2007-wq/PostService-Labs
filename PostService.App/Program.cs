using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PostService.Models;
using PostService.Dtos;
using PostService.Mappings;
using PostService.BusinessLogic;
using PostService.DataAccess;
using PostService.Endpoints;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Ті самі налаштування для Swagger, щоб enum показувався текстом
builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
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

// Swagger: генерація OpenAPI-документа та підключення XML-коментарів
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API обліку поштових відправлень",
        Version = "v1",
        Description = "REST API для створення, перегляду, редагування та видалення поштових відправлень."
    });

    // Поля типу string без ? у коді вважаються обов'язковими
    options.SupportNonNullableReferenceTypes();

    // Підключаємо всі XML-файли документації (застосунок, DTO, типи)
    foreach (var xmlFile in Directory.GetFiles(AppContext.BaseDirectory, "PostService*.xml"))
    {
        options.IncludeXmlComments(xmlFile);
    }
});

builder.Services.AddDbContext<PostingDbContext>();
builder.Services.AddScoped<IPostingService, PostingService>();
builder.Services.AddScoped<IPostingRepository, PostingRepository>();

var app = builder.Build();

// CORS: має бути до MapGet/MapPost/...
app.UseCors();

// Swagger: JSON-документ і сторінка документації за адресою /swagger
app.UseSwagger();
app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PostingDbContext>();
    context.Database.EnsureCreated();
}

app.MapPostingEndpoints();

app.Run();