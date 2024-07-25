using MediatR;
using Prosigliere.SimpleBlog.Application.UseCases.BlogPost.Common;

namespace Prosigliere.SimpleBlog.Application.UseCases.BlogPost.AddComment;

    public interface IAddComment : IRequestHandler<AddCommentInput, CommentModelOutput>
    {
    }