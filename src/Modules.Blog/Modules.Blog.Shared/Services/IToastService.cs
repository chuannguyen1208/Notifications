namespace Modules.Blog.Shared.Services;
public interface IToastService
{
	Task ToastInfo(string message);
	Task ToastSuccess(string message);
	Task ToastError(string message);
}
