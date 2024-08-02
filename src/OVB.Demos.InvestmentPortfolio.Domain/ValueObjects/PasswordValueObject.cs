using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;
using System.Security.Cryptography;
using System.Text;

namespace OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

public readonly struct PasswordValueObject
{
    private string? Password { get; }
    private bool IsEncrypted { get; }
    public MethodResult<INotification> MethodResult { get; }

    private PasswordValueObject(string? password, MethodResult<INotification> methodResult, bool isEncrypted = false)
    {
        Password = password;
        MethodResult = methodResult;
        IsEncrypted = isEncrypted;
    }

    private const string PASSWORD_LENGTH_CANNOT_BE_LESS_THAN_THE_MINIMUM_ALLOWED_NOTIFICATION_CODE = "PASSWORD_LENGTH_CANNOT_BE_LESS_THAN_THE_MINIMUM_ALLOWED";
    private static string PASSWORD_LENGTH_CANNOT_BE_LESS_THAN_THE_MINIMUM_ALLOWED_NOTIFICATION_MESSAGE => $"A senha deve conter no mínimo {MIN_LENGTH} caracteres.";

    private const string PASSWORD_LENGTH_CANNOT_BE_GREATER_THAN_THE_MAXIMUM_ALLOWED_NOTIFICATION_CODE = "PASSWORD_LENGTH_CANNOT_BE_GREATER_THAN_THE_MAXIMUM_ALLOWED";
    private static string PASSWORD_LENGTH_CANNOT_BE_GREATER_THAN_THE_MAXIMUM_ALLOWED_NOTIFICATION_MESSAGE => $"A senha deve conter até no máximo {MAX_LENGTH} caracteres.";

    private const string PASSWORD_NEEDS_TO_HAVE_DIGITS_NOTIFICATION_CODE = "PASSWORD_NEEDS_TO_HAVE_DIGITS";
    private const string PASSWORD_NEEDS_TO_HAVE_DIGITS_NOTIFICATION_MESSAGE = "A senha deve conter dígitos numéricos.";

    private const string PASSWORD_NEEDS_TO_HAVE_LETTERS_NOTIFICATION_CODE = "PASSWORD_NEEDS_TO_HAVE_LETTERS";
    private const string PASSWORD_NEEDS_TO_HAVE_LETTERS_NOTIFICATION_MESSAGE = "A senha deve conter letras.";

    private const string PASSWORD_NEEDS_TO_HAVE_SPECIAL_CHARACTERS_NOTIFICATION_CODE = "PASSWORD_NEEDS_TO_HAVE_SPECIAL_CHARACTERS";
    private const string PASSWORD_NEEDS_TO_HAVE_SPECIAL_CHARACTERS_NOTIFICATION_MESSAGE = "A senha deve conter caracteres especiais.";

    public const int MIN_LENGTH = 8;
    public const int MAX_LENGTH = 32;

    public static PasswordValueObject Factory(string password, bool isEncrypted = false)
    {
        if (isEncrypted)
            return new PasswordValueObject(
                password: password,
                methodResult: MethodResult<INotification>.FactorySuccess(),
                isEncrypted: isEncrypted);

        const int CAPACITY_NOTIFICATIONS_POSSIBLE = 5;

        var notifications = new List<INotification>(CAPACITY_NOTIFICATIONS_POSSIBLE);

        if (password.Length < MIN_LENGTH)
            notifications.Add(Notification.FactoryFailure(
                code: PASSWORD_LENGTH_CANNOT_BE_LESS_THAN_THE_MINIMUM_ALLOWED_NOTIFICATION_CODE,
                message: PASSWORD_LENGTH_CANNOT_BE_LESS_THAN_THE_MINIMUM_ALLOWED_NOTIFICATION_MESSAGE));

        if (password.Length > MAX_LENGTH)
            notifications.Add(Notification.FactoryFailure(
                code: PASSWORD_LENGTH_CANNOT_BE_GREATER_THAN_THE_MAXIMUM_ALLOWED_NOTIFICATION_CODE,
                message: PASSWORD_LENGTH_CANNOT_BE_GREATER_THAN_THE_MAXIMUM_ALLOWED_NOTIFICATION_MESSAGE));

        var hasAnyDigit = password.Any(char.IsDigit);

        if (!hasAnyDigit)
            notifications.Add(Notification.FactoryFailure(
                code: PASSWORD_NEEDS_TO_HAVE_DIGITS_NOTIFICATION_CODE,
                message: PASSWORD_NEEDS_TO_HAVE_DIGITS_NOTIFICATION_MESSAGE));

        var hasAnyLetter = password.Any(char.IsLetter);

        if (!hasAnyLetter)
            notifications.Add(Notification.FactoryFailure(
                code: PASSWORD_NEEDS_TO_HAVE_LETTERS_NOTIFICATION_CODE,
                message: PASSWORD_NEEDS_TO_HAVE_LETTERS_NOTIFICATION_MESSAGE));

        var hasAnySpecialLetter = password.Any(p => !char.IsLetterOrDigit(p));

        if (!hasAnySpecialLetter)
            notifications.Add(Notification.FactoryFailure(
                code: PASSWORD_NEEDS_TO_HAVE_SPECIAL_CHARACTERS_NOTIFICATION_CODE,
                message: PASSWORD_NEEDS_TO_HAVE_SPECIAL_CHARACTERS_NOTIFICATION_MESSAGE));

        if (Notification.HasAnyNotifications(notifications))
            return new PasswordValueObject(
                password: null,
                methodResult: MethodResult<INotification>.FactoryError(
                    notifications: notifications.ToArray()));

        return new PasswordValueObject(
            password: password,
            methodResult: MethodResult<INotification>.FactorySuccess());
    }

    private string GetPassword()
    {
        BusinessValueObjectException.ThrowExceptionMethodResultIsError(MethodResult);

        return BusinessValueObjectException.ThrowExceptionIfTheObjectCannotBeNull(Password);
    }

    public (string PasswordHash, string Salt) GetPasswordHashAndSalt(string privateKey, string? alreadyDefinedSalt = null)
    {
        var salt = alreadyDefinedSalt ?? GenerateSalt();
        var password = GetPassword();
        var key = GetPrivateKey(privateKey);

        var passwordWithSalt = $"{salt}-{password}";

        using var encrypter = new HMACSHA256(key);
        var computedHash = encrypter.ComputeHash(Encoding.UTF8.GetBytes(passwordWithSalt));

        int INITIAL_LENGTH_STRING_BUILDER = computedHash.Length * 2;

        const string HEXADECIMAL_APPEND_FORMAT_MODE = "{0:x2}";

        var stringBuilder = new StringBuilder(INITIAL_LENGTH_STRING_BUILDER);
        foreach (var byteAssociated in computedHash)
            stringBuilder.AppendFormat(HEXADECIMAL_APPEND_FORMAT_MODE, byteAssociated);

        return (stringBuilder.ToString(), salt);
    }

    private static byte[] GetPrivateKey(string privateKey)
        => Convert.FromBase64String(privateKey);

    private static string GenerateSalt()
    {
        const int SALT_LENGTH = 16;

        byte[] salt = new byte[SALT_LENGTH];

        RandomNumberGenerator.Fill(salt);

        return Convert.ToBase64String(salt);
    }

    public static implicit operator MethodResult<INotification>(PasswordValueObject obj)
        => obj.MethodResult;
}
