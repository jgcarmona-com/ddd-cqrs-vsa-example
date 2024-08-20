using System.Security.Cryptography;
using Jgcarmona.Qna.Domain.Abstract.Services;

namespace Jgcarmona.Qna.Application.Services
{

    public sealed class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 100000;
        private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;

        public string Hash(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);
            return $"{Convert.ToHexString(hash)}={Convert.ToHexString(salt)}";
        }

        public bool Verify(string hashedPassword, string providedPassword)
        {
            var parts = hashedPassword.Split('=');
            if (parts.Length != 2)
                return false;

            byte[] hash = Convert.FromHexString(parts[0]);
            byte[] salt = Convert.FromHexString(parts[1]);

            byte[] providedHash = Rfc2898DeriveBytes.Pbkdf2(providedPassword, salt, Iterations, Algorithm, HashSize);
            return CryptographicOperations.FixedTimeEquals(providedHash, hash);
        }
    }
}