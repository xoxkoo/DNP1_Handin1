namespace Domain.DTOs;

public class UserCreationDto
{
	public string UserName { get;}
	public string Password { get; }
	public string PasswordRepeat { get; }

	public UserCreationDto(string userName, string password, string passwordRepeat)
	{
		UserName = userName;
		Password = password;
		PasswordRepeat = passwordRepeat;
	}
}
