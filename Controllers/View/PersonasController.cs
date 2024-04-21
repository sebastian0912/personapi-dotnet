using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.Interfaces;

namespace personapi_dotnet.Controllers.View
{
    public class PersonasController : Controller
    {
        private readonly IPersonaRepository _personaRepository;

        public PersonasController(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        // GET: Personas
        public async Task<IActionResult> Index()
        {
            var personas = await _personaRepository.GetAllAsync();
            return View(personas);
        }

        // GET: Personas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Personas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Persona persona)
        {
            if (ModelState.IsValid)
            {
                await _personaRepository.CreateAsync(persona);
                return RedirectToAction(nameof(Index));
            }
            return View(persona);
        }

        // GET: Personas/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var persona = await _personaRepository.GetByIdAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            return View(persona);
        }

        // POST: Personas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Persona persona)
        {
            if (id != persona.Cc)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _personaRepository.UpdateAsync(persona);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await PersonaExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(persona);
        }


        // GET: Personas/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var persona = await _personaRepository.GetByIdAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            return View(persona);
        }

        // GET: Personas/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var persona = await _personaRepository.GetByIdAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            return View(persona);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _personaRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> PersonaExists(int id)
        {
            return await _personaRepository.GetByIdAsync(id) != null;
        }
    }
}
