using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using System.Security.Cryptography;
using System.Text;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Domain.ValueObjects;

public sealed class PasswordValueObjectValidationTests
{
    private const string PRIVATE_KEY = "OGIxMmM1NmFiY2I2NDA2YTkwMGUyNGZiZDU4NGNlZjA=";

    [Theory]
    [InlineData("DJJ54H*LJ#@0", false)]
    [InlineData("_836hjfk7DH8!", false)]
    [InlineData("f9964c14d7d9a76a917a2d564c7a45f2280eb6c9945ff9af86808da0573af170", true)]
    public void Password_Value_Object_Should_Be_Valid(string password, bool isEncypted)
    {
        // Arrange
        const int SALT_LENGTH = 16;

        byte[] salt = new byte[SALT_LENGTH];

        RandomNumberGenerator.Fill(salt);

        var alreadyHasSalt = Convert.ToBase64String(salt);

        var passwordWithSalt = $"{alreadyHasSalt}-{password}";

        using var encrypter = new HMACSHA256(Convert.FromBase64String(PRIVATE_KEY));
        var computedHash = encrypter.ComputeHash(Encoding.UTF8.GetBytes(passwordWithSalt));

        int INITIAL_LENGTH_STRING_BUILDER = computedHash.Length * 2;

        const string HEXADECIMAL_APPEND_FORMAT_MODE = "{0:x2}";

        var stringBuilder = new StringBuilder(INITIAL_LENGTH_STRING_BUILDER);
        foreach (var byteAssociated in computedHash)
            stringBuilder.AppendFormat(HEXADECIMAL_APPEND_FORMAT_MODE, byteAssociated);

        // Act
        var passwordValueObject = PasswordValueObject.Factory(password, isEncrypted: isEncypted);
        var hash = passwordValueObject.GetPasswordHashAndSalt(PRIVATE_KEY, alreadyHasSalt);

        // Assert
        Assert.True(passwordValueObject.MethodResult.IsSuccess);
        Assert.Equal(stringBuilder.ToString(), hash.PasswordHash);
        Assert.Equal(alreadyHasSalt, hash.Salt);
    }

    [Theory]
    [InlineData("8456453*#$%")]
    [InlineData("800985%$@#$")]
    public void Password_Value_Object_Should_Be_Send_Notification_Errors_When_The_Password_Doesnt_Has_Letters(string password)
    {
        // Arrange
        const string EXPECTED_CODE = "PASSWORD_NEEDS_TO_HAVE_LETTERS";
        const string EXPECTED_MESSAGE = "A senha deve conter letras.";
        const string EXPECTED_TYPE = "Failure";

        // Act
        var passwordValueObject = PasswordValueObject.Factory(password);

        // Assert
        Assert.False(passwordValueObject.MethodResult.IsSuccess);
        Assert.Single(passwordValueObject.MethodResult.Notifications);
        Assert.Equal(EXPECTED_CODE, passwordValueObject.MethodResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_MESSAGE, passwordValueObject.MethodResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_TYPE, passwordValueObject.MethodResult.Notifications[0].Type);
    }

    [Theory]
    [InlineData("JAFUDHUFA*#$%")]
    [InlineData("LAOIFXHAKMF%$@#$")]
    public void Password_Value_Object_Should_Be_Send_Notification_Errors_When_The_Password_Doesnt_Has_Digits(string password)
    {
        // Arrange
        const string EXPECTED_CODE = "PASSWORD_NEEDS_TO_HAVE_DIGITS";
        const string EXPECTED_MESSAGE = "A senha deve conter dígitos numéricos.";
        const string EXPECTED_TYPE = "Failure";

        // Act
        var passwordValueObject = PasswordValueObject.Factory(password);

        // Assert
        Assert.False(passwordValueObject.MethodResult.IsSuccess);
        Assert.Single(passwordValueObject.MethodResult.Notifications);
        Assert.Equal(EXPECTED_CODE, passwordValueObject.MethodResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_MESSAGE, passwordValueObject.MethodResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_TYPE, passwordValueObject.MethodResult.Notifications[0].Type);
    }

