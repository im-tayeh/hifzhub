using FluentAssertions;
using HifzHub.Infrastructure.Security;

namespace HifzHub.UnitTests;

public class PasswordHasherTests
{
    [Fact]
    public void Hashed_password_should_verify_correctly()
    {
        var hasher = new PasswordHasher();
        var password = "MyPassword123";


        var hash = hasher.Hash(password);
        var isValid = hasher.Verify(password, hash);
        var isWrongValid = hasher.Verify("WrongPassword", hash);

        hash.Should().NotBe(password);
        isValid.Should().BeTrue();
        isWrongValid.Should().BeFalse();
    }
}