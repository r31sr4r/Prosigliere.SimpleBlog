using Prosigliere.SimpleBlog.Application;

namespace Prosigliere.SimpleBlog.Infra.Data.EF;

public class UnitOfWork
    : IUnitOfWork
{
    private readonly ProsigliereSimpleBlogDbContext _context;

    public UnitOfWork(ProsigliereSimpleBlogDbContext context)
    {
        _context = context;
    }

    public Task Commit(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public Task Rollback(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}