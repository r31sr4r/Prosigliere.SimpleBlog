using MediatR;
using Prosigliere.SimpleBlog.Application.Common;
using Prosigliere.SimpleBlog.Domain.SeedWork.SearchebleRepository;

namespace Prosigliere.SimpleBlog.Application.UseCases.BlogPost.ListPosts;
public class ListBlogPostsInput
    : PaginatedListInput,
    IRequest<ListBlogPostsOutput>
{
    public ListBlogPostsInput(
        int page = 1,
        int perPage = 15,
        string search = "",
        string sort = "",
        SearchOrder dir = SearchOrder.Asc)
        : base(page, perPage, search, sort, dir)
    { }

    public ListBlogPostsInput()
        : base(1, 15, "", "", SearchOrder.Asc)
    { }
}
