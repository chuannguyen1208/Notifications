using AutoMapper;
using Modules.Blog.Shared;
using Modules.Blog.UseCases.Entity;

namespace Modules.Blog.UseCases.Blogs;
internal class BlogProfile : Profile
{
	public BlogProfile()
	{
		CreateMap<BlogEntity, BlogDto>();
		CreateMap<EditBlogDto, BlogEntity>();
	}
}
