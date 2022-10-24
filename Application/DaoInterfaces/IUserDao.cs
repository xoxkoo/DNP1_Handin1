
using Domain.DTOs;
using Domain.Models;

namespace App.DaoInterfaces;

public interface IUserDao
{
	Task<User> CreateAsync(User user);
	// We use the question mark User? to indicate we might return null, in case no user is found.
	Task<User?> GetByUsernameAsync(string userName);
	public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters);

	Task<User?> GetByIdAsync(int dtoOwnerId);
}
