using DomainEntity = Prosigliere.SimpleBlog.Domain.Entity;

namespace Prosigliere.SimpleBlog.Application.UseCases.BlogPost.Common;

 public class BlogPostModelOutput
{
    public BlogPostModelOutput(
        Guid id, 
        string title, 
        string content, 
        DateTime createdAt, 
        DateTime? updatedAt = null, 
        List<CommentModelOutput>? comments = null)
    {
        Id = id;
        Title = title;
        Content = content;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Comments = comments ?? new List<CommentModelOutput>();
    }

    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public List<CommentModelOutput> Comments { get; private set; }

    public static BlogPostModelOutput FromBlogPost(DomainEntity.BlogPost blogPost)
    {
        return new BlogPostModelOutput(
            blogPost.Id,
            blogPost.Title,
            blogPost.Content,
            blogPost.CreatedAt,
            blogPost.UpdatedAt,
            blogPost.Comments.Select(CommentModelOutput.FromComment).ToList()
        );
    }
}