using FluentAssertions;
using Prosigliere.SimpleBlog.Application.UseCases.BlogPost.CreateBlogPost;
using Prosigliere.SimpleBlog.Domain.Exceptions;
using UseCases = Prosigliere.SimpleBlog.Application.UseCases.BlogPost.CreateBlogPost;
using DomainEntity = Prosigliere.SimpleBlog.Domain.Entity;
using Moq;

namespace Prosigliere.SimpleBlog.UnitTests.Application.BlogPost.CreateBlogPost;

[Collection(nameof(CreateBlogPostTestFixture))]
public class CreateBlogPostTest
{
    private readonly CreateBlogPostTestFixture _fixture;

    public CreateBlogPostTest(CreateBlogPostTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(CreateBlogPost))]
    [Trait("Application", "Create BlogPost - Use Cases")]
    public async void CreateBlogPost()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

        var useCase = new UseCases.CreateBlogPost(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        var input = _fixture.GetInput();

        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(
            repository => repository.Insert(
                It.IsAny<DomainEntity.BlogPost>(),
                It.IsAny<CancellationToken>()
            ),
            Times.Once
        );

        unitOfWorkMock.Verify(
            unitOfWork => unitOfWork.Commit(It.IsAny<CancellationToken>()),
            Times.Once
        );

        output.Should().NotBeNull();
        output.Title.Should().Be(input.Title);
        output.Content.Should().Be(input.Content);
        output.Id.Should().NotBeEmpty();
        output.CreatedAt.Should().NotBeSameDateAs(default);
    }

    [Theory(DisplayName = nameof(ThrowWhenCantInstantiateBlogPost))]
    [Trait("Application", "Create BlogPost - Use Cases")]
    [MemberData(
        nameof(CreateBlogPostTestDataGenerator.GetInvalidInputs),
        parameters: 6,
        MemberType = typeof(CreateBlogPostTestDataGenerator)
    )]
    public async void ThrowWhenCantInstantiateBlogPost(
        CreateBlogPostInput input,
        string exceptionMessage
    )
    {
        var useCase = new UseCases.CreateBlogPost(
            _fixture.GetRepositoryMock().Object,
            _fixture.GetUnitOfWorkMock().Object
        );

        Func<Task> task = async () => await useCase.Handle(input, CancellationToken.None);

        await task.Should()
            .ThrowAsync<EntityValidationException>()
            .WithMessage(exceptionMessage);
    }
}
