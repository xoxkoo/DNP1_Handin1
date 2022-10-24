namespace Domain.DTOs;

public class UserRegisterDto
{
	public string UserName { get;}
	public string Password { get; }

	public UserRegisterDto(string userName, string password)
	{
		UserName = userName;
		Password = password;
	}
}
