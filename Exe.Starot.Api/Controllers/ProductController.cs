using Exe.Starot.Application.Common.Pagination;
using Exe.Starot.Application.Product.Create;
using Exe.Starot.Application.Product.Delete;
using Exe.Starot.Application.Product.Filter;
using Exe.Starot.Application.Product.Update;
using Exe.Starot.Application.Product;
using Exe.Starot.Domain.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Exe.Starot.Api.Controllers
{
    [ApiController]
    [Route("api/v1/products")]
    public class ProductController : ControllerBase
    {
        private readonly ISender _mediator;
        public ProductController(ISender mediator)
        {
            _mediator = mediator;
        }

        // POST: api/v1/products
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateProduct(
            [FromForm] CreateProductCommand command,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _mediator.Send(command, cancellationToken);
                return CreatedAtAction(nameof(CreateProduct), new { id = result }, new JsonResponse<string>(result));
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

        // GET: api/v1/products
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PagedResult<ProductDto>>> FilterProducts(
            [FromQuery] FilterProductQuery query,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpPut]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateProduct(
     [FromForm] UpdateProductCommand command,
     CancellationToken cancellationToken = default)
        {
            try
            {
                // Assuming the id is part of the UpdateProductCommand (sent via form)
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

        // DELETE: api/v1/products/{id}
        [HttpDelete("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteProduct(
            [FromRoute] string id,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var command = new DeleteProductCommand { Id = id };
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
    }
}