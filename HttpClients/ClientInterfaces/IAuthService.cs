using System.Security.Claims;
using Domain.DTOs;
using Domain.Models;

namespace BlazorWasm.Services;

public interface IAuthService
{
	public Task LoginAsync(string username, string password);
	public Task LogoutAsync();
	public Task RegisterAsync(UserCreationDto userCreationDto);
	public Task<ClaimsPrincipal> GetAuthAsync();

	public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
}
