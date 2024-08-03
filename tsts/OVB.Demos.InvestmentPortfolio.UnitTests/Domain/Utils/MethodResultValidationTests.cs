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

    [Fact]
    public void Method_Result_Value_Object_Should_Be_Valid_When_Created_By_Anothers_Method_Result_And_Output_Should_Be_Success()
    {
        // Arrange
        INotification[] notifications = { Notification.FactorySuccess(
            code: "SCS9348JH",
            message: "Factory Success") };
        var methodResult = MethodResult<INotification>.FactorySuccess(
            notifications: notifications);
        const string EXPECTED_OUTPUT = "output";

        // Act
        var methodResultIncremental = MethodResult<INotification, string>.Factory(
            output: EXPECTED_OUTPUT,
            methodResult);

        // Assert
        Assert.Equal(EXPECTED_OUTPUT, methodResultIncremental.Output);
        Assert.True(methodResultIncremental.IsSuccess);
        Assert.Equal(notifications, methodResultIncremental.Notifications);
    }

    [Fact]
    public void Method_Result_Value_Object_Should_Be_Valid_When_Created_By_Anothers_Method_Result_And_Output_Should_Be_Error()
    {
        // Arrange
        INotification[] notifications = { Notification.FactorySuccess(
            code: "SCS9348JH",
            message: "Factory Success") };
        INotification[] notifications2 = { Notification.FactoryFailure(
            code: "FAIL9348JH",
            message: "Factory Failure") };
        var methodResult = MethodResult<INotification>.FactorySuccess(
            notifications: notifications);
        var methodResult2 = MethodResult<INotification>.FactoryError(
            notifications: notifications2);

        const string EXPECTED_OUTPUT = "output";

        // Act
        var methodResultIncremental = MethodResult<INotification, string>.Factory(
            output: EXPECTED_OUTPUT,
            methodResult, methodResult2);

        // Assert
        Assert.Equal(EXPECTED_OUTPUT, methodResultIncremental.Output);
        Assert.False(methodResultIncremental.IsSuccess);
        Assert.NotEqual(notifications, methodResultIncremental.Notifications);
    }
}
