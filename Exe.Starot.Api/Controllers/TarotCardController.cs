using Exe.Starot.Application.Common.Pagination;
using Exe.Starot.Application.TarotCard;
using Exe.Starot.Application.TarotCard.Create;
using Exe.Starot.Application.TarotCard.Delete;
using Exe.Starot.Application.TarotCard.Filter;
using Exe.Starot.Application.TarotCard.Get;
using Exe.Starot.Application.TarotCard.Update;
using Exe.Starot.Domain.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Exe.Starot.Api.Controllers
{
    [ApiController]
    [Route("api/v1/tarotcards")]
    public class TarotCardController : ControllerBase
    {

        private readonly ISender _mediator;
        public TarotCardController(ISender mediator)
        {
            _mediator = mediator;

        }

        [HttpPost]
      
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateTarotCard(
              [FromForm] CreateTarotCardCommand command,
            CancellationToken cancellationToken = default)
        {
            // Send command to the Mediator
            try
            {
                var result = await _mediator.Send(command, cancellationToken);

                // Return 201 Created, with the location of the new Tarot card
                return CreatedAtAction(nameof(CreateTarotCard), new { id = result }, new JsonResponse<string>(result));
            }
            catch (DuplicationException ex)
            {
                // If there's a validation error (e.g., duplicate card name), return 400 Bad Request
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
            catch (Exception ex)
            {
                // Return 500 Internal Server Error if something unexpected happens
                return StatusCode(StatusCodes.Status500InternalServerError, new JsonResponse<string>(ex.Message));
            }
        }
        // GET: api/v1/tarotcards/random
        [HttpGet("random")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TarotCardDto>> GetRandomTarotCard(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _mediator.Send(new GetRandomTarotCardQuery(), cancellationToken);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new JsonResponse<string>(ex.Message));
            }
        }

        // GET: api/v1/tarotcards
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PagedResult<TarotCardDto>>> FilterTarotCards(
            [FromQuery] FilterTarotCardQuery query,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        // PUT: api/v1/tarotcards
        [HttpPut]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateTarotCard(
            [FromBody] UpdateCardCommand command,
            CancellationToken cancellationToken = default)
        {
            // Ensure that the ID is present in the command
            if (command.Id <= 0) // or any other appropriate validation for your ID
            {
                return BadRequest(new JsonResponse<string>("Invalid ID provided"));
            }

            try
            {
                // Send the command to the mediator to handle the update
                var result = await _mediator.Send(command, cancellationToken);
                return Ok(new JsonResponse<string>(result));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new JsonResponse<string>(ex.Message));
            }
            catch (DuplicationException ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new JsonResponse<string>(ex.Message));
            }
        }
        // DELETE: api/v1/tarotcards/{id}
        [HttpDelete("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteTarotCard(
            [FromRoute] int id,
            CancellationToken cancellationToken = default)
        {
            try
            {
                // Create the command with the ID to delete the tarot card
                var command = new DeleteTarotCardCommand { Id = id };

                // Send the command to the Mediator
                var result = await _mediator.Send(command, cancellationToken);

                // Return 200 OK with success message
                return Ok(new JsonResponse<string>(result));
            }
            catch (NotFoundException ex)
            {
                // If tarot card is not found, return 404 Not Found
                return NotFound(new JsonResponse<string>(ex.Message));
            }
            catch (Exception ex)
            {
                // Return 500 Internal Server Error if something unexpected happens
                return StatusCode(StatusCodes.Status500InternalServerError, new JsonResponse<string>(ex.Message));
            }
        }
    }
}
