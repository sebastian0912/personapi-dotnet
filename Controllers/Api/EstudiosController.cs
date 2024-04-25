using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Controllers.Interfaces;
using personapi_dotnet.Models.Entities;

[ApiController]
[Route("api/[controller]")]
public class EstudiosController : ControllerBase
{
    private readonly IEstudioRepository _repository;

    public EstudiosController(IEstudioRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Estudio>>> Get()
    {
        return Ok(await _repository.GetAllAsync());
    }

    [HttpGet("{idProf}/{ccPer}")]
    public async Task<ActionResult<Estudio>> Get(int idProf, int ccPer)
    {
        var estudio = await _repository.GetByIdAsync(idProf, ccPer);
        if (estudio == null)
        {
            return NotFound();
        }
        return estudio;
    }

    [HttpPost]
    public async Task<ActionResult<Estudio>> Post([FromBody] Estudio estudio)
    {

        await _repository.CreateAsync(estudio);
        return CreatedAtAction(nameof(Get), new { idProf = estudio.IdProf, ccPer = estudio.CcPer }, estudio);
    }


    [HttpPut("{idProf}/{ccPer}")]
    public async Task<IActionResult> Put(int idProf, int ccPer, [FromBody] Estudio estudio)
    {
        if (idProf != estudio.IdProf || ccPer != estudio.CcPer)
        {
            return BadRequest();
        }
        await _repository.UpdateAsync(estudio);
        return NoContent();
    }

    [HttpDelete("{idProf}/{ccPer}")]
    public async Task<IActionResult> DeleteEstudios(int idProf, int ccPer)
    {
        await _repository.DeleteAsync(idProf, ccPer);
        return NoContent();
    }
}
