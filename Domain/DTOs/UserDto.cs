namespace Domain.DTOs;

public class UserDto
{
	public int Id { get; }
	public string UserName { get;}

	public UserDto(int id, string userName)
	{
		Id = id;
		UserName = userName;
	}
}
