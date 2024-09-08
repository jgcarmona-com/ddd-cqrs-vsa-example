namespace Jgcarmona.Qna.Domain.Services
{
    /// <summary>
    /// Defines a contract for hashing and verifying passwords.
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        /// Hashes the provided plain text password and returns a hashed version.
        /// </summary>
        /// <param name="password">The plain text password to hash.</param>
        /// <returns>A string that represents the hashed password, including the salt used in the hashing process.</returns>
        string Hash(string password);

        /// <summary>
        /// Verifies if the provided plain text password matches the hashed password.
        /// </summary>
        /// <param name="hashedPassword">The hashed password to verify against.</param>
        /// <param name="providedPassword">The plain text password to check.</param>
        /// <returns>True if the password matches; otherwise, false.</returns>
        bool Verify(string hashedPassword, string providedPassword);
    }
}
