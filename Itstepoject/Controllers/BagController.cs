using Itstepoject.Models;
using Itstepoject.Services;
using Microsoft.AspNetCore.Mvc;

namespace Itstepoject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BagController : ControllerBase
    {
        private readonly IBagService _bagService;

        public BagController(IBagService bagService)
        {
            _bagService = bagService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Bag>>> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await _bagService.GetAllAsync(cancellationToken));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Bag>> GetById(int id, CancellationToken cancellationToken)
        {
            var item = await _bagService.GetByIdAsync(id, cancellationToken);
            return item is null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Bag>> Create([FromBody] Bag item, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            item.Id = 0;
            var created = await _bagService.CreateAsync(item, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Bag item, CancellationToken cancellationToken)
        {
            if (id != item.Id)
            {
                return BadRequest("Route id and body id must match.");
            }

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            await _bagService.UpdateAsync(item, cancellationToken);
            return NoContent();
        }
    }
}
