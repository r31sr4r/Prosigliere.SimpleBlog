namespace Prosigliere.SimpleBlog.Application.UseCases.BlogPost.ListPosts;
public class BlogPostWithCommentCountModelOutput
{
    public BlogPostWithCommentCountModelOutput(
        Guid id, 
        string title, 
        string content, 
        DateTime createdAt, 
        int commentCount, 
        DateTime? updatedAt = null)
    {
        Id = id;
        Title = title;
        Content = content;
        CreatedAt = createdAt;
        CommentCount = commentCount;
        UpdatedAt = updatedAt;
    }

    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public int CommentCount { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public static BlogPostWithCommentCountModelOutput FromBlogPost(Domain.Entity.BlogPost blogPost)
    {
        return new BlogPostWithCommentCountModelOutput(
            blogPost.Id,
            blogPost.Title,
            blogPost.Content,
            blogPost.CreatedAt,
            blogPost.Comments.Count,
            blogPost.UpdatedAt
        );
    }
}
