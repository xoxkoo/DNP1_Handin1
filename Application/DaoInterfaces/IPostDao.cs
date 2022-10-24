using Domain.Models;

namespace App.DaoInterfaces;

public interface IPostDao
{
	Task<Post> CreateAsync(Post post);
	// Task<IEnumerable<Post>> GetAsync(SearchTodoParametersDto searchParameters);
	Task UpdateAsync(Post post);
	Task<Post?> GetByIdAsync(int id);
	Task DeleteAsync(int id);
}
