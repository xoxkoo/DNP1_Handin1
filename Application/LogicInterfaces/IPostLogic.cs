using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IPostLogic
{
	Task<Post> CreateAsync(PostDto dto);
	Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters);

}
