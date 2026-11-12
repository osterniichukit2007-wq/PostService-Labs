using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using PostService.Dtos;

namespace PostService.Client;

public class ApiException : Exception
{
    public HttpStatusCode StatusCode { get; }

    public ApiException(HttpStatusCode statusCode, string body)
        : base(BuildMessage(statusCode, body))
    {
        StatusCode = statusCode;
    }

    private static string BuildMessage(HttpStatusCode statusCode, string body)
    {
        var message = $"Сервер повернув статус {(int)statusCode}.";
        if (!string.IsNullOrWhiteSpace(body))
        {
            var text = body.Length > 300 ? body[..300] : body;
            message += $" {text}";
        }
        return message;
    }
}

public class PostingApiClient
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter() }
    };

    private readonly HttpClient _http = new()
    {
        BaseAddress = new Uri("http://localhost:5077")
    };

    public async Task<PostingGetDto[]> GetListAsync()
    {
        var result = await _http.GetFromJsonAsync<PostingGetDto[]>("postings", JsonOptions);
        return result ?? [];
    }

    public async Task<PostingGetDto> CreateAsync(PostingPostDto dto)
    {
        var response = await _http.PostAsJsonAsync("postings", dto, JsonOptions);
        await EnsureSuccessAsync(response);
        return (await response.Content.ReadFromJsonAsync<PostingGetDto>(JsonOptions))!;
    }

    public async Task<PostingGetDto> UpdateAsync(PostingPutDto dto)
    {
        var response = await _http.PutAsJsonAsync($"postings/{dto.Id}", dto, JsonOptions);
        await EnsureSuccessAsync(response);
        return (await response.Content.ReadFromJsonAsync<PostingGetDto>(JsonOptions))!;
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"postings/{id}");
        await EnsureSuccessAsync(response);
    }

    private static async Task EnsureSuccessAsync(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        var body = await response.Content.ReadAsStringAsync();
        throw new ApiException(response.StatusCode, body);
    }
}