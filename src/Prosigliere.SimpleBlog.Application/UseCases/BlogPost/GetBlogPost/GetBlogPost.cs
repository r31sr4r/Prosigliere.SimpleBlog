using Prosigliere.SimpleBlog.Application.UseCases.BlogPost.Common;
using Prosigliere.SimpleBlog.Domain.Repository;

namespace Prosigliere.SimpleBlog.Application.UseCases.BlogPost.GetBlogPost;

public class GetBlogPost : IGetBlogPost
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public GetBlogPost(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        public async Task<BlogPostModelOutput> Handle(GetBlogPostInput request, CancellationToken cancellationToken)
        {
            var blogPost = await _blogPostRepository.Get(request.Id, cancellationToken);
            return BlogPostModelOutput.FromBlogPost(blogPost);
        }
    }