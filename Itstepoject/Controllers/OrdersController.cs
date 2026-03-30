using Itstepoject.Models;
using Itstepoject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Itstepoject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        private string GetUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new InvalidOperationException("Authenticated user id is missing.");
            }

            return userId;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetAll(CancellationToken cancellationToken)
        {
            var userId = GetUserId();
            return Ok(await _orderService.GetAllAsync(userId, cancellationToken));
        }

        [HttpGet("cart")]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetCart(CancellationToken cancellationToken)
        {
            var userId = GetUserId();
            return Ok(await _orderService.GetCartAsync(userId, cancellationToken));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Order>> GetById(int id, CancellationToken cancellationToken)
        {
            var userId = GetUserId();
            var order = await _orderService.GetByIdAsync(userId, id, cancellationToken);
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

            var userId = GetUserId();
            var created = await _orderService.CreateAsync(userId, order, cancellationToken);
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

            var userId = GetUserId();
            var updated = await _orderService.UpdateAsync(userId, order, cancellationToken);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("cart/{id:int}")]
        public async Task<IActionResult> RemoveFromCart(int id, CancellationToken cancellationToken)
        {
            var userId = GetUserId();
            var result = await _orderService.RemoveFromCartAsync(userId, id, cancellationToken);
            return result switch
            {
                RemoveFromCartResult.Deleted => NoContent(),
                RemoveFromCartResult.NotInCart => Conflict("Order is not in cart."),
                _ => NotFound()
            };
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var userId = GetUserId();
            var deleted = await _orderService.DeleteAsync(userId, id, cancellationToken);
            return deleted ? NoContent() : NotFound();
        }
    }
}
