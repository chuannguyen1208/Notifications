using Modules.Blog.Shared.Services;

namespace Modules.Blog.Services;

internal class BlobService(IHttpClientFactory httpClientFactory) : IBlobService
{
	public async Task<string> BlobUrlToBase64(string blobUrl)
	{
		using var client = httpClientFactory.CreateClient();
		// Exeption: blob sheme not support
		var bytes = await client.GetByteArrayAsync(new Uri(blobUrl));
		var base64 = Convert.ToBase64String(bytes);
		return base64;
	}
}
