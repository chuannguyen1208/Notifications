namespace Modules.Blog.Shared;

public class EditBlogDto
{
	public int Id { get; set; }
	public required string Title { get; set; }
	public string Description { get; set; } = string.Empty;
}

public class BlogDto : EditBlogDto
{
}
