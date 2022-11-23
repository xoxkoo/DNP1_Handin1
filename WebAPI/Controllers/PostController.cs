using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
	private readonly IPostLogic postLogic;

	public PostController(IPostLogic postLogic)
	{
		this.postLogic = postLogic;
	}

	[HttpPost]
	public async Task<ActionResult<Post>> CreateAsync(PostDto dto)
	{
		try
		{
			Console.WriteLine('h' + dto.Title);
			Post created = await postLogic.CreateAsync(dto);
			return Created($"/posts/{created.Id}", created);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			return StatusCode(500, e.Message);
		}
	}

	[HttpGet]
	public async Task<ActionResult<Post>> GetAsync([FromQuery] int? id, [FromQuery] int? userId, [FromQuery] string? titleContains)
	{
		try
		{
			SearchPostParametersDto parameters = new(id, userId, titleContains);
			IEnumerable<Post> posts = await postLogic.GetAsync(parameters);
			return Ok(posts);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			return  StatusCode(500,e.Message);
		}

	}
}
