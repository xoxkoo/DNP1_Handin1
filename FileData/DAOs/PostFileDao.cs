using App.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace FileData.DAOs;

public class PostFileDao : IPostDao
{
	private readonly FileContext context;

	public PostFileDao(FileContext context)
	{
		this.context = context;
	}

	public Task<Post> CreateAsync(Post post)
	{
		int postId = 1;
		if (context.Posts.Any())
		{
			postId = context.Posts.Max(p => p.Id);
			postId++;
		}

		post.Id = postId;

		context.Posts.Add(post);
		context.SaveChanges();

		return Task.FromResult(post);
	}

	public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
	{
		IEnumerable<Post> result = context.Posts.AsEnumerable();

		if (searchParameters.Id != null)
		{
			result = result.Where(p => p.Id == searchParameters.Id);
		}

		if (searchParameters.UserId != null)
		{
			result = result.Where(t => t.OwnerId == searchParameters.UserId);
		}

		if (!string.IsNullOrEmpty(searchParameters.TitleContains))
		{
			result = result.Where(t =>
				t.Title.Contains(searchParameters.TitleContains, StringComparison.OrdinalIgnoreCase));
		}

		return Task.FromResult(result);
	}

	public Task<Post?> GetByIdAsync(int id)
	{
		Post? existing = context.Posts.FirstOrDefault(t =>
			t.Id == id
		);
		return Task.FromResult(existing);
	}
}
