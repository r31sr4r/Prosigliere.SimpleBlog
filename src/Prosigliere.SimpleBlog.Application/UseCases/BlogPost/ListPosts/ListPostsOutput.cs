using Prosigliere.SimpleBlog.Application.Common;
using Prosigliere.SimpleBlog.Application.UseCases.BlogPost.Common;

namespace Prosigliere.SimpleBlog.Application;

 public class ListPostsOutput
        : PaginatedListOutput<BlogPostModelOutput>
{
    public ListPostsOutput(
        int page,
        int perPage,
        int total,
        IReadOnlyList<BlogPostModelOutput> items)
        : base(page, perPage, total, items)
    { }
}