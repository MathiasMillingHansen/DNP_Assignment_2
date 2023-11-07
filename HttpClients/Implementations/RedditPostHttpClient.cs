using System.Net.Http.Json;
using System.Text.Json;
using Clients.ClientInterfaces;
using Shared.DTOs.RedditPost;
using Shared.Models;

namespace Clients.Implementations;

public class RedditPostHttpClient : IRedditPostService
{
    private readonly HttpClient _client;

    public RedditPostHttpClient(HttpClient client)
    {
        _client = client;
    }

    public async Task CreatePostAsync(RedditPostCreationDto postToCreate)
    {
        var response = await _client.PostAsJsonAsync("/RedditPost", postToCreate);
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task<ICollection<RedditPost>> GetPostsAsync(string? owner, string? title, string? id)
    {
        var query = ConstructQuery(owner, title, id);

        var response = await _client.GetAsync("/RedditPost" + query);
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new Exception(content);
        return JsonSerializer.Deserialize<ICollection<RedditPost>>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }

    public async Task<RedditPost> GetPostByIdAsync(int id)
    {
        var response = await _client.GetAsync($"/RedditPost/{id}");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new Exception(content);
        var temp = JsonSerializer.Deserialize<RedditPost>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        return temp;
    }

    public async Task UpdateAsync(RedditPostUpdateDto dto)
    {
        var response = await _client.PatchAsync($"/RedditPost/{dto.Id}", JsonContent.Create(dto));
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task DeletePostAsync(int id)
    {
        var response = await _client.DeleteAsync($"RedditPost/{id}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    private static string ConstructQuery(string? owner, string? title, string? id)
    {
        var query = "";
        if (!string.IsNullOrEmpty(owner)) query += $"?owner={owner}";
        if (!string.IsNullOrEmpty(title)) query += string.IsNullOrEmpty(query) ? $"?title={title}" : $"&title={title}";
        if (!string.IsNullOrEmpty(id)) query += string.IsNullOrEmpty(query) ? $"?id={id}" : $"&id={id}";

        return query;
    }
}