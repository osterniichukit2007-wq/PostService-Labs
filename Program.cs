using PostService.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

var postings = new List<Posting>
{
    new Posting
    {
        Id = 1,
        From = "Alice",
        To = "Bob",
        Content = "Books",
        DeliveryType = DeliveryType.Courier,
        Weight = 2.5f,
        Width = 30,
        Height = 20,
        Depth = 10,
        Value = 50.0f,
        Price = 10.0f,
        CreatedAt = DateTime.UtcNow
    },
    new Posting
    {
        Id = 2,
        From = "Ivan",
        To = "Olena",
        Content = "Documents",
        DeliveryType = DeliveryType.ExpressCourier,
        Weight = 0.5f,
        Width = 25,
        Height = 15,
        Depth = 2,
        Value = 100.0f,
        Price = 25.0f,
        CreatedAt = DateTime.UtcNow
    }
};

app.MapGet("/postings", () => postings);

app.MapGet("/postings/{id}", (int id) =>
{
    var posting = postings.FirstOrDefault(p => p.Id == id);
    return posting is null ? Results.NotFound() : Results.Ok(posting);
});

app.MapPost("/postings", (Posting posting) =>
{
    posting.Id = postings.Count == 0 ? 1 : postings.Max(p => p.Id) + 1;
    posting.CreatedAt = DateTime.UtcNow;
    postings.Add(posting);
    return Results.Created($"/postings/{posting.Id}", posting);
});

app.MapPut("/postings/{id}", (int id, Posting updated) =>
{
    var posting = postings.FirstOrDefault(p => p.Id == id);
    if (posting is null)
        return Results.NotFound();

    posting.From = updated.From;
    posting.To = updated.To;
    posting.Content = updated.Content;
    posting.DeliveryType = updated.DeliveryType;
    posting.Weight = updated.Weight;
    posting.Width = updated.Width;
    posting.Height = updated.Height;
    posting.Depth = updated.Depth;
    posting.Value = updated.Value;
    posting.Price = updated.Price;

    return Results.Ok(posting);
});

app.MapDelete("/postings/{id}", (int id) =>
{
    var posting = postings.FirstOrDefault(p => p.Id == id);
    if (posting is null)
        return Results.NotFound();

    postings.Remove(posting);
    return Results.NoContent();
});

app.Run();
