using Prosigliere.SimpleBlog.Application.UseCases.BlogPost.Common;
using Prosigliere.SimpleBlog.Domain.Repository;

namespace Prosigliere.SimpleBlog.Application;

public class ListPosts : IListPosts
{
    private readonly IBlogPostRepository _blogPostRepository;

    public ListPosts(IBlogPostRepository blogPostRepository)
    {
        _blogPostRepository = blogPostRepository;
    }

    public async Task<ListPostsOutput> Handle(
        ListPostsInput request,
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
        return new ListPostsOutput(
            searchOutput.CurrentPage,
            searchOutput.PerPage,
            searchOutput.Total,
            searchOutput.Items
                .Select(BlogPostModelOutput.FromBlogPost)
                .ToList()
        );
    }
}
