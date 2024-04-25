using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Controllers.Interfaces;
using personapi_dotnet.Models.Entities;
using System.Threading.Tasks;

namespace personapi_dotnet.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaRepository _personaRepository;

        public PersonaController(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        // GET: api/Persona
        [HttpGet]
        public async Task<IActionResult> GetPersonas()
        {
            var personas = await _personaRepository.GetAllAsync();
            return Ok(personas);
        }

        // GET: api/Persona/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersona(int id)
        {
            var persona = await _personaRepository.GetByIdAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            return Ok(persona);
        }

        // POST: api/Persona
        [HttpPost]
        public async Task<IActionResult> PostPersona([FromBody] Persona persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdPersona = await _personaRepository.CreateAsync(persona);
            return CreatedAtAction(nameof(GetPersona), new { id = createdPersona.Cc }, createdPersona);
        }

        // PUT: api/Persona/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersona(int id, [FromBody] Persona persona)
        {
            if (id != persona.Cc)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _personaRepository.UpdateAsync(persona);
                return NoContent();
            }
            catch
            {
                return NotFound();
            }
        }

        // DELETE: api/Persona/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersona(int id)
        {
            var persona = await _personaRepository.GetByIdAsync(id);
            if (persona == null)
            {
                return NotFound();
            }

            await _personaRepository.DeleteAsync(id);
            return NoContent(); // Retorna un 204 No Content cuando una operación de borrado es exitosa
        }
    }
}
