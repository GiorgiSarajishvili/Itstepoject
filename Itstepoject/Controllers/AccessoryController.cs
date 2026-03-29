using Itstepoject.Models;
using Itstepoject.Services;
using Microsoft.AspNetCore.Mvc;

namespace Itstepoject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccessoryController : ControllerBase
    {
        private readonly IAccessoryService _accessoryService;

        public AccessoryController(IAccessoryService accessoryService)
        {
            _accessoryService = accessoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Accessory>>> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await _accessoryService.GetAllAsync(cancellationToken));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Accessory>> GetById(int id, CancellationToken cancellationToken)
        {
            var item = await _accessoryService.GetByIdAsync(id, cancellationToken);
            return item is null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Accessory>> Create([FromBody] Accessory item, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            item.Id = 0;
            var created = await _accessoryService.CreateAsync(item, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Accessory item, CancellationToken cancellationToken)
        {
            if (id != item.Id)
            {
                return BadRequest("Route id and body id must match.");
            }

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            await _accessoryService.UpdateAsync(item, cancellationToken);
            return NoContent();
        }
    }
}
