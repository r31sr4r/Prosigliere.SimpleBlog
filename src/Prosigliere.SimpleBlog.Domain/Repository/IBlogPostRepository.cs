using Prosigliere.SimpleBlog.Domain.Entity;
using Prosigliere.SimpleBlog.Domain.SeedWork;
using Prosigliere.SimpleBlog.Domain.SeedWork.SearchebleRepository;

namespace Prosigliere.SimpleBlog.Domain.Repository;

public interface IBlogPostRepository 
    : IGenericRepository<BlogPost>, ISearchableRepository<BlogPost>
{ }