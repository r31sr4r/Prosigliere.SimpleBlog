using Prosigliere.SimpleBlog.Domain.Entity;
using Prosigliere.SimpleBlog.SeedWork;

namespace Prosigliere.SimpleBlog.Domain.Repository;

 public interface ICommentRepository : IRepository
{
    Task<Comment> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Comment>> ListAllAsync(CancellationToken cancellationToken);
    Task<Comment> AddAsync(Comment entity, CancellationToken cancellationToken);
    Task UpdateAsync(Comment entity, CancellationToken cancellationToken);
    Task DeleteAsync(Comment entity, CancellationToken cancellationToken);
    Task<IReadOnlyList<Comment>> GetCommentsByPostIdAsync(Guid postId, CancellationToken cancellationToken);
}
