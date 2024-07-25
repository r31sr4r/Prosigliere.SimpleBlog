using Microsoft.EntityFrameworkCore;
using Prosigliere.SimpleBlog.Domain.Entity;
using Prosigliere.SimpleBlog.Infra.Data.EF.Configurations;

namespace Prosigliere.SimpleBlog.Infra.Data.EF;

public class ProsigliereSimpleBlogDbContext : DbContext
    {
        public DbSet<BlogPost> BlogPosts => Set<BlogPost>();
        public DbSet<Comment> Comments => Set<Comment>();

        public ProsigliereSimpleBlogDbContext(DbContextOptions<ProsigliereSimpleBlogDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BlogPostConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
        }
    }