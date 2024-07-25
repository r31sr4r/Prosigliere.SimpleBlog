
# Prosigliere.SimpleBlog

**Prosigliere.SimpleBlog** is a simple blogging platform developed as part of a selection process. The application allows authenticated users to create their own blog posts and add comments to specific posts. It includes functionalities to view posts and count associated comments.

## Features

- Create blog posts
- Retrieve a specific blog post by its ID, including its title, content, and associated comments
- List all blog posts with titles and the number of comments associated with each post
- Add a comment to a specific blog post

## Getting Started

### Prerequisites
- .NET 8 SDK
- Docker

### Running the Project
1. Start the Database
The project uses a database that needs to be started using Docker. Navigate to the root of the project and run:

```bash
 docker-compose up
```
2. Run the Application
Now you can run the application using the following command:

```bash
dotnet run --project src/Prosigliere.SimpleBlog.Api
```
The application will be available at http://localhost:5241. You can access the Swagger UI for API documentation and testing at http://localhost:5241/swagger/index.html.

## Running Tests

The project includes unit tests. To run the tests, navigate to the root directory and execute:

```bash
dotnet test
```

## Usage

### Create a Blog Post

To create a blog post, use the following endpoint:

- POST /api/posts
Example payload:

```javascript
{
  "Title": "My First Post",
  "Content": "This is the content of my first post."
}


```

### Retrieve a Blog Post by ID

- GET /api/posts/{id}

This endpoint retrieves a specific blog post by its ID, including its title, content, and a list of associated comments.

### List All Blog Posts

- GET /api/posts

This endpoint returns a list of all blog posts, including their titles and the number of comments associated with each post.

### Add a Comment to a Blog Post
- POST /api/posts/{id}/comments
Example payload:

```javascript
{
  "Content": "This is a comment."
}
```
### Improvements
If given more time, the following improvements could be made:

- Implement unit tests for all use cases (currently, only the CreateBlogPost use case has unit tests and all domain entity tests are implemented)
- Add integration/E2E tests for the main use cases
- Implement access control to associate blog posts and comments with specific users, and ensure only the owners can edit them
- Add comprehensive logging throughout the project
- Implement functionality to edit and delete posts and comments

### Conclusion
This project demonstrates a simple but functional blogging platform with basic blog post management and comment counting. Follow the instructions above to set up, run, and test the application. Enjoy using Prosigliere.SimpleBlog!