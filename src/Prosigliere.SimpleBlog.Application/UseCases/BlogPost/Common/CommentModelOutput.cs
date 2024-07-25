using DomainEntity = Prosigliere.SimpleBlog.Domain.Entity;

namespace Prosigliere.SimpleBlog.Application.UseCases.BlogPost.Common;

 public class CommentModelOutput
{
    public CommentModelOutput(Guid id, string content, Guid blogPostId, DateTime createdAt, DateTime? updatedAt = null)
    {
        Id = id;
        Content = content;
        BlogPostId = blogPostId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public Guid Id { get; private set; }
    public string Content { get; private set; }
    public Guid BlogPostId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public static CommentModelOutput FromComment(DomainEntity.Comment comment)
    {
        return new CommentModelOutput(
            comment.Id,
            comment.Content,
            comment.BlogPostId,
            comment.CreatedAt,
            comment.UpdatedAt
        );
    }
}