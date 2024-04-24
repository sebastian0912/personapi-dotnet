using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Text.Json;
using personapi_dotnet.Models.Entities;
using System.Text;
using System.Collections.Generic;

namespace personapi_dotnet.Controllers.View
{
    public class ProfesionController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public ProfesionController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("API");
            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true
            };
        }

        // GET: Profesion
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("Profesions");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var profesiones = JsonSerializer.Deserialize<List<Profesion>>(content, _options);
                return View(profesiones ?? new List<Profesion>());
            }
            return NotFound();
        }

        // GET: Profesion/Create
        public IActionResult Create()
        {
            return View(new Profesion());
        }

        // POST: Profesion/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Profesion profesion)
        {
            if (!ModelState.IsValid)
            {
                return View(profesion);
            }

            var json = JsonSerializer.Serialize(profesion);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("Profesions", data);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "An error occurred while creating the profesion.");
            return View(profesion);
        }

        // GET: Profesion/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"Profesions/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var profesion = JsonSerializer.Deserialize<Profesion>(content, _options);
                return View(profesion);
            }
            return NotFound();
        }

        // GET: Profesion/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"Profesions/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var profesion = JsonSerializer.Deserialize<Profesion>(content, _options);
                return View(profesion);
            }
            return NotFound();
        }

        // POST: Profesion/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Profesion profesion)
        {
            if (id != profesion.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(profesion);
            }

            var json = JsonSerializer.Serialize(profesion);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"Profesions/{id}", data);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "An error occurred while updating the profesion.");
            return View(profesion);
        }

        // GET: Profesion/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"Profesions/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var profesion = JsonSerializer.Deserialize<Profesion>(content, _options);
                return View(profesion);
            }
            return NotFound();
        }

        // POST: Profesion/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"Profesions/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "An error occurred while deleting the profesion.");
            return View();
        }
    }
}
