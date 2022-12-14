using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
	Task<User> CreateAsync(UserAuthDto userToCreate);
	public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters);
	// Task<User> ValidateUser(string userName, string password);
}
