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

	public async Task<User> RegisterUser(UserCreationDto dto)
	{

		users = await userService.GetUsersAsync();

		if (string.IsNullOrEmpty(dto.UserName))
		{
			throw new ValidationException("Username cannot be null");
		}

		if (string.IsNullOrEmpty(dto.Password))
		{
			throw new ValidationException("Password cannot be null");
		}

		User? existingUser = users.FirstOrDefault(u =>
			u.UserName.Equals(dto.UserName, StringComparison.OrdinalIgnoreCase));

		if (existingUser != null)
		{
			throw new Exception("This username is taken");
		}

		if (dto.Password != dto.PasswordRepeat)
		{
			throw new ValidationException("Passwords do not match");
		}

		return await userService.CreateAsync(new UserAuthDto(dto.UserName, dto.Password));
	}
}
