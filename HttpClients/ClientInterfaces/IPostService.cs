using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IPostService
{
	Task<Post> CreateAsync(PostDto dto);
	Task<ICollection<Post>> GetAsync(
		string? userName,
		int? id,
		string? titleContains
	);
	Task<ICollection<Post>> GetAllAsync();
	Task<Post> GetByIdAsync(int id);
}
