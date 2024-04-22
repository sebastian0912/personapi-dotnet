using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.Interfaces;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace personapi_dotnet.Controllers.View
{
    public class EstudiosController : Controller
    {
        private readonly IEstudioRepository _estudioRepository;
        private readonly PersonaDbContext _context;

        public EstudiosController(IEstudioRepository estudioRepository, PersonaDbContext context)
        {
            _estudioRepository = estudioRepository;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var estudios = await _estudioRepository.GetAllAsync();
            return View(estudios);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Estudio estudio)
        {
            // Elimina los errores relacionados con las propiedades de navegación
            ModelState.Remove("CcPerNavigation");
            ModelState.Remove("IdProfNavigation");

            // Verifica que el estado del modelo sea válido
            if (ModelState.IsValid)
            {
                // Comprueba si la Persona existe
                bool personaExists = await _context.Personas.AnyAsync(p => p.Cc == estudio.CcPer);
                if (!personaExists)
                {
                    // Si no existe, agrega un error al ModelState y devuelve a la vista
                    ModelState.AddModelError("CcPer", "La persona especificada no existe.");
                    return View(estudio);
                }

                // Comprueba si la Profesion existe
                bool profesionExists = await _context.Profesions.AnyAsync(p => p.Id == estudio.IdProf);
                if (!profesionExists)
                {
                    // Si no existe, agrega un error al ModelState y devuelve a la vista
                    ModelState.AddModelError("IdProf", "La profesión especificada no existe.");
                    return View(estudio);
                }

                try
                {
                    // Intenta crear el estudio
                    await _estudioRepository.CreateAsync(estudio);
                    // Si todo va bien, redirige al índice
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Si ocurre una excepción, agrega el error al ModelState y devuelve a la vista
                    ModelState.AddModelError("", $"Ocurrió un error al guardar el estudio: {ex.Message}");
                }
            }

            // Si el ModelState no es válido, simplemente devuelve a la vista con los mensajes de error
            return View(estudio);
        }




        public async Task<IActionResult> Details(int idProf, int ccPer)
        {
            var estudio = await _estudioRepository.GetByIdAsync(idProf, ccPer);

            if (estudio == null)
            {
                return NotFound();
            }
            return View(estudio);
        }



        public async Task<IActionResult> Edit(int idProf, int ccPer)
        {
            var estudio = await _estudioRepository.GetByIdAsync(idProf, ccPer);
            if (estudio == null)
            {
                return NotFound();
            }
            return View(estudio);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int idProf, int ccPer, [Bind("IdProf, CcPer, Fecha, Univer")] Estudio estudio)

        {
            if (idProf != estudio.IdProf || ccPer != estudio.CcPer)
            {
                return NotFound();
            }

            // Elimina los errores relacionados con las propiedades de navegación
            ModelState.Remove("CcPerNavigation");
            ModelState.Remove("IdProfNavigation");

            if (ModelState.IsValid)
            {
                bool personaExists = await _context.Personas.AnyAsync(p => p.Cc == estudio.CcPer);
                bool profesionExists = await _context.Profesions.AnyAsync(p => p.Id == estudio.IdProf);

                if (!personaExists || !profesionExists)
                {
                    if (!personaExists)
                    {
                        ModelState.AddModelError("CcPer", "La persona especificada no existe.");
                    }
                    if (!profesionExists)
                    {
                        ModelState.AddModelError("IdProf", "La profesión especificada no existe.");
                    }
                    return View(estudio);
                }

                try
                {
                    await _estudioRepository.UpdateAsync(estudio);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    ModelState.AddModelError("", $"Ocurrió un error de concurrencia al actualizar el estudio: {ex.Message}");
                    return View(estudio);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Ocurrió un error al actualizar el estudio: {ex.Message}");
                    return View(estudio);
                }
            }

            return View(estudio);
        }




        public async Task<IActionResult> Delete(int idProf, int ccPer)
        {
            var estudio = await _estudioRepository.GetByIdAsync(idProf, ccPer);
            if (estudio == null)
            {
                return NotFound();
            }
            return View(estudio);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int idProf, int ccPer)
        {
            await _estudioRepository.DeleteAsync(idProf, ccPer);
            return RedirectToAction(nameof(Index));
        }
    }
}
