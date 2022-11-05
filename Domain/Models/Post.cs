namespace Domain.Models;

public class Post
{
	public int Id { get; set; }
	public int OwnerId { get; }
	public string Title { get; }
	public string Body { get; }

	public Post(int ownerId, string title, string body)
	{
		OwnerId = ownerId;
		Title = title;
		Body = body;
	}
}
