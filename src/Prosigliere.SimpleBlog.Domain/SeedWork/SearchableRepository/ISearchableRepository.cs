namespace Prosigliere.SimpleBlog.Domain.SeedWork.SearchebleRepository;

public interface ISearchableRepository<TAggregate>
    where TAggregate : AggregateRoot
{
    Task<SearchOutput<TAggregate>> Search(
        SearchInput input,
        CancellationToken cancellationToken
    );
}