using Prosigliere.SimpleBlog.Application.UseCases.BlogPost.Common;
using Prosigliere.SimpleBlog.Domain.Repository;
using DomainEntity = Prosigliere.SimpleBlog.Domain.Entity;

namespace Prosigliere.SimpleBlog.Application.UseCases.BlogPost.CreateBlogPost;

public class CreateBlogPost : ICreateBlogPost
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBlogPost(IBlogPostRepository blogPostRepository, IUnitOfWork unitOfWork)
        {
            _blogPostRepository = blogPostRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BlogPostModelOutput> Handle(CreateBlogPostInput request, CancellationToken cancellationToken)
        {
            var blogPost = new DomainEntity.BlogPost(
                request.Title,
                request.Content
            );

            await _blogPostRepository.Insert(blogPost, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);

            return BlogPostModelOutput.FromBlogPost(blogPost);
        }
    }
