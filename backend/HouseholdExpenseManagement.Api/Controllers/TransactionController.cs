using HouseholdExpenseManagement.Application.Commands.Transaction.Create;
using HouseholdExpenseManagement.Application.Commands.Transaction.Delete;
using HouseholdExpenseManagement.Application.Models.Transaction;
using HouseholdExpenseManagement.Application.Queries.Transaction.GetAll;
using HouseholdExpenseManagement.Application.Queries.Transaction.GetById;
using HouseholdExpenseManagement.Application.Queries.Transaction.GetByPersonId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HouseholdExpenseManagement.Api.Controllers
{
    /// <summary>
    /// Gerenciamento de transações financeiras domésticas.
    /// </summary>
    [ApiController]
    [Route("api/transactions")]
    [Produces("application/json")]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retorna todas as transações cadastradas.
        /// </summary>
        /// <returns>Lista de transações.</returns>
        /// <response code="200">Lista retornada com sucesso.</response>
        /// <response code="404">Nenhuma transação encontrada.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TransactionViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new TransactionGetAllQueryRequest());
            return Ok(result);
        }

        /// <summary>
        /// Retorna todas as transações de uma pessoa específica.
        /// </summary>
        /// <param name="personId">ID (GUID) da pessoa.</param>
        /// <returns>Lista de transações vinculadas à pessoa.</returns>
        /// <response code="200">Lista retornada com sucesso.</response>
        [HttpGet("person/{personId:guid}")]
        [ProducesResponseType(typeof(IEnumerable<TransactionViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByPerson(Guid personId)
        {
            var result = await _mediator.Send(new TransactionGetByPersonIdQueryRequest(personId));
            return Ok(result);
        }

        /// <summary>
        /// Busca uma transação pelo seu ID.
        /// </summary>
        /// <param name="id">ID (GUID) da transação.</param>
        /// <returns>Dados da transação encontrada.</returns>
        /// <response code="200">Transação encontrada.</response>
        /// <response code="404">Transação não encontrada.</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(TransactionViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new TransactionGetByIdQueryRequest(id));
            if (result is null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Registra uma nova transação financeira.
        /// </summary>
        /// <param name="request">Dados da transação a ser criada.</param>
        /// <returns>ID da transação criada.</returns>
        /// <response code="201">Transação criada com sucesso.</response>
        /// <response code="400">Dados inválidos ou regra de negócio violada.</response>
        [HttpPost]
        [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] TransactionModel request)
        {
            var result = await _mediator.Send(new TransactionCreateCommandRequest(request));
            if (!result.IsSuccess)
                return BadRequest(result.Error);

            var id = result.Data;
            return CreatedAtAction(nameof(Create), new { id }, new { id });
        }

        /// <summary>
        /// Remove uma transação pelo seu ID.
        /// </summary>
        /// <param name="id">ID (GUID) da transação a ser removida.</param>
        /// <response code="204">Transação removida com sucesso.</response>
        /// <response code="404">Transação não encontrada.</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new TransactionDeleteCommandRequest(id));

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return NoContent();
        }
    }
}