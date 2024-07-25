using MediatR;
using Prosigliere.SimpleBlog.Application.UseCases.BlogPost.Common;

namespace Prosigliere.SimpleBlog.Application.UseCases.BlogPost.GetBlogPost;

public class GetBlogPostInput : IRequest<BlogPostModelOutput>
    {
        public GetBlogPostInput(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }