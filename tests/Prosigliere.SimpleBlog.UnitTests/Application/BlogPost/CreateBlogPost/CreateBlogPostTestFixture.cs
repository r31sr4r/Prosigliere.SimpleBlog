using Prosigliere.SimpleBlog.Application.UseCases.BlogPost.CreateBlogPost;
using Prosigliere.SimpleBlog.UnitTests.Application.BlogPost.Common;

namespace Prosigliere.SimpleBlog.UnitTests.Application.BlogPost.CreateBlogPost;


[CollectionDefinition(nameof(CreateBlogPostTestFixture))]
public class CreateBlogPostTestFixtureCollection
    : ICollectionFixture<CreateBlogPostTestFixture>
{ }

public class CreateBlogPostTestFixture : BlogPostUseCasesBaseFixture
{
    public CreateBlogPostInput GetInput()
    {
        var blogPost = GetValidBlogPost();
        return new CreateBlogPostInput(
            blogPost.Title,
            blogPost.Content
        );
    }

    public CreateBlogPostInput GetInputWithInvalidTitle()
    {
        var input = GetInput();
        input.Title = "ab"; 
        return input;
    }

    public CreateBlogPostInput GetInputWithInvalidContent()
    {
        var input = GetInput();
        input.Content = ""; 
        return input;
    }
}