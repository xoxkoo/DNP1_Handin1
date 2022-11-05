using System.Net.Http.Json;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class UserHttpClient : IUserService
{
	private readonly HttpClient client;

	public UserHttpClient(HttpClient client)
	{
		this.client = client;
	}

	public async Task<User> CreateAsync(UserAuthDto dto)
	{
		HttpResponseMessage response = await client.PostAsJsonAsync("/users", dto);
		string result = await response.Content.ReadAsStringAsync();
		if (!response.IsSuccessStatusCode)
		{
			throw new Exception(result);
		}

		User user = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		})!;
		return user;
	}

	public async Task<IEnumerable<User>> GetUsersAsync(string? usernameContains = null)
	{
		string uri = "https://localhost:7208/users";
		if (!string.IsNullOrEmpty(usernameContains))
		{
			uri += $"?username={usernameContains}";
		}
		HttpResponseMessage response = await client.GetAsync(uri);
		string result = await response.Content.ReadAsStringAsync();
		if (!response.IsSuccessStatusCode)
		{
			throw new Exception(result);
		}

		IEnumerable<User> users = JsonSerializer.Deserialize<IEnumerable<User>>(result, new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		})!;
		return users;
	}

	public async Task<User> GetUserAsync(int id)
	{
		HttpResponseMessage response = await client.GetAsync("https://localhost:7208/Users?id=" + id);
		string result = await response.Content.ReadAsStringAsync();
		if (!response.IsSuccessStatusCode)
		{
			throw new Exception(result);
		}

		IEnumerable<User> users = JsonSerializer.Deserialize<IEnumerable<User>>(result, new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		})!;

		return users.FirstOrDefault();
	}
}
