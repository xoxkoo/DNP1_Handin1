namespace Domain.Models;

public class Post
{
	public int Id { get; set; }
	public int UserId { get; set; }
	public string Title { get; set; }
	public string Body { get; set; }

	public Post(int userId, string title, string body)
	{
		UserId = userId;
		Title = title;
		Body = body;
	}

	private Post() {}
}
