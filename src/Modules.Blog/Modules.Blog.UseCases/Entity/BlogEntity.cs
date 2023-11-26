namespace Modules.Blog.UseCases.Entity;
public class BlogEntity
{
	public int Id { get; set; }
	public required string Title { get; set; }
	public string Description { get; set; } = string.Empty;
	public bool Published { get; set; }
}
