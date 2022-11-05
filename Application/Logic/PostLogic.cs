using App.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
	private readonly IPostDao PostDao;
	private readonly IUserDao UserDao;


	public PostLogic(IPostDao postDao, IUserDao userDao)
	{
		PostDao = postDao;
		UserDao = userDao;
	}

	public async Task<Post> CreateAsync(PostDto dto)
	{
		User? user = await UserDao.GetByIdAsync(dto.OwnerId);
		if (user == null)
		{
			throw new Exception($"User with id {dto.OwnerId} was not found.");
		}

		Validate(dto);
		Post post = new Post(user.Id, dto.Title, dto.Body);
		Post created = await PostDao.CreateAsync(post);
		return created;
	}

	public async Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
	{
		return await PostDao.GetAsync(searchParameters);
	}

	private void Validate(PostDto dto)
	{
		if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
		else if (string.IsNullOrEmpty(dto.Body)) throw new Exception("Body cannot be empty.");
	}

}
