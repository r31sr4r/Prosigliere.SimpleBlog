using MediatR;
using Prosigliere.SimpleBlog.Application.UseCases.BlogPost.Common;

namespace Prosigliere.SimpleBlog.Application.UseCases.BlogPost.CreateBlogPost;

  public class CreateBlogPostInput : IRequest<BlogPostModelOutput>
    {
        public CreateBlogPostInput(string title, string content)
        {
            Title = title;
            Content = content;
        }

        public string Title { get; set; }
        public string Content { get; set; }
    }