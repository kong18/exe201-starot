using Exe.Starot.Application.FavoriteProduct;
using Exe.Starot.Application.FavoriteProduct.Create;
using Exe.Starot.Application.FavoriteProduct.Delete;
using Exe.Starot.Application.FavoriteProduct.Get;
using Exe.Starot.Domain.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Exe.Starot.Api.Controllers
{
    [ApiController]
    [Route("api/v1/favorite-products")]
    public class FavoriteProductController : ControllerBase
    {
        private readonly ISender _mediator;

        public FavoriteProductController(ISender mediator)
        {
            _mediator = mediator;
        }

        // POST: api/v1/favorite-products
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddFavoriteProduct(
            [FromBody] AddFavoriteProductCommand command,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _mediator.Send(command, cancellationToken);
                return Ok(new JsonResponse<string>(result));
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

        // DELETE: api/v1/favorite-products/{productId}
        [HttpDelete("{productId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> RemoveFavoriteProduct(
            [FromRoute] string productId,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var command = new DeleteFavoriteProductCommand { ProductId = productId };
                var result = await _mediator.Send(command, cancellationToken);
                return Ok(new JsonResponse<string>(result));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new JsonResponse<string>(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new JsonResponse<string>(ex.Message));
            }
        }
        // GET: api/v1/favorite-products/user/{userId}
        [HttpGet("user/{userId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<FavoriteProductDto>>> GetUserFavoriteProducts(
            [FromRoute] string userId,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var query = new GetFavoriteProductsByUserQuery { UserId = userId };
                var result = await _mediator.Send(query, cancellationToken);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new JsonResponse<string>(ex.Message));
            }
        }
    }
}
