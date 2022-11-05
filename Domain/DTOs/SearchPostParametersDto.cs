namespace Domain.DTOs;

public class SearchPostParametersDto
{
	public int? Id { get;}
	public int? UserId { get;}
	public string? TitleContains { get;}

	public SearchPostParametersDto(int? id, int? userId, string? titleContains)
	{
		Id = id;
		UserId = userId;
		TitleContains = titleContains;
	}
}
