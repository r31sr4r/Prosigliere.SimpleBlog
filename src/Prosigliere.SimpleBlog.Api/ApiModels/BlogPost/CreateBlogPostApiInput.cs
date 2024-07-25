namespace Prosigliere.SimpleBlog.Api;

 public class CreateBlogPostApiInput
{
    public CreateBlogPostApiInput(string title, string content)
    {
        Title = title;
        Content = content;
    }

    public string Title { get; set; }
    public string Content { get; set; }
}