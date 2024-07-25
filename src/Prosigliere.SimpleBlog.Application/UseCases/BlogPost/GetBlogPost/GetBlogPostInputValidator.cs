using FluentValidation;

namespace Prosigliere.SimpleBlog.Application.UseCases.BlogPost.GetBlogPost;

  public class GetBlogPostInputValidator : AbstractValidator<GetBlogPostInput>
    {
        public GetBlogPostInputValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
