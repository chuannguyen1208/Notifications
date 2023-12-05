using Markdig;

namespace Modules.Blog.UseCases.Blogs;
public class MarkdigProvider
{
	private readonly MarkdownPipeline _markdownPipeline;


	public MarkdigProvider()
	{
		_markdownPipeline = new MarkdownPipelineBuilder()
			.UsePipeTables()
			.UseAdvancedExtensions()
			.Build();
	}

	public string ToHtml(string markdown)
	{
		var html = Markdown.ToHtml(markdown, _markdownPipeline);
		return html;
	}
}
