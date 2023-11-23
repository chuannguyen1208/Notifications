namespace Modules.Blog.UseCases.Blogs;
public class BlogDto
{
	public int Id { get; set; }
	public required string Name { get; set; }
	public string Description { get; set; } = string.Empty;
	public string ImgUrl { get; set; } = string.Empty;
}
