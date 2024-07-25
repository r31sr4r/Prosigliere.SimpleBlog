using Microsoft.EntityFrameworkCore;
using Prosigliere.SimpleBlog.Application.Exceptions;
using Prosigliere.SimpleBlog.Domain.Entity;
using Prosigliere.SimpleBlog.Domain.Repository;
using Prosigliere.SimpleBlog.Domain.SeedWork.SearchebleRepository;

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
        var blogPost = await _blogPosts
                .AsNoTracking()
                .Include(c => c.Comments)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        NotFoundException.ThrowIfNull(blogPost, $"CoBlogPostllection with id {id} not found");
        return blogPost!;
    }

    public Task Update(BlogPost aggregate, CancellationToken _)
        => Task.FromResult(_blogPosts.Update(aggregate));

    public Task Delete(BlogPost aggregate, CancellationToken _)
        => Task.FromResult(_blogPosts.Remove(aggregate));

    public async Task<SearchOutput<BlogPost>> Search(
        SearchInput input,
        CancellationToken cancellationToken)
    {
        var toSkip = (input.Page - 1) * input.PerPage;
        var query = _blogPosts
            .Include(p => p.Comments)
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(input.Search))
            query = query.Where(x => x.Title.Contains(input.Search) 
            || x.Content.Contains(input.Search));

        query = AddSorting(query, input.OrderBy, input.Order);

        var total = await query.CountAsync();
        var items = await query.AsNoTracking()
            .Skip(toSkip)
            .Take(input.PerPage)
            .ToListAsync();

        return new SearchOutput<BlogPost>(
            currentPage: input.Page,
            perPage: input.PerPage,
            total: total,
            items: items
        );
    }


    private IQueryable<BlogPost> AddSorting(
        IQueryable<BlogPost> query,
        string orderProperty,
        SearchOrder order
    )
    {
        var orderedEnumerable = (orderProperty, order) switch
        {
            ("title", SearchOrder.Asc) => query.OrderBy(x => x.Title),
            ("title", SearchOrder.Desc) => query.OrderByDescending(x => x.Title),
            ("createdAt", SearchOrder.Asc) => query.OrderBy(x => x.CreatedAt),
            ("createdAt", SearchOrder.Desc) => query.OrderByDescending(x => x.CreatedAt),
            _ => query.OrderBy(x => x.Title)
        };

        return orderedEnumerable
            .ThenBy(x => x.CreatedAt);
    }       
}