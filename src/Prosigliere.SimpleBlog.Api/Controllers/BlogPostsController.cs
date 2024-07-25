using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prosigliere.SimpleBlog.Application.UseCases.BlogPost.AddComment;
using Prosigliere.SimpleBlog.Application.UseCases.BlogPost.CreateBlogPost;
using Prosigliere.SimpleBlog.Application.UseCases.BlogPost.GetBlogPost;
using Prosigliere.SimpleBlog.Application.UseCases.BlogPost.Common;
using Prosigliere.SimpleBlog.Api.ApiModels.Response;
using Prosigliere.SimpleBlog.Application;
using Prosigliere.SimpleBlog.Domain.SeedWork.SearchebleRepository;

namespace Prosigliere.SimpleBlog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogPostsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BlogPostsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(BlogPostModelOutput), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Create(
            [FromBody] CreateBlogPostApiInput apiInput,
            CancellationToken cancellationToken
        )
        {
            var input = new CreateBlogPostInput(
                apiInput.Title,
                apiInput.Content
            );

            var result = await _mediator.Send(input, cancellationToken);

            return CreatedAtAction(
                nameof(Create),
                new { result.Id },
                new ApiResponse<BlogPostModelOutput>(result)
            );
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(BlogPostModelOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(
            [FromRoute] Guid id,
            CancellationToken cancellationToken
        )
        {
            var result = await _mediator.Send(
                new GetBlogPostInput(id),
                cancellationToken
            );

            return Ok(new ApiResponse<BlogPostModelOutput>(result));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ListBlogPostsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> List(
            CancellationToken cancellation,
            [FromQuery] int? page = null,
            [FromQuery(Name = "per_page")] int? perPage = null,
            [FromQuery] string? search = null,
            [FromQuery] string? sort = null,
            [FromQuery] SearchOrder? dir = null
        )
        {
            var input = new ListBlogPostsInput();
            if (page.HasValue)
                input.Page = page.Value;
            if (perPage.HasValue)
                input.PerPage = perPage.Value;
            if (!string.IsNullOrWhiteSpace(search))
                input.Search = search;
            if (!string.IsNullOrWhiteSpace(sort))
                input.Sort = sort;
            if (dir.HasValue)
                input.Dir = dir.Value;

            var output = await _mediator.Send(input, cancellation);

            return Ok(new ApiResponseList<BlogPostModelOutput>(output));
        }

        [HttpPost("{id:guid}/comments")]
        [ProducesResponseType(typeof(CommentModelOutput), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> AddComment(
            [FromRoute] Guid id,
            [FromBody] AddCommentInput input,
            CancellationToken cancellationToken
        )
        {
            input.BlogPostId = id;
            var result = await _mediator.Send(input, cancellationToken);
            return CreatedAtAction(
                nameof(GetById),
                new { id = input.BlogPostId },
                result
            );
        }
    }
}
