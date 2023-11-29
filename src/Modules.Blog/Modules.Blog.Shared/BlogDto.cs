namespace Modules.Blog.Shared;

public class EditBlogDto
{
	public int Id { get; set; }
	public required string Title { get; set; }
	public string Description { get; set; } = string.Empty;
	public string Content { get; set; } = string.Empty;
	public bool Published { get; set; }
	public DateTimeOffset Created { get; set; }
}

public class BlogDto : EditBlogDto
{
}
