using HouseholdExpenseManagement.Application.Commands.Person.Create;
using HouseholdExpenseManagement.Application.Commands.Person.Delete;
using HouseholdExpenseManagement.Application.Commands.Person.Update;
using HouseholdExpenseManagement.Application.Models.Person;
using HouseholdExpenseManagement.Application.Queries.Person.GetAll;
using HouseholdExpenseManagement.Application.Queries.Person.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HouseholdExpenseManagement.Api.Controllers
{
    /// <summary>
    /// Gerenciamento de pessoas vinculadas às despesas domésticas.
    /// </summary>
    [ApiController]
    [Route("api/persons")]
    [Produces("application/json")]
    public class PersonController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retorna todas as pessoas cadastradas.
        /// </summary>
        /// <returns>Lista de pessoas.</returns>
        /// <response code="200">Lista retornada com sucesso.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PersonViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new PersonGetAllQueryRequest());
            return Ok(result);
        }

        /// <summary>
        /// Busca uma pessoa pelo seu ID.
        /// </summary>
        /// <param name="id">ID (GUID) da pessoa.</param>
        /// <returns>Dados da pessoa encontrada.</returns>
        /// <response code="200">Pessoa encontrada.</response>
        /// <response code="404">Pessoa não encontrada.</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(PersonViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new PersonGetByIdQueryRequest(id));
            if (result is null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Cria uma nova pessoa.
        /// </summary>
        /// <param name="request">Dados da pessoa a ser criada.</param>
        /// <returns>ID da pessoa criada.</returns>
        /// <response code="201">Pessoa criada com sucesso.</response>
        /// <response code="400">Dados inválidos ou regra de negócio violada.</response>
        [HttpPost]
        [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] PersonModel request)
        {
            var result = await _mediator.Send(new PersonCreateCommandRequest(request));
            if (!result.IsSuccess)
                return BadRequest(result.Error);

            var id = result.Data;
            return CreatedAtAction(nameof(Create), new { id }, new { id });
        }

        /// <summary>
        /// Atualiza os dados de uma pessoa existente.
        /// </summary>
        /// <param name="id">ID (GUID) da pessoa a ser atualizada.</param>
        /// <param name="request">Novos dados da pessoa.</param>
        /// <response code="204">Atualização realizada com sucesso.</response>
        /// <response code="400">Dados inválidos ou regra de negócio violada.</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid id, [FromBody] PersonModel request)
        {
            var result = await _mediator.Send(new PersonUpdateCommandRequest(id, request));

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return NoContent();
        }

        /// <summary>
        /// Remove uma pessoa pelo seu ID.
        /// </summary>
        /// <param name="id">ID (GUID) da pessoa a ser removida.</param>
        /// <response code="204">Pessoa removida com sucesso.</response>
        /// <response code="400">Não foi possível remover a pessoa.</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new PersonDeleteCommandRequest(id));

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return NoContent();
        }
    }
}