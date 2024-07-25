using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prosigliere.SimpleBlog.Domain.Entity;

namespace Prosigliere.SimpleBlog.Infra.Data.EF.Configurations;

 internal class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
    {
        public void Configure(EntityTypeBuilder<BlogPost> builder)
        {
            builder.HasKey(bp => bp.Id);

            builder.Property(bp => bp.Title)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(bp => bp.Content)
                .IsRequired()
                .HasMaxLength(10000);

            builder.Property(bp => bp.CreatedAt)
                .IsRequired();

            builder.Property(bp => bp.UpdatedAt)
                .IsRequired(false);

            builder.HasMany(bp => bp.Comments)
                .WithOne()
                .HasForeignKey(c => c.BlogPostId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }