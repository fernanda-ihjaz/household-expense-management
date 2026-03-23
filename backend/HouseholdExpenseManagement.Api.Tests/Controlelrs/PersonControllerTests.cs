using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using HouseholdExpenseManagement.Api.Controllers;
using Xunit;
using HouseholdExpenseManagement.Application.Models.Person;
using HouseholdExpenseManagement.Application.Queries.Person.GetAll;
using HouseholdExpenseManagement.Application.Queries.Person.GetById;
using HouseholdExpenseManagement.Application.Results;
using HouseholdExpenseManagement.Application.Commands.Person.Create;
using HouseholdExpenseManagement.Application.Commands.Person.Update;
using HouseholdExpenseManagement.Application.Commands.Person.Delete;

namespace HouseholdExpenseManagement.Api.Tests.Controlelrs;

public class PersonControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly PersonController _controller;

    public PersonControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new PersonController(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetAll_ShouldReturn200_WithPersonList()
    {
        // Arrange
        var persons = new List<PersonViewModel>
        {
            new() { Id = Guid.NewGuid(), Name = "Fernanda" },
            new() { Id = Guid.NewGuid(), Name = "Raul" }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<PersonGetAllQueryRequest>(), default))
            .ReturnsAsync(persons);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        okResult.Value.Should().BeEquivalentTo(persons);
    }

    [Fact]
    public async Task GetById_ShouldReturn200_WhenPersonExists()
    {
        // Arrange
        var id = Guid.NewGuid();
        var person = new PersonViewModel { Id = id, Name = "Fernanda", Age = 22 };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<PersonGetByIdQueryRequest>(), default))
            .ReturnsAsync(person);

        // Act
        var result = await _controller.GetById(id);

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        okResult.Value.Should().BeEquivalentTo(person);
    }

    [Fact]
    public async Task GetById_ShouldReturn404_WhenPersonDoesNotExist()
    {
        // Arrange
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<PersonGetByIdQueryRequest>(), default))
            .ReturnsAsync((PersonViewModel?)null);

        // Act
        var result = await _controller.GetById(Guid.NewGuid());

        // Assert
        result.Should().BeOfType<NotFoundResult>()
            .Which.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task Create_ShouldReturn201_WhenPersonIsCreatedSuccessfully()
    {
        // Arrange
        var newId = Guid.NewGuid();
        var request = new PersonModel { Name = "Fernanda", Age = 22 };
        var commandResult = Result<Guid>.Ok(newId);

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<PersonCreateCommandRequest>(), default))
            .ReturnsAsync(commandResult);

        // Act
        var result = await _controller.Create(request);

        // Assert
        var createdResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
        createdResult.StatusCode.Should().Be(StatusCodes.Status201Created);
        createdResult.Value.Should().BeEquivalentTo(new { id = newId });
    }

    [Fact]
    public async Task Create_ShouldReturn400_WhenCommandFails()
    {
        // Arrange
        var request = new PersonModel { Name = "" };
        var commandResult = Result<Guid>.Fail("Name is required.");

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<PersonCreateCommandRequest>(), default))
            .ReturnsAsync(commandResult);

        // Act
        var result = await _controller.Create(request);

        // Assert
        var badRequest = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        badRequest.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        badRequest.Value.Should().Be("Name is required.");
    }

    [Fact]
    public async Task Update_ShouldReturn204_WhenUpdateIsSuccessful()
    {
        // Arrange
        var id = Guid.NewGuid();
        var request = new PersonModel { Name = "Fernanda Updated", Age = 22 };
        var commandResult = Result<bool>.Ok(true);

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<PersonUpdateCommandRequest>(), default))
            .ReturnsAsync(commandResult);

        // Act
        var result = await _controller.Update(id, request);

        // Assert
        result.Should().BeOfType<NoContentResult>()
            .Which.StatusCode.Should().Be(StatusCodes.Status204NoContent);
    }

    [Fact]
    public async Task Update_ShouldReturn400_WhenCommandFails()
    {
        // Arrange
        var id = Guid.NewGuid();
        var request = new PersonModel { Name = "", Age = 17 };
        var commandResult = Result<bool>.Fail("Name is required.");

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<PersonUpdateCommandRequest>(), default))
            .ReturnsAsync(commandResult);

        // Act
        var result = await _controller.Update(id, request);

        // Assert
        var badRequest = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        badRequest.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        badRequest.Value.Should().Be("Name is required.");
    }


    [Fact]
    public async Task Delete_ShouldReturn204_WhenDeleteIsSuccessful()
    {
        // Arrange
        var id = Guid.NewGuid();
        var commandResult = Result<bool>.Ok(true);

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<PersonDeleteCommandRequest>(), default))
            .ReturnsAsync(commandResult);

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
        var id = Guid.NewGuid();
        var commandResult = Result<bool>.Fail("Person not found.");

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<PersonDeleteCommandRequest>(), default))
            .ReturnsAsync(commandResult);

        // Act
        var result = await _controller.Delete(id);

        // Assert
        var badRequest = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        badRequest.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        badRequest.Value.Should().Be("Person not found.");
    }
}
