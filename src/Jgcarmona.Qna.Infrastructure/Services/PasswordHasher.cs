using Jgcarmona.Qna.Domain.Services;
using System.Security.Cryptography;

namespace Jgcarmona.Qna.Infrastructure.Services
{
    /// <summary>
    /// Provides an implementation of <see cref="IPasswordHasher"/> using PBKDF2 with SHA512 hashing.
    /// </summary>
    public sealed class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 100000;
        private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;

        /// <summary>
        /// Hashes the provided plain text password using PBKDF2 and returns the hash along with the salt.
        /// </summary>
        /// <param name="password">The plain text password to hash.</param>
        /// <returns>A string in the format {hash}={salt}, where the hash and salt are in hexadecimal format.</returns>
        public string Hash(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);
            return $"{Convert.ToHexString(hash)}={Convert.ToHexString(salt)}";
        }

        /// <summary>
        /// Verifies if the provided plain text password matches the given hashed password.
        /// </summary>
        /// <param name="hashedPassword">The previously hashed password stored in the format {hash}={salt}.</param>
        /// <param name="providedPassword">The plain text password provided by the user.</param>
        /// <returns>True if the provided password matches the hashed password; otherwise, false.</returns>
        public bool Verify(string hashedPassword, string providedPassword)
        {
            var parts = hashedPassword.Split('=');
            if (parts.Length != 2)
                return false;

            try
            {
                byte[] hash = Convert.FromHexString(parts[0]);
                byte[] salt = Convert.FromHexString(parts[1]);

                if (hash.Length != HashSize || salt.Length != SaltSize)
                {
                    return false;
                }

                byte[] providedHash = Rfc2898DeriveBytes.Pbkdf2(providedPassword, salt, Iterations, Algorithm, HashSize);
                return CryptographicOperations.FixedTimeEquals(providedHash, hash);
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}