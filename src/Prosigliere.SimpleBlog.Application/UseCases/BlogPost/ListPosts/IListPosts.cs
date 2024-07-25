using MediatR;

namespace Prosigliere.SimpleBlog.Application;

public interface IListPosts : IRequestHandler<ListPostsInput, ListPostsOutput>
{}