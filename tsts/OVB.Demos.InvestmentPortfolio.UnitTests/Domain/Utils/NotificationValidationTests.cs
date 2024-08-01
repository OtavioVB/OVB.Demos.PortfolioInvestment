using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Enumerators;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Domain.Utils;

public sealed class NotificationValidationTests
{
    [Theory]
    [InlineData("NOTIFICATION_1", "Mensagem da Notificação", TypeNotification.Success)]
    [InlineData("INF_34KJSADGY", "Mensagem da Notificação", TypeNotification.Information)]
    [InlineData("FAIL048KJ4", "Falha", TypeNotification.Failure)]
    public void Notification_Should_Be_Valid_As_Expected(string code, string message, TypeNotification type)
    {
        // Arrange

        // Act
        var notification = Notification.Factory(
            code: code,
            message: message,
            type: type);

        // Assert
        Assert.Equal(code, notification.Code);
        Assert.Equal(message, notification.Message);
        Assert.Equal(type.ToString(), notification.Type);
    }

    [Theory]
    [InlineData("", "  ", TypeNotification.Information)]
    [InlineData("", "", TypeNotification.Information)]
    [InlineData("    ", "", TypeNotification.Information)]
    public void Notification_Should_Be_Throw_Exception_When_Code_Or_Message_Is_Null(string code, string message, TypeNotification type)
    {
        // Arrange

        // Act

        // Assert
        Assert.Throws<ArgumentNullException>(() => Notification.Factory(code, message, type));
    }

    [Theory]
    [InlineData("NOTIFICATION_1", "Mensagem da Notificação", (TypeNotification)0)]
    [InlineData("INF_34KJSADGY", "Mensagem da Notificação", (TypeNotification)0)]
    [InlineData("FAIL048KJ4", "Falha", (TypeNotification)0)]
    public void Notification_Should_Be_Throw_Exception_When_Type_Is_Not_Defined(string code, string message, TypeNotification type)
    {
        // Arrange

        // Act

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => Notification.Factory(code, message, type));
    }

    [Theory]
    [InlineData("NOTIFICATION_1", "Mensagem da Notificação")]
    public void Notification_Should_Be_Create_Success_Notification_As_Expected(string code, string message)
    {
        // Arrange
        const TypeNotification EXPECTED_TYPE_NOTIFICATION = TypeNotification.Success;

        // Act
        var notification = Notification.FactorySuccess(code, message);

        // Assert
        Assert.Equal(code, notification.Code);
        Assert.Equal(message, notification.Message);
        Assert.Equal(EXPECTED_TYPE_NOTIFICATION.ToString(), notification.Type);
    }

    [Theory]
    [InlineData("NOTIFICATION_1", "Mensagem da Notificação")]
    public void Notification_Should_Be_Create_Information_Notification_As_Expected(string code, string message)
    {
        // Arrange
        const TypeNotification EXPECTED_TYPE_NOTIFICATION = TypeNotification.Information;

        // Act
        var notification = Notification.FactoryInformation(code, message);

        // Assert
        Assert.Equal(code, notification.Code);
        Assert.Equal(message, notification.Message);
        Assert.Equal(EXPECTED_TYPE_NOTIFICATION.ToString(), notification.Type);
    }

    [Theory]
    [InlineData("NOTIFICATION_1", "Mensagem da Notificação")]
    public void Notification_Should_Be_Create_Failure_Notification_As_Expected(string code, string message)
    {
        // Arrange
        const TypeNotification EXPECTED_TYPE_NOTIFICATION = TypeNotification.Failure;

        // Act
        var notification = Notification.FactoryFailure(code, message);

        // Assert
        Assert.Equal(code, notification.Code);
        Assert.Equal(message, notification.Message);
        Assert.Equal(EXPECTED_TYPE_NOTIFICATION.ToString(), notification.Type);
    }
}
