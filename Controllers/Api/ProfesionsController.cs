using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class ProfesionsController : ControllerBase
{
    private readonly IProfesionRepository _repository;

    public ProfesionsController(IProfesionRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Profesion>>> Get()
    {
        return Ok(await _repository.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Profesion>> Get(int id)
    {
        var profesion = await _repository.GetByIdAsync(id);
        if (profesion == null)
        {
            return NotFound();
        }
        return profesion;
    }

    [HttpPost]
    public async Task<ActionResult<Profesion>> Post([FromBody] Profesion profesion)
    {
        await _repository.CreateAsync(profesion);
        return CreatedAtAction(nameof(Get), new { id = profesion.Id }, profesion);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Profesion profesion)
    {
        if (id != profesion.Id)
        {
            return BadRequest();
        }
        await _repository.UpdateAsync(profesion);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}
