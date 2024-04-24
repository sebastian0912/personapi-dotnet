using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Text.Json;
using personapi_dotnet.Models.Entities;
using System.Text; // Necesario para enviar datos en formato JSON
using System.Collections.Generic; // Necesario para usar List

namespace personapi_dotnet.Controllers.View
{
    public class PersonasController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public PersonasController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("API");
            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true
            };
        }

        // GET: Personas
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("Persona");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var personas = JsonSerializer.Deserialize<List<Persona>>(content, _options);
                return View(personas ?? new List<Persona>());
            }
            return NotFound();
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
            if (!ModelState.IsValid)
            {
                return View(persona);
            }

            var json = JsonSerializer.Serialize(persona);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("Persona", data);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the persona.");
                return View(persona);
            }
        }

        // GET: Personas/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"Persona/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var persona = JsonSerializer.Deserialize<Persona>(content, _options);
                return View(persona);
            }
            return NotFound();
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

            if (!ModelState.IsValid)
            {
                return View(persona);
            }

            var json = JsonSerializer.Serialize(persona);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"Persona/{id}", data);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while updating the persona.");
                return View(persona);
            }
        }

        // GET: Personas/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"Persona/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var persona = JsonSerializer.Deserialize<Persona>(content, _options);
                return View(persona);
            }
            return NotFound();
        }

        // GET: Personas/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"Persona/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var persona = JsonSerializer.Deserialize<Persona>(content, _options);
                return View(persona);
            }
            return NotFound();
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"Persona/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the persona.");
                return RedirectToAction(nameof(Delete), new { id = id });
            }
        }
    }
}
