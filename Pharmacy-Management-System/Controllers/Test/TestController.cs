using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pharmacy_Management_System.Application.Features.Test.Queries;

namespace Pharmacy_Management_System.Controllers.Test
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TestController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> TestApi(CancellationToken cancellationToken)
        {
            var query = new TestApiQuery();
            var data = await _mediator.Send(query, cancellationToken);
            return Ok(data);
        }
    }
}
