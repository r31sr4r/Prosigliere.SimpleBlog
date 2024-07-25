using Prosigliere.SimpleBlog.Application.UseCases.BlogPost.Common;
using Prosigliere.SimpleBlog.Domain.Repository;
using DomainEntity = Prosigliere.SimpleBlog.Domain.Entity;

namespace Prosigliere.SimpleBlog.Application.UseCases.BlogPost.AddComment;

   public class AddComment : IAddComment
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddComment(
            IBlogPostRepository blogPostRepository,
            ICommentRepository commentRepository,
            IUnitOfWork unitOfWork)
        {
            _blogPostRepository = blogPostRepository;
            _commentRepository = commentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommentModelOutput> Handle(AddCommentInput request, CancellationToken cancellationToken)
        {
            var blogPost = await _blogPostRepository.Get(request.BlogPostId, cancellationToken);
            var comment = new DomainEntity.Comment(request.Content, blogPost.Id);

            await _commentRepository.AddAsync(comment, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);

            return CommentModelOutput.FromComment(comment);
        }
    }