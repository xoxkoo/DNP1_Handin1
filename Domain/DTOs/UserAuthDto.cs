namespace Domain.DTOs;

public class UserAuthDto
{
	public string UserName { get;}
	public string Password { get; }

	public UserAuthDto(string userName, string password)
	{
		UserName = userName;
		Password = password;
	}
}
