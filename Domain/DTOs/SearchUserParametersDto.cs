namespace Domain.DTOs;

public class SearchUserParametersDto
{
	public string? UsernameContains { get;  }
	public int? Id { get; }

	//The property is marked with "?", i.e. string? to indicate this search parameter can be null, i.e. it should be ignored when searching users
	public SearchUserParametersDto(int? id, string? usernameContains)
	{
		UsernameContains = usernameContains;
		Id = id;
	}
}
