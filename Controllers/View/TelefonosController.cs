using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Text.Json;
using personapi_dotnet.Models.Entities;
using System.Text;
using System.Collections.Generic;

namespace personapi_dotnet.Controllers.View
{
    public class TelefonosController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public TelefonosController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("API");
            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true
            };
        }

        // GET: Telefonos
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("Telefonos");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var telefonos = JsonSerializer.Deserialize<List<Telefono>>(content, _options);
                return View(telefonos ?? new List<Telefono>());
            }
            return NotFound();
        }

        // GET: Telefonos/Create
        public IActionResult Create()
        {
            return View(new Telefono());
        }

        // POST: Telefonos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Telefono telefono)
        {
            if (!ModelState.IsValid)
            {
                return View(telefono);
            }

            var json = JsonSerializer.Serialize(telefono);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("Telefonos", data);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "An error occurred while creating the telefono.");
            return View(telefono);
        }

        // GET: Telefonos/Edit/{num}
        public async Task<IActionResult> Edit(string num)
        {
            var response = await _httpClient.GetAsync($"Telefonos/{num}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var telefono = JsonSerializer.Deserialize<Telefono>(content, _options);
                return View(telefono);
            }
            return NotFound();
        }

        // POST: Telefonos/Edit/{num}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string num, Telefono telefono)
        {
            if (num != telefono.Num)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(telefono);
            }

            var json = JsonSerializer.Serialize(telefono);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"Telefonos/{num}", data);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "An error occurred while updating the telefono.");
            return View(telefono);
        }

        // GET: Telefonos/Details/{num}
        public async Task<IActionResult> Details(string num)
        {
            var response = await _httpClient.GetAsync($"Telefonos/{num}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var telefono = JsonSerializer.Deserialize<Telefono>(content, _options);
                return View(telefono);
            }
            return NotFound();
        }

        // GET: Telefonos/Delete/{num}
        public async Task<IActionResult> Delete(string num)
        {
            var response = await _httpClient.GetAsync($"Telefonos/{num}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var telefono = JsonSerializer.Deserialize<Telefono>(content, _options);
                return View(telefono);
            }
            return NotFound();
        }

        // POST: Telefonos/Delete/{num}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string num)
        {
            var response = await _httpClient.DeleteAsync($"Telefonos/{num}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "An error occurred while deleting the telefono.");
            return View();
        }
    }
}
