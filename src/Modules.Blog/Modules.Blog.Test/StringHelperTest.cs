using Modules.Blog.Client.Services.Helper;
using System.Text.RegularExpressions;
using System.Text;

namespace Modules.Blog.Test;

public class StringHelperTest
{
	[Fact]
	public void Test1()
	{
		string content = "<head></head><body><p><img src=\"blob:http://localhost:5000/f8b32c59-5e15-46e7-b29e-74f9cbb10825\" alt=\"beauty-category-2.jpg\"></p>";
		string base64  = "base64Sample";
		var imgsMatches = StringHelper.MarkdownImgBlobGeneratedRegex().Matches(content);

		if (imgsMatches.Count > 0)
		{
			var contentStringBuilder = new StringBuilder(content);

			foreach (Match match in imgsMatches.Cast<Match>())
			{
				var blobUrl = match.Groups[1].Value;
				contentStringBuilder.Replace(blobUrl, base64);
			}

		}

		Assert.True(true);
	}
}