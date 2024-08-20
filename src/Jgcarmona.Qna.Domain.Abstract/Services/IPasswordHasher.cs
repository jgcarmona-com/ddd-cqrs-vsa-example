namespace Jgcarmona.Qna.Domain.Abstract.Services
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string hashedPassword, string providedPassword);
    }
}