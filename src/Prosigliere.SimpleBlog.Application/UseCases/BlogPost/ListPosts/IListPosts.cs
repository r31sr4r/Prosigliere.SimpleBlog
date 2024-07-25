using MediatR;

namespace Prosigliere.SimpleBlog.Application.UseCases.BlogPost.ListPosts;

public interface IListPosts : IRequestHandler<ListBlogPostsInput, ListBlogPostsOutput>
{}