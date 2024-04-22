using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class TelefonosController : ControllerBase
{
    private readonly ITelefonoRepository _repository;

    public TelefonosController(ITelefonoRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Telefono>>> Get()
    {
        return Ok(await _repository.GetAllAsync());
    }

    [HttpGet("{num}")]
    public async Task<ActionResult<Telefono>> Get(string num)
    {
        var telefono = await _repository.GetByIdAsync(num);
        if (telefono == null)
        {
            return NotFound();
        }
        return telefono;
    }

    [HttpPost]
    public async Task<ActionResult<Telefono>> Post([FromBody] Telefono telefono)
    {
        await _repository.CreateAsync(telefono);
        return CreatedAtAction(nameof(Get), new { num = telefono.Num }, telefono);
    }

    [HttpPut("{num}")]
    public async Task<IActionResult> Put(string num, [FromBody] Telefono telefono)
    {
        if (num != telefono.Num)
        {
            return BadRequest();
        }
        await _repository.UpdateAsync(telefono);
        return NoContent();
    }

    [HttpDelete("{num}")]
    public async Task<IActionResult> DeleteTelefonos(string num)
    {
        await _repository.DeleteAsync(num);
        return NoContent();
    }
}
