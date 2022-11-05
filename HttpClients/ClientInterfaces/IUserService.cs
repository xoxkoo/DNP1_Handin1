using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IUserService
{
	Task<User> CreateAsync(UserDto dto);
	Task<IEnumerable<User>> GetUsersAsync(string? usernameContains = null);
	Task<User> GetUserAsync(int id);

}
