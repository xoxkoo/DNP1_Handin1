namespace Domain.DTOs;

public class UserLoginDto
{
	public string UserName { get;}
	public string Password { get; }

	public UserLoginDto(string userName, string password)
	{
		UserName = userName;
		Password = password;
	}
}
