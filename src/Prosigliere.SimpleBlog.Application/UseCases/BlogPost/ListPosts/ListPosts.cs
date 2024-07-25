using Prosigliere.SimpleBlog.Application.UseCases.BlogPost.Common;
using Prosigliere.SimpleBlog.Domain.Repository;

namespace Prosigliere.SimpleBlog.Application.UseCases.BlogPost.ListPosts;

public class ListPosts : IListPosts
{
    private readonly IBlogPostRepository _blogPostRepository;

    public ListPosts(IBlogPostRepository blogPostRepository)
    {
        _blogPostRepository = blogPostRepository;
    }

    public async Task<ListBlogPostsOutput> Handle(
        ListBlogPostsInput request,
        CancellationToken cancellationToken)
    {
        var searchOutput = await _blogPostRepository.Search(
            new(
                request.Page,
                request.PerPage,
                request.Search,
                request.Sort,
                request.Dir
                ),
            cancellationToken
        );
        return new ListBlogPostsOutput(
            searchOutput.CurrentPage,
            searchOutput.PerPage,
            searchOutput.Total,
            searchOutput.Items
                .Select(BlogPostWithCommentCountModelOutput.FromBlogPost)
                .ToList()
        );
    }
}
