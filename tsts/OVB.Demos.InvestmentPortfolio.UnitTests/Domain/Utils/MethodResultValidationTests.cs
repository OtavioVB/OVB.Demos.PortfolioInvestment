using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext.Enumerators;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Domain.Utils;

public sealed class MethodResultValidationTests
{
    [Fact]
    public void Method_Result_Value_Object_Should_Be_Valid()
    {
        // Arrange
        INotification[] notifications = { Notification.FactoryFailure(
            code: "FAIL9348JH",
            message: "Fail Message") };

        // Act
        var methodResult = MethodResult<INotification>.Factory(
            type: MethodResultType.Error,
            notifications: notifications);

        // Assert
        Assert.Equal(notifications, methodResult.Notifications);
        Assert.False(methodResult.IsSuccess);
        Assert.Equal(MethodResultType.Error, methodResult.Type);
    }

    [Fact]
    public void Method_Result_Value_Object_Should_Be_Valid_When_Create_By_Success_Method()
    {
        // Arrange
        INotification[] notifications = { Notification.FactoryFailure(
            code: "FAIL9348JH",
            message: "Fail Message") };

        // Act
        var methodResult = MethodResult<INotification>.FactorySuccess(
            notifications: notifications);

        // Assert
        Assert.Equal(notifications, methodResult.Notifications);
        Assert.True(methodResult.IsSuccess);
        Assert.Equal(MethodResultType.Success, methodResult.Type);
    }

    [Fact]
    public void Method_Result_Value_Object_Should_Be_Valid_When_Create_By_Error_Method()
    {
        // Arrange
        INotification[] notifications = { Notification.FactoryFailure(
            code: "FAIL9348JH",
            message: "Fail Message") };

        // Act
        var methodResult = MethodResult<INotification>.FactoryError(
            notifications: notifications);

        // Assert
        Assert.Equal(notifications, methodResult.Notifications);
        Assert.True(methodResult.IsError);
        Assert.Equal(MethodResultType.Error, methodResult.Type);
    }
}
