using Prosigliere.SimpleBlog.Domain.Exceptions;
using Prosigliere.SimpleBlog.Domain.Entity;
using FluentAssertions;

namespace Prosigliere.SimpleBlog.UnitTests.Domain.Entity;

public class BlogPostTest
{
    private class BlogPostData
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
    }

    private BlogPostData GetInitialData() => new BlogPostData
    {
        Title = "Sample Title",
        Content = "Sample Content"
    };

    private BlogPost CreateBlogPost(BlogPostData data) =>
        new BlogPost(data.Title!, data.Content!);

    [Fact(DisplayName = nameof(Instantiate))]
    [Trait("Domain", "BlogPost - Aggregates")]
    public void Instantiate()
    {
        var validData = new
        {
            Title = "Sample Title",
            Content = "Sample Content"
        };
        var dateTimeBefore = DateTime.Now;

        var blogPost = new BlogPost(
            validData.Title,
            validData.Content
        );
        var dateTimeAfter = DateTime.Now;

        blogPost.Should().NotBeNull();
        blogPost.Title.Should().Be(validData.Title);
        blogPost.Content.Should().Be(validData.Content);
        blogPost.CreatedAt.Should().NotBe(default);
        blogPost.CreatedAt.Should().BeAfter(dateTimeBefore).And.BeBefore(dateTimeAfter);
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenTitleIsEmpty))]
    [Trait("Domain", "BlogPost - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("  ")]
    public void InstantiateErrorWhenTitleIsEmpty(string? title)
    {
        var data = new
        {
            Title = title,
            Content = "Sample Content"
        };

        Action action = () => new BlogPost(
            data.Title!,
            data.Content
        );

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Title should not be empty or null");
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenContentIsEmpty))]
    [Trait("Domain", "BlogPost - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("  ")]
    public void InstantiateErrorWhenContentIsEmpty(string? content)
    {
        var data = new
        {
            Title = "Sample Title",
            Content = content
        };

        Action action = () => new BlogPost(
            data.Title,
            data.Content!
        );

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Content should not be empty or null");
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenTitleIsLessThan4Characters))]
    [Trait("Domain", "BlogPost - Aggregates")]
    [InlineData("abc")]
    [InlineData("a")]
    public void InstantiateErrorWhenTitleIsLessThan4Characters(string invalidTitle)
    {
        var data = new
        {
            Title = invalidTitle,
            Content = "Sample Content"
        };

        Action action = () => new BlogPost(
            data.Title,
            data.Content
        );

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Title should be greater than 3 characters");
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenTitleIsGreaterThan255Characters))]
    [Trait("Domain", "BlogPost - Aggregates")]
    public void InstantiateErrorWhenTitleIsGreaterThan255Characters()
    {
        var invalidTitle = new string('a', 256);

        Action action = () => new BlogPost(
            invalidTitle,
            "Sample Content"
        );

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Title should be less than 255 characters");
    }

    [Fact(DisplayName = nameof(Update))]
    [Trait("Domain", "BlogPost - Aggregates")]
    public void Update()
    {
        var initialData = new
        {
            Title = "Sample Title",
            Content = "Sample Content"
        };
        var blogPost = new BlogPost(
            initialData.Title,
            initialData.Content
        );
        var updatedData = new
        {
            Title = "Updated Title",
            Content = "Updated Content"
        };

        blogPost.Update(
            updatedData.Title,
            updatedData.Content
        );

        blogPost.Title.Should().Be(updatedData.Title);
        blogPost.Content.Should().Be(updatedData.Content);
        blogPost.UpdatedAt.Should().NotBeNull();
    }

    [Theory(DisplayName = nameof(UpdateErrorWhenTitleIsEmpty))]
    [Trait("Domain", "BlogPost - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("  ")]
    public void UpdateErrorWhenTitleIsEmpty(string? title)
    {
        var initialData = GetInitialData();
        var blogPost = CreateBlogPost(initialData);

        Action action = () => blogPost.Update(title!, initialData.Content);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Title should not be empty or null");
    }

    [Theory(DisplayName = nameof(UpdateErrorWhenContentIsEmpty))]
    [Trait("Domain", "BlogPost - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("  ")]
    public void UpdateErrorWhenContentIsEmpty(string? content)
    {
        var initialData = GetInitialData();
        var blogPost = CreateBlogPost(initialData);

        Action action = () => blogPost.Update(initialData.Title, content!);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Content should not be empty or null");
    }

    [Theory(DisplayName = nameof(UpdateErrorWhenTitleIsLessThan4Characters))]
    [Trait("Domain", "BlogPost - Aggregates")]
    [InlineData("abc")]
    [InlineData("a")]
    public void UpdateErrorWhenTitleIsLessThan4Characters(string invalidTitle)
    {
        var initialData = GetInitialData();
        var blogPost = CreateBlogPost(initialData);

        Action action = () => blogPost.Update(invalidTitle, initialData.Content);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Title should be greater than 3 characters");
    }

    [Fact(DisplayName = nameof(UpdateErrorWhenTitleIsGreaterThan255Characters))]
    [Trait("Domain", "BlogPost - Aggregates")]
    public void UpdateErrorWhenTitleIsGreaterThan255Characters()
    {
        var invalidTitle = new string('a', 256);
        var initialData = GetInitialData();
        var blogPost = CreateBlogPost(initialData);

        Action action = () => blogPost.Update(invalidTitle, initialData.Content);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Title should be less than 255 characters");
    }

    [Fact(DisplayName = nameof(AddComment))]
    [Trait("Domain", "BlogPost - Aggregates")]
    public void AddComment()
    {
        var blogPost = CreateBlogPost(GetInitialData());
        var commentContent = "This is a comment";

        blogPost.AddComment(commentContent);

        blogPost.Comments.Should().HaveCount(1);
        blogPost.Comments.First().Content.Should().Be(commentContent);
        blogPost.Comments.First().CreatedAt.Should().NotBe(default);
    }

}
