using Jgcarmona.Qna.Infrastructure.Services;

namespace Jgcarmona.Qna.Infrastructure.Tests.Services
{
    public class PasswordHasherTests
    {
        [Fact]
        public void Hash_ShouldReturnNonNullNonEmptyString()
        {
            // Arrange
            var passwordHasher = new PasswordHasher();
            var password = "password123";

            // Act
            var hashedPassword = passwordHasher.Hash(password);

            // Assert
            Assert.False(string.IsNullOrEmpty(hashedPassword));
        }

        [Fact]
        public void Verify_ShouldReturnTrueForCorrectPassword()
        {
            // Arrange
            var passwordHasher = new PasswordHasher();
            var password = "password123";
            var hashedPassword = passwordHasher.Hash(password);

            // Act
            var result = passwordHasher.Verify(hashedPassword, password);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Verify_ShouldReturnFalseForIncorrectPassword()
        {
            // Arrange
            var passwordHasher = new PasswordHasher();
            var password = "password123";
            var wrongPassword = "wrongpassword";
            var hashedPassword = passwordHasher.Hash(password);

            // Act
            var result = passwordHasher.Verify(hashedPassword, wrongPassword);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Verify_ShouldReturnFalseForMalformedHash()
        {
            // Arrange
            var passwordHasher = new PasswordHasher();
            var malformedHash = "malformedhash";
            var password = "password123";

            // Act
            var result = passwordHasher.Verify(malformedHash, password);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Hash_ShouldReturnDifferentHashesForSamePassword()
        {
            // Arrange
            var passwordHasher = new PasswordHasher();
            var password = "password123";

            // Act
            var hashedPassword1 = passwordHasher.Hash(password);
            var hashedPassword2 = passwordHasher.Hash(password);

            // Assert           
            Assert.NotEqual(hashedPassword1, hashedPassword2);
        }

        [Fact]
        public void Hash_ShouldReturnNonNullNonEmptyStringForEmptyPassword()
        {
            // Arrange
            var passwordHasher = new PasswordHasher();
            var password = "";

            // Act
            var hashedPassword = passwordHasher.Hash(password);

            // Assert
            Assert.False(string.IsNullOrEmpty(hashedPassword));
        }

        [Fact]
        public void Hash_ShouldThrowArgumentNullExceptionForNullPassword()
        {
            // Arrange
            var passwordHasher = new PasswordHasher();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => passwordHasher.Hash(null));
        }

        [Fact]
        public void Verify_ShouldReturnFalseForHashesOfDifferentLengths()
        {
            // Arrange
            var passwordHasher = new PasswordHasher();
            var shortHash = "1a2b3c4d5e6f7g8h9i0j=12345678";
            var password = "password123";

            // Act
            var result = passwordHasher.Verify(shortHash, password);

            // Assert
            Assert.False(result);
        }
    }
}
