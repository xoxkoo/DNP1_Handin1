namespace Domain.DTOs;

public class UserDto
{
	public string UserName { get;}
	public string Password { get; }

	public UserDto(string userName, string password)
	{
		UserName = userName;
		Password = password;
	}
}
