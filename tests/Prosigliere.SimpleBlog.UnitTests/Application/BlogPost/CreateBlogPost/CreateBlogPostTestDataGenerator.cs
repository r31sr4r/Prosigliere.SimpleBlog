namespace Prosigliere.SimpleBlog.UnitTests.Application.BlogPost.CreateBlogPost;

public class CreateBlogPostTestDataGenerator
{
    public static IEnumerable<object[]> GetInvalidInputs(int numberOfIterations = 6)
    {
        var fixture = new CreateBlogPostTestFixture();
        var invalidInputsList = new List<object[]>();
        var totalInvalidCases = 2;

        for (int index = 0; index < numberOfIterations; index++)
        {
            switch (index % totalInvalidCases)
            {
                case 0:
                    invalidInputsList.Add(new object[]
                    {
                        fixture.GetInputWithInvalidTitle(),
                        "Title should be greater than 3 characters"
                    });
                    break;
                case 1:
                    invalidInputsList.Add(new object[]
                    {
                        fixture.GetInputWithInvalidContent(),
                        "Content should not be empty or null"
                    });
                    break;
            }
        }
        return invalidInputsList;
    }
}