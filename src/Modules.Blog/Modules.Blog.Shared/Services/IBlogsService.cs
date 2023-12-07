namespace Modules.Blog.Shared.Services;
public interface IBlogsService
{
	Task<IEnumerable<BlogDto>> GetBlogsAsync();
	Task<BlogDto> GetBlogAsync(int id);
	Task<DashboardSummary> GetDashboardSummaryAsync();
	Task EditBlogAsync(EditBlogDto editBlogDto);
	Task DeleteAsync(int id);
}
