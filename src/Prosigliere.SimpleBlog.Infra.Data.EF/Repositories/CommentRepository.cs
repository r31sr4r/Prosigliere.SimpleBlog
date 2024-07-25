using Microsoft.EntityFrameworkCore;
using Prosigliere.SimpleBlog.Application.Exceptions;
using Prosigliere.SimpleBlog.Domain.Entity;
using Prosigliere.SimpleBlog.Domain.Repository;

namespace Prosigliere.SimpleBlog.Infra.Data.EF.Repositories;

public class CommentRepository : ICommentRepository
    {
        private readonly ProsigliereSimpleBlogDbContext _context;
        private DbSet<Comment> _comments => _context.Set<Comment>();

        public CommentRepository(ProsigliereSimpleBlogDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var comment = await _comments.AsNoTracking().FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken
            );
            if (comment == null)
            {
                throw new NotFoundException($"Comment with id {id} not found");
            }
            return comment!;
        }

        public async Task<IReadOnlyList<Comment>> ListAllAsync(CancellationToken cancellationToken)
        {
            return await _comments.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Comment> AddAsync(Comment entity, CancellationToken cancellationToken)
        {
            await _comments.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task UpdateAsync(Comment entity, CancellationToken cancellationToken)
        {
            _comments.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Comment entity, CancellationToken cancellationToken)
        {
            _comments.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Comment>> GetCommentsByPostIdAsync(Guid postId, CancellationToken cancellationToken)
        {
            return await _comments.AsNoTracking()
                .Where(c => c.BlogPostId == postId)
                .ToListAsync(cancellationToken);
        }
    }