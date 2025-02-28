﻿namespace Prosigliere.SimpleBlog.Domain.SeedWork.SearchebleRepository;

public class SearchOutput<TAggregate>
    where TAggregate : AggregateRoot
{
    public SearchOutput(
        int currentPage,
        int perPage,
        int total,
        IReadOnlyList<TAggregate> items)
    {
        CurrentPage = currentPage;
        PerPage = perPage;
        Total = total;
        Items = items;
    }

    public int CurrentPage { get; set; }
    public int PerPage { get; set; }
    public int Total { get; set; }

    public IReadOnlyList<TAggregate> Items { get; set; }
}