using FluentValidation;

namespace Prosigliere.SimpleBlog.Application.UseCases.BlogPost.AddComment;

public class AddCommentInputValidator : AbstractValidator<AddCommentInput>
{
    public AddCommentInputValidator()
    {
        RuleFor(x => x.BlogPostId)
            .NotEmpty();
            
        RuleFor(x => x.Content)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(1000);
    }
}