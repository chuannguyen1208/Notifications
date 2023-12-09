namespace Modules.Blog.Shared.Services;
public interface IUserService
{
	Task<bool> IsAuthenticated();
	Task<string?> CurrentUserName();
}
