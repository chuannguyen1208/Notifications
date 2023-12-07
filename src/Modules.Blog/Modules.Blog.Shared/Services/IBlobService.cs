namespace Modules.Blog.Shared.Services;
public interface IBlobService
{
	Task<string> BlobUrlToBase64(string blobUrl);
}
