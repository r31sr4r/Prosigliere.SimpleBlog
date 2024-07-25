using MediatR;
using Prosigliere.SimpleBlog.Application.UseCases.BlogPost.Common;

namespace Prosigliere.SimpleBlog.Application.UseCases.BlogPost.CreateBlogPost;

public interface ICreateBlogPost : IRequestHandler<CreateBlogPostInput, BlogPostModelOutput>
{
}
