using FluentAssertions;
using MMP.Core.Domain.Enums;
using MMP.Core.Domain.Models;
using Xunit;

namespace MMP.Core.Domain.Tests;

[Trait("Domain.Models.Operation", "Unit")]
public class OperationShould
{
    [Fact]
    public async Task CreateOperationWhenGivenRequiredParameters()
    {
        //Arrange
        var operationId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        decimal value = 1000;
        var operationType = OperationType.Deposit;
        var operationCategory = OperationCategory.Home;
        var description = "some description";

        //Action
        Operation op = new(operationId, accountId, value, operationType, operationCategory, description);

        //Assert
        op.OperationId.Should().Be(operationId);
        op.AccountId.Should().Be(accountId);
        op.Value.Should().Be(value);
        op.OperationCategory.Should().Be(operationCategory);
        op.OperationType.Should().Be(operationType);
        op.Description.Should().Be(description);
        op.ExternalId.Should().BeNull();
    }

    [Fact]
    public async Task CreateOperationWhenGivenAllParameters()
    {
        //Arrange
        var operationId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var externalId = Guid.NewGuid();
        decimal value = 1000;
        var operationType = OperationType.Deposit;
        var operationCategory = OperationCategory.Home;
        var description = "some description";

        //Action
        Operation op = new(operationId, accountId, value, operationType, operationCategory, description, externalId);

        //Assert
        op.OperationId.Should().Be(operationId);
        op.AccountId.Should().Be(accountId);
        op.Value.Should().Be(value);
        op.OperationCategory.Should().Be(operationCategory);
        op.OperationType.Should().Be(operationType);
        op.Description.Should().Be(description);
        op.ExternalId.Should().NotBeNull().And.Be(externalId);
    }

    [Fact]
    public async Task ChangeDescriptionWhenGivenValidDescription()
    {
        //Arrange
        var op = Setup();
        var description = "new description";

        //Action
        op.ChangeDescription(description);

        //Assert
        op.Description.Should().Be(description);
    }

    [Fact]
    public async Task ThrowExceptionWhenGivenEmptyDescription()
    {
        //Arrange
        var op = Setup();
        op.ChangeOperationCategory(OperationCategory.None);

        //Action
        var action = () => op.ChangeDescription("");

        //Assert
        action.Should().Throw<Exception>().WithMessage("Description can not be empty when category is none");
    }

    [Fact]
    public async Task ThrowExceptionWhenGivenOperationCategoryNone()
    {
        //Arrange
        var op = Setup();
        op.ChangeDescription("");

        //Action
        var action = () => op.ChangeOperationCategory(OperationCategory.None);

        //Assert
        action.Should().Throw<Exception>().WithMessage("Operation category can not be none when description is empty");
    }

    [Fact]
    public async Task ThrowExceptionWhenGivenOperationTypeInvestment()
    {
        //Arrange
        var op = Setup();

        //Action
        var action = () => op.ChangeOperationType(OperationType.Investment);

        //Assert
        action.Should().Throw<Exception>().WithMessage("Operation category must be a Investment");
    }

    [Fact]
    public async Task ChangeOperationValueToNegativeWhenGivenOperationTypeIsNotDeposit()
    {
        //Arrange
        var op = Setup();
        op.ChangeOperationType(OperationType.Withdraw);
        
        //Action
        op.ChangeOperationValue(200);

        //Assert
        op.Value.Should().Be(-200);
    }
    
    [Fact]
    public async Task ChangeOperationValueWhenOperationTypeIsDeposit()
    {
        //Arrange
        var op = Setup();
        
        //Action
        op.ChangeOperationValue(200);

        //Assert
        op.Value.Should().Be(200);
    }

    private Operation Setup()
    {
        var operationId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var externalId = Guid.NewGuid();
        decimal value = 1000;
        var operationType = OperationType.Deposit;
        var operationCategory = OperationCategory.Home;
        var description = "some description";

        return new(operationId, accountId, value, operationType, operationCategory, description, externalId);
    }
}