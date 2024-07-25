using Prosigliere.SimpleBlog.Application.Common;
using Prosigliere.SimpleBlog.Application.UseCases.BlogPost.Common;

namespace Prosigliere.SimpleBlog.Application.UseCases.BlogPost.ListPosts;

 public class ListBlogPostsOutput
        : PaginatedListOutput<BlogPostWithCommentCountModelOutput>
{
    public ListBlogPostsOutput(
        int page,
        int perPage,
        int total,
        IReadOnlyList<BlogPostWithCommentCountModelOutput> items)
        : base(page, perPage, total, items)
    { }
}