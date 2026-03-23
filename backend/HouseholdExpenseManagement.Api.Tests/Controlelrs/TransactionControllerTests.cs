using FluentAssertions;
using HouseholdExpenseManagement.Api.Controllers;
using HouseholdExpenseManagement.Application.Commands.Transaction.Create;
using HouseholdExpenseManagement.Application.Commands.Transaction.Delete;
using HouseholdExpenseManagement.Application.Models.Transaction;
using HouseholdExpenseManagement.Application.Queries.Transaction.GetAll;
using HouseholdExpenseManagement.Application.Queries.Transaction.GetById;
using HouseholdExpenseManagement.Application.Queries.Transaction.GetByPersonId;
using HouseholdExpenseManagement.Application.Results;
using HouseholdExpenseManagement.Domain.AggregatesModel.Transaction;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HouseholdExpenseManagement.Tests.Controllers;

public class TransactionControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly TransactionController _controller;

    public TransactionControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new TransactionController(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetAll_ShouldReturn200_WithTransactionList()
    {
        // Arrange
        var transactions = new List<TransactionViewModel>
        {
            new() { Id = Guid.NewGuid(), Description = "Supermercado", Amount = 350.00m, Type = "Expense" },
            new() { Id = Guid.NewGuid(), Description = "Salário",      Amount = 5000.00m, Type = "Income"  }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<TransactionGetAllQueryRequest>(), default))
            .ReturnsAsync(transactions);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var ok = result.Should().BeOfType<OkObjectResult>().Subject;
        ok.StatusCode.Should().Be(StatusCodes.Status200OK);
        ok.Value.Should().BeEquivalentTo(transactions);
    }

    [Fact]
    public async Task GetAll_ShouldReturn200_WithEmptyList_WhenNoTransactionsExist()
    {
        // Arrange
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<TransactionGetAllQueryRequest>(), default))
            .ReturnsAsync(new List<TransactionViewModel>());

        // Act
        var result = await _controller.GetAll();

        // Assert
        var ok = result.Should().BeOfType<OkObjectResult>().Subject;
        (ok.Value as IEnumerable<TransactionViewModel>).Should().BeEmpty();
    }

    [Fact]
    public async Task GetByPerson_ShouldReturn200_WithTransactionsForPerson()
    {
        // Arrange
        var personId = Guid.NewGuid();
        var transactions = new List<TransactionViewModel>
        {
            new() { Id = Guid.NewGuid(), Description = "Aluguel", Amount = 1500.00m, Type = "Expense", PersonId = personId }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<TransactionGetByPersonIdQueryRequest>(), default))
            .ReturnsAsync(transactions);

        // Act
        var result = await _controller.GetByPerson(personId);

        // Assert
        var ok = result.Should().BeOfType<OkObjectResult>().Subject;
        ok.StatusCode.Should().Be(StatusCodes.Status200OK);
        ok.Value.Should().BeEquivalentTo(transactions);
    }

    [Fact]
    public async Task GetByPerson_ShouldReturn200_WithEmptyList_WhenPersonHasNoTransactions()
    {
        // Arrange
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<TransactionGetByPersonIdQueryRequest>(), default))
            .ReturnsAsync(new List<TransactionViewModel>());

        // Act
        var result = await _controller.GetByPerson(Guid.NewGuid());

        // Assert
        var ok = result.Should().BeOfType<OkObjectResult>().Subject;
        (ok.Value as IEnumerable<TransactionViewModel>).Should().BeEmpty();
    }

    [Fact]
    public async Task GetById_ShouldReturn200_WhenTransactionExists()
    {
        // Arrange
        var id = Guid.NewGuid();
        var transaction = new TransactionViewModel
        {
            Id = id,
            Description = "Supermercado",
            Amount = 350.00m,
            Type = "Expense",
            PersonId = Guid.NewGuid(),
            CategoryId = Guid.NewGuid()
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<TransactionGetByIdQueryRequest>(), default))
            .ReturnsAsync(transaction);

        // Act
        var result = await _controller.GetById(id);

        // Assert
        var ok = result.Should().BeOfType<OkObjectResult>().Subject;
        ok.StatusCode.Should().Be(StatusCodes.Status200OK);
        ok.Value.Should().BeEquivalentTo(transaction);
    }

    [Fact]
    public async Task GetById_ShouldReturn404_WhenTransactionDoesNotExist()
    {
        // Arrange
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<TransactionGetByIdQueryRequest>(), default))
            .ReturnsAsync((TransactionViewModel?)null);

        // Act
        var result = await _controller.GetById(Guid.NewGuid());

        // Assert
        result.Should().BeOfType<NotFoundResult>()
            .Which.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task Create_ShouldReturn201_WhenTransactionIsCreatedSuccessfully()
    {
        // Arrange
        var newId = Guid.NewGuid();
        var request = new TransactionModel
        {
            Description = "Supermercado",
            Amount = 350.00m,
            Type = TransactionType.Expense,
            PersonId = Guid.NewGuid(),
            CategoryId = Guid.NewGuid()
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<TransactionCreateCommandRequest>(), default))
            .ReturnsAsync(Result<Guid>.Ok(newId));

        // Act
        var result = await _controller.Create(request);

        // Assert
        var created = result.Should().BeOfType<CreatedAtActionResult>().Subject;
        created.StatusCode.Should().Be(StatusCodes.Status201Created);
        created.Value.Should().BeEquivalentTo(new { id = newId });
    }

    [Fact]
    public async Task Create_ShouldReturn400_WhenCommandFails()
    {
        // Arrange
        var request = new TransactionModel
        {
            Description = "",
            Amount = 0,
            Type = TransactionType.Expense,
            PersonId = Guid.NewGuid(),
            CategoryId = Guid.NewGuid()
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<TransactionCreateCommandRequest>(), default))
            .ReturnsAsync(Result<Guid>.Fail("Invalid description"));

        // Act
        var result = await _controller.Create(request);

        // Assert
        var badRequest = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        badRequest.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        badRequest.Value.Should().Be("Invalid description");
    }


    [Fact]
    public async Task Delete_ShouldReturn204_WhenDeleteIsSuccessful()
    {
        // Arrange
        var id = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<TransactionDeleteCommandRequest>(), default))
            .ReturnsAsync(Result<bool>.Ok(true));

        // Act
        var result = await _controller.Delete(id);

        // Assert
        result.Should().BeOfType<NoContentResult>()
            .Which.StatusCode.Should().Be(StatusCodes.Status204NoContent);
    }

    [Fact]
    public async Task Delete_ShouldReturn400_WhenCommandFails()
    {
        // Arrange
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<TransactionDeleteCommandRequest>(), default))
            .ReturnsAsync(Result<bool>.Fail("Transaction not found."));

        // Act
        var result = await _controller.Delete(Guid.NewGuid());

        // Assert
        var badRequest = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        badRequest.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        badRequest.Value.Should().Be("Transaction not found.");
    }
}
