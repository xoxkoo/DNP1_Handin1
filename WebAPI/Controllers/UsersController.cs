using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
	private readonly IUserLogic userLogic;

	public UsersController(IUserLogic userLogic)
	{
		this.userLogic = userLogic;
	}

	[HttpPost]
	public async Task<ActionResult<User>> CreateAsync(UserDto dto)
	{
		try
		{
			User user = await userLogic.CreateAsync(dto);
			return Created($"/users/{user.Id}", user);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			return StatusCode(500, e.Message);
		}
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<User>>> GetAsync([FromQuery] string? username, [FromQuery] int? id)
	{
		try
		{
			SearchUserParametersDto parameters = new(id, username);
			IEnumerable<User> users = await userLogic.GetAsync(parameters);

			return Ok(users);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			return StatusCode(500, e.Message);
		}
	}
}
