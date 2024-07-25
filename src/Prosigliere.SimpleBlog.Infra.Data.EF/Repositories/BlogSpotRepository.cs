using Microsoft.EntityFrameworkCore;
using Prosigliere.SimpleBlog.Application;
using Prosigliere.SimpleBlog.Domain.Entity;
using Prosigliere.SimpleBlog.Domain.Repository;

namespace Prosigliere.SimpleBlog.Infra.Data.EF.Repositories;

public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ProsigliereSimpleBlogDbContext _context;
        private DbSet<BlogPost> _blogPosts => _context.Set<BlogPost>();

        public BlogPostRepository(ProsigliereSimpleBlogDbContext context)
        {
            _context = context;
        }

        public async Task Insert(BlogPost aggregate, CancellationToken cancellationToken)
            => await _blogPosts.AddAsync(aggregate, cancellationToken);

        public async Task<BlogPost> Get(Guid id, CancellationToken cancellationToken)
        {
            var blogPost = await _blogPosts.AsNoTracking().FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken
            );
            if (blogPost == null)
            {
                throw new NotFoundException($"BlogPost with id {id} not found");
            }
            return blogPost!;
        }

        public Task Update(BlogPost aggregate, CancellationToken _)
            => Task.FromResult(_blogPosts.Update(aggregate));

        public Task Delete(BlogPost aggregate, CancellationToken _)
            => Task.FromResult(_blogPosts.Remove(aggregate));


}