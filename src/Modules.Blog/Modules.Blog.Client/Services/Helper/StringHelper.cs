using System.Text.RegularExpressions;

namespace Modules.Blog.Client.Services.Helper;

public static partial class StringHelper
{
	[GeneratedRegex("!\\[[^\\]]*\\]\\((blob:[^)]+)\\)", RegexOptions.Compiled)]
	public static partial Regex MarkdownImgBlobGeneratedRegex();
}
