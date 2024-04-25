using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Text.Json;
using personapi_dotnet.Models.Entities;
using System.Text;
using System.Collections.Generic;

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
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true,
                PropertyNamingPolicy = null
            };
        }

        // GET: Personas
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("Persona");
            if (response.IsSuccessStatusCode)
            {
                var personas = await DeserializeResponse<List<Persona>>(response);
                return View(personas ?? new List<Persona>());
            }
            return HandleErrorResponse(response);
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

            var json = JsonSerializer.Serialize(persona, _options);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("Persona", data);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return HandleErrorResponse(response);
        }

        // GET: Personas/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"Persona/{id}");
            if (response.IsSuccessStatusCode)
            {
                var persona = await DeserializeResponse<Persona>(response);
                if (persona != null)
                    return View(persona);
            }
            return HandleErrorResponse(response);
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

            var json = JsonSerializer.Serialize(persona, _options);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"Persona/{id}", data);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return HandleErrorResponse(response);
        }

        // GET: Personas/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"Persona/{id}");
            if (response.IsSuccessStatusCode)
            {
                var persona = await DeserializeResponse<Persona>(response);
                if (persona != null)
                    return View(persona);
            }
            return HandleErrorResponse(response);
        }

        // GET: Personas/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"Persona/{id}");
            if (response.IsSuccessStatusCode)
            {
                var persona = await DeserializeResponse<Persona>(response);
                if (persona != null)
                    return View(persona);
            }
            return HandleErrorResponse(response);
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
            return HandleErrorResponse(response);
        }

        private async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(content, _options);
            }
            return default;
        }

        private IActionResult HandleErrorResponse(HttpResponseMessage response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return View("Error"); // Asegúrate de tener una vista de Error adecuada.
            }
        }
    }
}
