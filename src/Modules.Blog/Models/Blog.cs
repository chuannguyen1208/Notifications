namespace Modules.Blog.Models;

public class Blog
{
	public int Id { get; set; }
	public required string Title { get; set; }
	public string Description { get; set; } = string.Empty;
	public string ImgUrl { get; set; } = string.Empty;
}
