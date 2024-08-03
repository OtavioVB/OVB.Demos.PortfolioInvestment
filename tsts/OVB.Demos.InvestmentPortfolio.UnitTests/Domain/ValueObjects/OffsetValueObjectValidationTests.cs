using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Domain.ValueObjects;

public sealed class OffsetValueObjectValidationTests
{
    [Theory]
    [InlineData(5)]
    [InlineData(25)]
    [InlineData(50)]
    public void Offset_Value_Object_Should_Be_Valid(int offset)
    {
        // Arrange

        // Act
        var offsetValueObject = OffsetValueObject.Factory(offset);

        // Assert
        Assert.True(offsetValueObject.MethodResult.IsSuccess);
        Assert.Empty(offsetValueObject.MethodResult.Notifications);
        Assert.Equal(offset, offsetValueObject.GetOffset());
        Assert.Equal<int>(offset, offsetValueObject);
    }

    [Theory]
    [InlineData(4)]
    [InlineData(51)]
    public void Offset_Value_Object_Should_Be_Not_Valid(int offset)
    {
        // Arrange

        // Act
        var offsetValueObject = OffsetValueObject.Factory(offset);

        // Assert
        Assert.False(offsetValueObject.MethodResult.IsSuccess);
        Assert.NotEmpty(offsetValueObject.MethodResult.Notifications);
        Assert.Throws<BusinessValueObjectException>(() => offsetValueObject.GetOffset());
        Assert.Throws<BusinessValueObjectException>(() => (int)offsetValueObject);
        Assert.False(((MethodResult<INotification>)offsetValueObject).IsSuccess);
    }

    [Theory]
    [InlineData(4)]
    [InlineData(0)]
    [InlineData(-5)]
    [InlineData(-1000)]
    public void Offset_Value_Object_Should_Send_Notification_Error_Messages_When_Offset_Is_Less_Than_The_Minimum_Value(int offset)
    {
        // Arrange
        const int EXPECTED_MINIMUM_VALUE = 5;
        const string EXPECTED_NOTIFICATION_CODE = "OFFSET_VALUE_CANNOT_BE_LESS_THAN_THE_MINIMUM_VALUE_ALLOWED";
        const string EXPECTED_NOTIFICATION_MESSAGE = "O número de itens a ser paginados não pode ser menor que número mínimo de 5 iten(s).";
        const string EXPECTED_NOTIFICATION_TYPE = "Failure";

        // Act
        var offsetValueObject = OffsetValueObject.Factory(offset);

        // Assert
        Assert.Equal(EXPECTED_MINIMUM_VALUE, OffsetValueObject.MIN_OFFSET);
        Assert.Equal(EXPECTED_NOTIFICATION_CODE, offsetValueObject.MethodResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_NOTIFICATION_MESSAGE, offsetValueObject.MethodResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_NOTIFICATION_TYPE, offsetValueObject.MethodResult.Notifications[0].Type);
    }

    [Theory]
    [InlineData(51)]
    [InlineData(52)]
    [InlineData(1408124)]
    [InlineData(55)]
    public void Offset_Value_Object_Should_Send_Notification_Error_Messages_When_Offset_Is_Greater_Than_The_Maximum_Value(int offset)
    {
        // Arrange
        const int EXPECTED_MAXIMUM_VALUE = 50;
        const string EXPECTED_NOTIFICATION_CODE = "OFFSET_VALUE_CANNOT_BE_GREATER_THAN_THE_MAXIMUM_VALUE_ALLOWED";
        const string EXPECTED_NOTIFICATION_MESSAGE = "O número de itens a ser paginados não pode ser maior que número máximo de 50 iten(s).";
        const string EXPECTED_NOTIFICATION_TYPE = "Failure";

        // Act
        var offsetValueObject = OffsetValueObject.Factory(offset);

        // Assert
        Assert.Equal(EXPECTED_MAXIMUM_VALUE, OffsetValueObject.MAX_OFFSET);
        Assert.Equal(EXPECTED_NOTIFICATION_CODE, offsetValueObject.MethodResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_NOTIFICATION_MESSAGE, offsetValueObject.MethodResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_NOTIFICATION_TYPE, offsetValueObject.MethodResult.Notifications[0].Type);
    }
}
