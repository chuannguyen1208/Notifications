using Modules.Blog.Shared.Services;

namespace Modules.Blog.Services;

internal class BlobService(IHttpClientFactory httpClientFactory) : IBlobService
{
	public async Task<string> BlobUrlToBase64(string blobUrl)
	{
		using var client = httpClientFactory.CreateClient("default");
		var bytes = await client.GetByteArrayAsync(blobUrl);
		var base64 = Convert.ToBase64String(bytes);
		return $"data:image/png;base64,{base64}";
	}
}
