namespace Modules.Blog.UseCases.Entity;
public class BlogEntity
{
	public int Id { get; set; }
	public required string Title { get; set; }
	public string Description { get; set; } = string.Empty;
	public string Content { get; set; } = string.Empty;
	public bool Published { get; set; }


	public DateTimeOffset Created { get; set; }
	public string CreatedBy { get; set; } = string.Empty;
	public DateTimeOffset? Modified { get; set; }
	public string ModifiedBy { get; set;} = string.Empty;
}
