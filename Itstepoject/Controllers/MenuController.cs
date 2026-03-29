using Itstepoject.Models;
using Itstepoject.Services;
using Microsoft.AspNetCore.Mvc;

namespace Itstepoject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<MenuItem>>> GetAll(CancellationToken cancellationToken)
        {
            var items = await _menuService.GetAllAsync(cancellationToken);
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MenuItem>> GetById(int id, CancellationToken cancellationToken)
        {
            var item = await _menuService.GetByIdAsync(id, cancellationToken);
            return item is null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<MenuItem>> Create([FromBody] MenuItem item, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            item.Id = 0;
            var created = await _menuService.CreateAsync(item, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] MenuItem item, CancellationToken cancellationToken)
        {
            if (id != item.Id)
            {
                return BadRequest("Route id and body id must match.");
            }

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            await _menuService.UpdateAsync(item, cancellationToken);
            return NoContent();
        }
    }
}
