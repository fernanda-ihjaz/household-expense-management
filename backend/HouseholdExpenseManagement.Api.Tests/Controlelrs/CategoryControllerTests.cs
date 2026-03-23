using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using HouseholdExpenseManagement.Api.Controllers;
using HouseholdExpenseManagement.Application.Models.Category;
using Xunit;
using HouseholdExpenseManagement.Application.Queries.Category.GetAll;
using HouseholdExpenseManagement.Application.Results;
using HouseholdExpenseManagement.Application.Commands.Category.Create;

namespace HouseholdExpenseManagement.Api.Tests.Controlelrs
{
    public class CategoryControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly CategoryController _controller;

        public CategoryControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new CategoryController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturn200_WithCategoryList()
        {
            // Arrange
            var categories = new List<CategoryViewModel>
        {
            new() { Id = Guid.NewGuid(), Description = "Alimentação" },
            new() { Id = Guid.NewGuid(), Description = "Transporte" }
        };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CategoryGetAllQueryRequest>(), default))
                .ReturnsAsync(categories);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
            okResult.Value.Should().BeEquivalentTo(categories);
        }

        [Fact]
        public async Task GetAll_ShouldReturn200_WithEmptyList_WhenNoCategoriesExist()
        {
            // Arrange
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CategoryGetAllQueryRequest>(), default))
                .ReturnsAsync(new List<CategoryViewModel>());

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
            (okResult.Value as IEnumerable<CategoryViewModel>).Should().BeEmpty();
        }

        [Fact]
        public async Task Create_ShouldReturn201_WhenCategoryIsCreatedSuccessfully()
        {
            // Arrange
            var newId = Guid.NewGuid();
            var request = new CategoryModel { Description = "Alimentação" };
            var commandResult = Result<Guid>.Ok(newId);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CategoryCreateCommandRequest>(), default))
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
            var request = new CategoryModel { Description = "" };
            var commandResult = Result<Guid>.Fail("Description is required.");

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CategoryCreateCommandRequest>(), default))
                .ReturnsAsync(commandResult);

            // Act
            var result = await _controller.Create(request);

            // Assert
            var badRequest = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badRequest.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            badRequest.Value.Should().Be("Description is required.");
        }
    }
}
