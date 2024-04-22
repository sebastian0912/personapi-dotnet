using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.Interfaces;
using System.Threading.Tasks;

namespace personapi_dotnet.Controllers.View
{
    public class TelefonosController : Controller
    {
        private readonly ITelefonoRepository _telefonoRepository;

        public TelefonosController(ITelefonoRepository telefonoRepository)
        {
            _telefonoRepository = telefonoRepository;
        }

        // GET: Telefonos
        public async Task<IActionResult> Index()
        {
            var telefonos = await _telefonoRepository.GetAllAsync();
            return View(telefonos);
        }

        // GET: Telefonos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Telefonos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Telefono telefono)
        {
            if (ModelState.IsValid)
            {
                await _telefonoRepository.CreateAsync(telefono);
                return RedirectToAction(nameof(Index));
            }
            return View(telefono);
        }

        // GET: Telefonos/Edit/5
        public async Task<IActionResult> Edit(string num)
        {
            var telefono = await _telefonoRepository.GetByIdAsync(num);
            if (telefono == null)
            {
                return NotFound();
            }
            return View(telefono);
        }

        // POST: Telefonos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string num, Telefono telefono)
        {
            if (num != telefono.Num)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _telefonoRepository.UpdateAsync(telefono);
                return RedirectToAction(nameof(Index));
            }
            return View(telefono);
        }

        // GET: Telefonos/Details/5
        public async Task<IActionResult> Details(string num)
        {
            var telefono = await _telefonoRepository.GetByIdAsync(num);
            if (telefono == null)
            {
                return NotFound();
            }
            return View(telefono);
        }

        // GET: Telefonos/Delete/5
        public async Task<IActionResult> Delete(string num)
        {
            var telefono = await _telefonoRepository.GetByIdAsync(num);
            if (telefono == null)
            {
                return NotFound();
            }
            return View(telefono);
        }

        // POST: Telefonos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string num)
        {
            await _telefonoRepository.DeleteAsync(num);
            return RedirectToAction(nameof(Index));
        }

    }
}
