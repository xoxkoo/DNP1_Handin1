using App.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class PostEfcDao : IPostDao
{
	private readonly AppDbContext context;

	public PostEfcDao(AppDbContext context)
	{
		this.context = context;
	}
	public async Task<Post> CreateAsync(Post post)
	{
		EntityEntry<Post> created = await context.Posts.AddAsync(post);
		await context.SaveChangesAsync();
		return created.Entity;
	}

	public async Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
	{
		IQueryable<Post> result = context.Posts.AsQueryable();

		if (!string.IsNullOrEmpty(searchParameters.TitleContains))
		{
			// we know username is unique, so just fetch the first
			result = result.Where(p =>
				EF.Functions.Like(p.Title, $"%{searchParameters.TitleContains}%"));
		}

		if (searchParameters.UserId != null)
		{
			result = result.Where(p => p.UserId == searchParameters.UserId);
		}

		if (searchParameters.Id != null)
		{
			result = result.Where(p => p.Id == searchParameters.Id);
		}

		List<Post> posts = await result.ToListAsync();

		return posts;
	}

	public async Task<Post?> GetByIdAsync(int id)
	{
		Post? post = await context.Posts.FindAsync(id);
		return post;
	}
}
