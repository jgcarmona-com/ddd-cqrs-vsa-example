namespace Jgcarmona.Qna.Api
{
    public static class Constants
    {
        public const string AppName = "Jgcarmona.Qna.Api";
        public const string Title = "Jgcarmona QnA REST API";
        public const string Description = @"
Welcome to the Jgcarmona Q&A REST API documentation!

This API provides endpoints for managing questions, answers, comments, and user profiles in a Q&A system. You can use this API to create, retrieve, update, and delete records. The main features include:

- **Questions Management**: Create a question, retrieve a list of questions, get details of a specific question by its ID, update questions, and delete questions.
- **Answers Management**: Post an answer to a specific question, retrieve all answers for a given question, get details of a specific answer, update answers, and delete answers.
- **Comments Management**: Add comments to questions and answers, retrieve all comments for a given question or answer, update comments, and delete comments.
- **User Profiles**: Manage user profiles, including viewing, updating, and associating user-generated content like questions, answers, and comments.

### Authentication
This API supports JWT-based authentication. Ensure that your requests include the necessary Authorization header with a valid JWT token.

### Error Handling
The API provides meaningful error messages and HTTP status codes to help you understand what went wrong. Common status codes include:
- **200 OK**: The request was successful.
- **201 Created**: A new resource was successfully created.
- **400 Bad Request**: The request was invalid or cannot be otherwise served.
- **401 Unauthorized**: The request requires user authentication.
- **403 Forbidden**: The server understood the request, but refuses to authorize it.
- **404 Not Found**: The requested resource could not be found.
- **422 Unprocessable Entity**: The request was well-formed but was unable to be followed due to semantic errors, such as validation errors in the request body.
- **500 Internal Server Error**: An error occurred on the server.

Explore the endpoints below to see how you can integrate our API into your application. Happy coding!
";
        public static readonly Dictionary<string, string> Contact = new()
        {
            { "name", "Juan García Carmona" },
            { "url", "https://jgcarmona.com/contact" },
            { "email", "contact@jgcarmona.com" }
        };

        public static readonly Dictionary<string, string> LicenseInfo = new()
        {
            { "name", "MIT License" },
            { "url", "https://opensource.org/licenses/MIT" }
        };

        public static readonly Dictionary<string, string> SwaggerUiParameters = new()
        {
            { "syntaxHighlight.theme", "obsidian" },
            { "docExpansion", "none" }, // Collapse all sections by default
            { "persistAuthorization", "true" } // Preserve authorization across sessions
        };

        public const string SwaggerFaviconUrl = "https://jgcarmona.com/assets/favicon.ico";
    }
}
