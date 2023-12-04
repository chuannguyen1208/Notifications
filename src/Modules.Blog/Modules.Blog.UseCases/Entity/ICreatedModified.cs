namespace Modules.Blog.UseCases.Entity;
public interface ICreatedModified
{
	DateTimeOffset Created { get; set; }
	string CreatedBy { get; set; }
	DateTimeOffset? Modified { get; set; }
	string ModifiedBy { get; set; }
}
