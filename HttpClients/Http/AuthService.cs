using System.ComponentModel.DataAnnotations;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;
using HttpClients.Implementations;

namespace HttpClients.Http;


public class AuthService : IAuthService
{

	private IEnumerable<User>? users { get; set; }
	private IUserService userService = new UserHttpClient(new HttpClient());

	public AuthService()
	{
		// this.userService = userService;
		GetUsers();

		Console.WriteLine(users);
	}

	public Task<User> ValidateUser(string username, string password)
	{
		Console.WriteLine(users.FirstOrDefault().UserName);

		User? existingUser = users.FirstOrDefault(u =>
			u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));

		if (existingUser == null)
		{
			throw new Exception("User not found");
		}

		if (!existingUser.Password.Equals(password))
		{
			throw new Exception("Password mismatch");
		}

		return Task.FromResult(existingUser);
	}

	private async Task GetUsers()
	{
		users = await userService.GetUsersAsync();
	}

	public Task RegisterUser(User user)
	{

		if (string.IsNullOrEmpty(user.UserName))
		{
			throw new ValidationException("Username cannot be null");
		}

		if (string.IsNullOrEmpty(user.Password))
		{
			throw new ValidationException("Password cannot be null");
		}

		// save to persistence instead of list

		UserDto dto = new UserDto(user.UserName, user.Password);
		userService.CreateAsync(dto);

		return Task.CompletedTask;
	}
}
