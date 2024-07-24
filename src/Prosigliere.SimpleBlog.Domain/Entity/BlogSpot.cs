using Prosigliere.SimpleBlog.Domain.Exceptions;
using Prosigliere.SimpleBlog.SeedWork;

namespace Prosigliere.SimpleBlog.Domain.Entity;
public class BlogPost : AggregateRoot
    {
        public BlogPost() { }

        public BlogPost(string title, string content) : base()
        {
            Title = title;
            Content = content;
            CreatedAt = DateTime.Now;
            Validate();
        }

        public string Title { get; private set; }
        public string Content { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public ICollection<Comment> Comments { get; private set; } = new List<Comment>();

        public void Update(string title, string content)
        {
            Title = title;
            Content = content;
            UpdatedAt = DateTime.Now;
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Title))
                throw new EntityValidationException($"{nameof(Title)} should not be empty or null");
            if (Title.Length <= 3)
                throw new EntityValidationException($"{nameof(Title)} should be greater than 3 characters");
            if (Title.Length >= 255)
                throw new EntityValidationException($"{nameof(Title)} should be less than 255 characters");
            if (string.IsNullOrWhiteSpace(Content))
                throw new EntityValidationException($"{nameof(Content)} should not be empty or null");
            if (Content.Length <= 3)
                throw new EntityValidationException($"{nameof(Content)} should be greater than 3 characters");
            if (Content.Length >= 10000)
                throw new EntityValidationException($"{nameof(Content)} should be less than 10000 characters");
        }

        public void AddComment(string content)
        {
            var comment = new Comment(content, Id);
            Comments.Add(comment);
        }

    }