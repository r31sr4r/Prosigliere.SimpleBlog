using FluentAssertions;
using Prosigliere.SimpleBlog.Domain.Exceptions;
using Prosigliere.SimpleBlog.Domain.Entity;

namespace Prosigliere.SimpleBlog.UnitTests.Domain.Entities;

public class CommentTest
{
    [Fact(DisplayName = nameof(Can_Create_Comment_With_Valid_Data))]
    [Trait("Domain", "Comment - Aggregates")]
    public void Can_Create_Comment_With_Valid_Data()
    {
        var blogPost = new BlogPost("Valid Title", "Valid Content");
        var content = "This is a valid comment.";
        var dateTimeBefore = DateTime.Now;

        var comment = new Comment(content, blogPost.Id);
        var dateTimeAfter = DateTime.Now;

        comment.Should().NotBeNull();
        comment.Content.Should().Be(content);
        comment.BlogPostId.Should().Be(blogPost.Id);
        comment.CreatedAt.Should().NotBe(default);
        comment.CreatedAt.Should().BeAfter(dateTimeBefore).And.BeBefore(dateTimeAfter);
    }

    [Theory(DisplayName = nameof(Cannot_Create_Comment_With_Invalid_Data))]
    [Trait("Domain", "Comment - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("  ")]
    public void Cannot_Create_Comment_With_Invalid_Data(string? content)
    {
        var blogPost = new BlogPost("Valid Title", "Valid Content");

        Action action = () => new Comment(content!, blogPost.Id);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Content should not be empty or null");
    }

    [Theory(DisplayName = nameof(Cannot_Create_Comment_When_Content_Is_Less_Than_4_Characters))]
    [Trait("Domain", "Comment - Aggregates")]
    [InlineData("abc")]
    [InlineData("a")]
    public void Cannot_Create_Comment_When_Content_Is_Less_Than_4_Characters(string invalidContent)
    {
        var blogPost = new BlogPost("Valid Title", "Valid Content");

        Action action = () => new Comment(invalidContent, blogPost.Id);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Content should be greater than 3 characters");
    }

    [Fact(DisplayName = nameof(Cannot_Create_Comment_When_Content_Is_Greater_Than_1000_Characters))]
    [Trait("Domain", "Comment - Aggregates")]
    public void Cannot_Create_Comment_When_Content_Is_Greater_Than_1000_Characters()
    {
        var invalidContent = new string('a', 1001);
        var blogPost = new BlogPost("Valid Title", "Valid Content");

        Action action = () => new Comment(invalidContent, blogPost.Id);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Content should be less than 1000 characters");
    }

    [Fact(DisplayName = nameof(Can_Update_Comment))]
    [Trait("Domain", "Comment - Aggregates")]
    public void Can_Update_Comment()
    {
        var blogPost = new BlogPost("Valid Title", "Valid Content");
        var comment = new Comment("Initial Content", blogPost.Id);
        var updatedContent = "Updated Content";

        comment.Update(updatedContent);

        comment.Content.Should().Be(updatedContent);
        comment.UpdatedAt.Should().NotBeNull();
    }

    [Theory(DisplayName = nameof(Cannot_Update_Comment_With_Invalid_Data))]
    [Trait("Domain", "Comment - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("  ")]
    public void Cannot_Update_Comment_With_Invalid_Data(string? content)
    {
        var blogPost = new BlogPost("Valid Title", "Valid Content");
        var comment = new Comment("Initial Content", blogPost.Id);

        Action action = () => comment.Update(content!);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Content should not be empty or null");
    }

    [Theory(DisplayName = nameof(Cannot_Update_Comment_When_Content_Is_Less_Than_4_Characters))]
    [Trait("Domain", "Comment - Aggregates")]
    [InlineData("abc")]
    [InlineData("a")]
    public void Cannot_Update_Comment_When_Content_Is_Less_Than_4_Characters(string invalidContent)
    {
        var blogPost = new BlogPost("Valid Title", "Valid Content");
        var comment = new Comment("Initial Content", blogPost.Id);

        Action action = () => comment.Update(invalidContent);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Content should be greater than 3 characters");
    }

     [Fact(DisplayName = nameof(Cannot_Update_Comment_When_Content_Is_Greater_Than_1000_Characters))]
    [Trait("Domain", "Comment - Aggregates")]
    public void Cannot_Update_Comment_When_Content_Is_Greater_Than_1000_Characters()
    {
        var invalidContent = new string('a', 1001);
        var blogPost = new BlogPost("Valid Title", "Valid Content");
        var comment = new Comment("Initial Content", blogPost.Id);

        Action action = () => comment.Update(invalidContent);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Content should be less than 1000 characters");
    }
}
