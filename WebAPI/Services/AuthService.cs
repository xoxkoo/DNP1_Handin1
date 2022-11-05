using System.ComponentModel.DataAnnotations;
using Application.Logic;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;
using HttpClients.Implementations;


namespace WebAPI.Services;

public class AuthService : IAuthService
{

	private IUserService userService = new UserHttpClient(new HttpClient());
	private readonly IList<User> users2 = new List<User>
	{
		new User
		{
			UserName = "trmo",
			Password = "onetwo3FOUR",

		}
	};

	private IEnumerable<User> users;


	public async Task<User> ValidateUser(string username, string password)
	{

		users = await userService.GetUsersAsync();

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

		return await Task.FromResult(existingUser);
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
		// Do more user info validation here

		userService.CreateAsync(new UserAuthDto(user.UserName, user.Password));

		return Task.CompletedTask;
	}
}
