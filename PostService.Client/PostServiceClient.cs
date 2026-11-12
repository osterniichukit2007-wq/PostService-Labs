using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using PostService.Dtos;

namespace PostService.Client;

public class PostServiceClient : IPostServiceClient
{
    // Сервер передає тип доставки рядком ("Department", "Courier")
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter() }
    };

    private readonly HttpClient _httpClient;

    public PostServiceClient()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("http://localhost:5077");
    }

    public async Task<PostingGetDto[]> GetPostingsAsync()
    {
        using var response = await _httpClient.GetAsync("/postings");
        await EnsureSuccessAsync(response);
        return await response.Content.ReadFromJsonAsync<PostingGetDto[]>(JsonOptions) ?? [];
    }

    public async Task<PostingGetDto> GetPostingByIdAsync(int id)
    {
        using var response = await _httpClient.GetAsync($"/postings/{id}");
        await EnsureSuccessAsync(response);
        return (await response.Content.ReadFromJsonAsync<PostingGetDto>(JsonOptions))!;
    }

    public async Task<PostingGetDto> CreatePostingAsync(PostingPostDto posting)
    {
        using var response = await _httpClient.PostAsJsonAsync("/postings", posting, JsonOptions);
        await EnsureSuccessAsync(response);
        return (await response.Content.ReadFromJsonAsync<PostingGetDto>(JsonOptions))!;
    }

    public async Task<PostingGetDto> UpdatePostingAsync(int id, PostingPutDto posting)
    {
        using var response = await _httpClient.PutAsJsonAsync($"/postings/{id}", posting, JsonOptions);
        await EnsureSuccessAsync(response);
        return (await response.Content.ReadFromJsonAsync<PostingGetDto>(JsonOptions))!;
    }

    public async Task DeletePostingAsync(int id)
    {
        using var response = await _httpClient.DeleteAsync($"/postings/{id}");
        await EnsureSuccessAsync(response);
    }

    // Перетворює неуспішну відповідь на виняток; для успішної нічого не робить.
    private static async Task EnsureSuccessAsync(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        // 400 з JSON-тілом — помилки валідації за полями
        var mediaType = response.Content.Headers.ContentType?.MediaType;
        if (response.StatusCode == HttpStatusCode.BadRequest
            && mediaType is not null && mediaType.EndsWith("json", StringComparison.OrdinalIgnoreCase))
        {
            ValidationErrorResponse? problem = null;
            try
            {
                problem = await response.Content.ReadFromJsonAsync<ValidationErrorResponse>(JsonOptions);
            }
            catch (JsonException)
            {
                // тіло не відповідає очікуваному формату — нижче буде звичайний HttpRequestException
            }

            if (problem?.Errors is { Count: > 0 })
            {
                throw new PostingValidationException(problem.Errors);
            }
        }

        throw new HttpRequestException(
            $"Сервер повернув помилку {(int)response.StatusCode}.",
            null,
            response.StatusCode);
    }
}