    [Theory]
    [InlineData("00K3KJHBGDXÇ482095J")]
    [InlineData("LAJN87N4JJ236OLLKJ")]
    public void Password_Value_Object_Should_Be_Send_Notification_Errors_When_The_Password_Doesnt_Has_Special_Characters(string password)
    {
        // Arrange
        const string EXPECTED_CODE = "PASSWORD_NEEDS_TO_HAVE_SPECIAL_CHARACTERS";
        const string EXPECTED_MESSAGE = "A senha deve conter caracteres especiais.";
        const string EXPECTED_TYPE = "Failure";

        // Act
        var passwordValueObject = PasswordValueObject.Factory(password);

        // Assert
        Assert.False(passwordValueObject.MethodResult.IsSuccess);
        Assert.Single(passwordValueObject.MethodResult.Notifications);
        Assert.Equal(EXPECTED_CODE, passwordValueObject.MethodResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_MESSAGE, passwordValueObject.MethodResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_TYPE, passwordValueObject.MethodResult.Notifications[0].Type);
    }

    [Theory]
    [InlineData("Lj*5j")]
    [InlineData("La$9*")]
    public void Password_Value_Object_Should_Be_Send_Notification_Errors_When_The_Password_Length_Is_Less_Than_The_Minimum_Allowed(string password)
    {
        // Arrange
        const int EXPECTED_MIN_LENGTH_ALLOWED = 8;

        const string EXPECTED_CODE = "PASSWORD_LENGTH_CANNOT_BE_LESS_THAN_THE_MINIMUM_ALLOWED";
        string EXPECTED_MESSAGE = $"A senha deve conter no mínimo {EXPECTED_MIN_LENGTH_ALLOWED} caracteres.";
        const string EXPECTED_TYPE = "Failure";

        // Act
        var passwordValueObject = PasswordValueObject.Factory(password);

        // Assert
        Assert.False(passwordValueObject.MethodResult.IsSuccess);
        Assert.Equal(EXPECTED_MIN_LENGTH_ALLOWED, PasswordValueObject.MIN_LENGTH);
        Assert.Single(passwordValueObject.MethodResult.Notifications);
        Assert.Equal(EXPECTED_CODE, passwordValueObject.MethodResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_MESSAGE, passwordValueObject.MethodResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_TYPE, passwordValueObject.MethodResult.Notifications[0].Type);
    }

    [Theory]
    [InlineData("DJJ54H*LJ#@0DJJ54H*LJ#@0DJJ54H*LJ#@0DJJ54H*LJ#@0DJJ54H*LJ#@0")]
    [InlineData("_836hjfk7DH8!845J43%$#jsd7324JKFAS$%JJUDFHSYAY4O5KFDa|34AH7")]
    public void Password_Value_Object_Should_Be_Send_Notification_Errors_When_The_Password_Length_Is_Greater_Than_The_Maximum_Allowed(string password)
    {
        // Arrange
        const int EXPECTED_MAX_LENGTH_ALLOWED = 32;

        const string EXPECTED_CODE = "PASSWORD_LENGTH_CANNOT_BE_GREATER_THAN_THE_MAXIMUM_ALLOWED";
        string EXPECTED_MESSAGE = $"A senha deve conter até no máximo {EXPECTED_MAX_LENGTH_ALLOWED} caracteres.";
        const string EXPECTED_TYPE = "Failure";

        // Act
        var passwordValueObject = PasswordValueObject.Factory(password);

        // Assert
        Assert.False(passwordValueObject.MethodResult.IsSuccess);
        Assert.Equal(EXPECTED_MAX_LENGTH_ALLOWED, PasswordValueObject.MAX_LENGTH);
        Assert.Single(passwordValueObject.MethodResult.Notifications);
        Assert.Equal(EXPECTED_CODE, passwordValueObject.MethodResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_MESSAGE, passwordValueObject.MethodResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_TYPE, passwordValueObject.MethodResult.Notifications[0].Type);
    }
}
