using MediatR;

namespace Prosigliere.SimpleBlog.Application;

public interface IListPosts : IRequestHandler<ListBlogPostsInput, ListBlogPostsOutput>
{}