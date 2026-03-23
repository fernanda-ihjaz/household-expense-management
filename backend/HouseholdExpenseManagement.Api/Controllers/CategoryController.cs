using HouseholdExpenseManagement.Application.Commands.Category.Create;
using HouseholdExpenseManagement.Application.Models.Category;
using HouseholdExpenseManagement.Application.Queries.Category.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HouseholdExpenseManagement.Api.Controllers
{
    /// <summary>
    /// Gerenciamento de categorias de despesas.
    /// </summary>
    [ApiController]
    [Route("api/categories")]
    [Produces("application/json")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retorna todas as categorias cadastradas.
        /// </summary>
        /// <returns>Lista de categorias.</returns>
        /// <response code="200">Lista retornada com sucesso.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoryViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new CategoryGetAllQueryRequest());
            return Ok(result);
        }

        /// <summary>
        /// Cria uma nova categoria.
        /// </summary>
        /// <param name="request">Dados da categoria a ser criada.</param>
        /// <returns>ID da categoria criada.</returns>
        /// <response code="201">Categoria criada com sucesso.</response>
        /// <response code="400">Dados inválidos ou regra de negócio violada.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CategoryModel request)
        {
            var result = await _mediator.Send(new CategoryCreateCommandRequest(request));

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            var id = result.Data;
            return CreatedAtAction(nameof(Create), new { id }, new { id });
        }
    }
}