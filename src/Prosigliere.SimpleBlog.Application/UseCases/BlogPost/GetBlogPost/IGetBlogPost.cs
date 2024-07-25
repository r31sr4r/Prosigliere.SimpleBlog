using MediatR;
using Prosigliere.SimpleBlog.Application.UseCases.BlogPost.Common;

namespace Prosigliere.SimpleBlog.Application.UseCases.BlogPost.GetBlogPost;

 public interface IGetBlogPost : IRequestHandler<GetBlogPostInput, BlogPostModelOutput>
    {
    }
