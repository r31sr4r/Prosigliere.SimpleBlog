using Prosigliere.SimpleBlog.Domain.Exceptions;
using Prosigliere.SimpleBlog.Domain.SeedWork;

namespace Prosigliere.SimpleBlog.Domain.Entity;
  public class Comment : BaseEntity
    {
        public Comment() { }

        public Comment(string content, Guid blogPostId)
        {
            Content = content;
            BlogPostId = blogPostId;
            CreatedAt = DateTime.Now;
            Validate();
        }

        public string Content { get; private set; }
        public Guid BlogPostId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        public void Update(string content)
        {
            Content = content;
            UpdatedAt = DateTime.Now;
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Content))
                throw new EntityValidationException($"{nameof(Content)} should not be empty or null");
            if (Content.Length <= 3)
                throw new EntityValidationException($"{nameof(Content)} should be greater than 3 characters");
            if (Content.Length >= 1000)
                throw new EntityValidationException($"{nameof(Content)} should be less than 1000 characters");
        }
    }