using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class PostHttpClient : IPostService
{
	private readonly HttpClient client;

	public PostHttpClient(HttpClient client)
	{
		this.client = client;
	}

	public async Task<Post> CreateAsync(PostDto dto)
	{
		string subFormAsJson = JsonSerializer.Serialize(dto);
		StringContent content = new(subFormAsJson, Encoding.UTF8, "application/json");

		HttpResponseMessage response = await client.PostAsync("https://localhost:7208/post", content);
		string result = await response.Content.ReadAsStringAsync();

		if (!response.IsSuccessStatusCode)
		{
			throw new Exception(result);
		}

		Post post = JsonSerializer.Deserialize<Post>(result, new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		})!;

		return post;
	}

	public async Task<ICollection<Post>> GetAsync(string? userName, int? id, string? titleContains)
	{
		string query = ConstructQuery(userName, id, titleContains);

		HttpResponseMessage response = await client.GetAsync("https://localhost:7208/post"+query);

		string content = await response.Content.ReadAsStringAsync();
		if (!response.IsSuccessStatusCode)
		{
			throw new Exception(content);
		}

		ICollection<Post> posts = JsonSerializer.Deserialize<ICollection<Post>>(content, new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		})!;
		return posts;
	}

	public async Task<ICollection<Post>> GetAllAsync()
	{
		HttpResponseMessage response = await client.GetAsync("https://localhost:7208/post");

		string content = await response.Content.ReadAsStringAsync();
		if (!response.IsSuccessStatusCode)
		{
			throw new Exception(content);
		}

		ICollection<Post> posts = JsonSerializer.Deserialize<ICollection<Post>>(content, new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		})!;

		return posts;
	}

	public async Task<Post> GetByIdAsync(int id)
	{
		HttpResponseMessage response = await client.GetAsync($"https://localhost:7208/Post?id={id}");
		string content = await response.Content.ReadAsStringAsync();
		if (!response.IsSuccessStatusCode)
		{
			throw new Exception(content);
		}

		Post post = JsonSerializer.Deserialize<Post>(content, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			}
		)!;
		return post;
	}

	private static string ConstructQuery(string? userName, int? id, string? titleContains)
	{
		string query = "";
		if (!string.IsNullOrEmpty(userName))
		{
			query += $"?username={userName}";
		}

		if (id != null)
		{
			query += string.IsNullOrEmpty(query) ? "?" : "&";
			query += $"id={id}";
		}

		if (!string.IsNullOrEmpty(titleContains))
		{
			query += string.IsNullOrEmpty(query) ? "?" : "&";
			query += $"titlecontains={titleContains}";
		}

		return query;
	}
}
