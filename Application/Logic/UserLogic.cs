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

	public async Task<User> CreateAsync(UserRegisterDto dto)
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

	private static void ValidateData(UserRegisterDto userToCreate)
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
