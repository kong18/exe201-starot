using Exe.Starot.Application.Feedback.Create;
using Exe.Starot.Application.Feedback.Filter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Exe.Starot.Api.Controllers
{
    [ApiController]
    [Route("api/v1/feedbacks")]
    public class FeedbackController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FeedbackController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/v1/feedbacks
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateFeedback([FromBody] CreateFeedbackCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(CreateFeedback), new { message = result });
        }

        // GET: api/v1/feedbacks/filter
        [HttpGet("filter")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> FilterFeedbacks([FromQuery] FilterFeedbackQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
    }
}
