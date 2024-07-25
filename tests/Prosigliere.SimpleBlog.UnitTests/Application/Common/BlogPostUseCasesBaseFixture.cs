using Moq;
using Prosigliere.SimpleBlog.Application;
using Prosigliere.SimpleBlog.Domain.Repository;
using Prosigliere.SimpleBlog.UnitTests.Common;
using DomainEntity = Prosigliere.SimpleBlog.Domain.Entity;

namespace Prosigliere.SimpleBlog.UnitTests.Application.BlogPost.Common;

public class BlogPostUseCasesBaseFixture : BaseFixture
{
    public Mock<IBlogPostRepository> GetRepositoryMock() => new();

    public Mock<IUnitOfWork> GetUnitOfWorkMock() => new();

    public string GetValidTitle()
    {
        var title = "";
        while (title.Length < 3)
            title = Faker.Lorem.Sentence();
        if (title.Length > 255)
            title = title[..255];
        return title;
    }

    public string GetValidContent()
        => Faker.Lorem.Paragraphs();

    public DomainEntity.BlogPost GetValidBlogPost()
        => new(
            GetValidTitle(),
            GetValidContent()
        );

    public List<DomainEntity.BlogPost> GetExampleBlogPostList(int length = 10)
    {
        return Enumerable.Range(1, length)
            .Select(_ => GetValidBlogPost())
            .ToList();
    }
}
