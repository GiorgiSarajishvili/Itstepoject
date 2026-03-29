using Itstepoject.Models;
using Itstepoject.Services;
using Microsoft.AspNetCore.Mvc;

namespace Itstepoject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await _orderService.GetAllAsync(cancellationToken));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Order>> GetById(int id, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetByIdAsync(id, cancellationToken);
            return order is null ? NotFound() : Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> Create([FromBody] Order order, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            order.Id = 0;
            order.OrderDate = order.OrderDate == default ? DateTime.UtcNow : order.OrderDate;
            var created = await _orderService.CreateAsync(order, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Order order, CancellationToken cancellationToken)
        {
            if (id != order.Id)
            {
                return BadRequest("Route id and body id must match.");
            }

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            await _orderService.UpdateAsync(order, cancellationToken);
            return NoContent();
        }
    }
}
