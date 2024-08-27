namespace Jgcarmona.Qna.Application.Features.Users.Models;


public class SignupModel
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Role { get; set; } = "Author";
}

