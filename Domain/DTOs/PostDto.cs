namespace Domain.DTOs;

public class PostDto
{
	public int OwnerId { get; }
	public string Title { get; }
	public string Body { get; }

	public PostDto(int ownerId, string title, string body)
	{
		OwnerId = ownerId;
		Title = title;
		Body = body;
	}
}
