namespace Prosigliere.SimpleBlog.Api;

public class AddCommentApiInput
{
    public AddCommentApiInput(string content) => Content = content;

    public string Content { get; set; }
}


