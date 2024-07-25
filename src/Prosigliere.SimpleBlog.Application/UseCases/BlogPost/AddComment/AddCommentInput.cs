using MediatR;
using Prosigliere.SimpleBlog.Application.UseCases.BlogPost.Common;

namespace Prosigliere.SimpleBlog.Application.UseCases.BlogPost.AddComment;

 public class AddCommentInput : IRequest<CommentModelOutput>
{
    public AddCommentInput(Guid blogPostId, string content)
    {
        BlogPostId = blogPostId;
        Content = content;
    }

    public Guid BlogPostId { get; set; }
    public string Content { get; set; }
}