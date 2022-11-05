using App.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
	private readonly IUserDao userDao;

	public UserLogic(IUserDao userDao)
	{
		this.userDao = userDao;
	}

	public async Task<User> CreateAsync(UserDto dto)
	{
		User? existing = await userDao.GetByUsernameAsync(dto.UserName);
		if (existing != null)
			throw new Exception("Username already taken!");

		ValidateData(dto);
		User toCreate = new User
		{
			UserName = dto.UserName,
			Password = dto.Password
		};

		User created = await userDao.CreateAsync(toCreate);

		return created;
	}

	// public Task<User> ValidateLogin(string username, string password)
	// {
		// Task<IEnumerable<User>> users = userDao.GetAsync(new SearchUserParametersDto(null, username));
		// User? existingUser = users.FirstOrDefault(u =>
		// 	u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
		//
		// if (existingUser == null)
		// {
		// 	throw new Exception("User not found");
		// }
		//
		// if (!existingUser.Password.Equals(password))
		// {
		// 	throw new Exception("Password mismatch");
		// }
		//
		// return Task.FromResult();
	// }

	private static void ValidateData(UserDto userToCreate)
	{
		string userName = userToCreate.UserName;

		if (userName.Length < 3)
			throw new Exception("Username must be at least 3 characters!");

		if (userName.Length > 15)
			throw new Exception("Username must be less than 16 characters!");
	}

	public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
	{
		return userDao.GetAsync(searchParameters);
	}
}
