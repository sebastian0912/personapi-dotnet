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
                AllowTrailingCommas = true,
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve, // Adding to handle potential circular references
                PropertyNamingPolicy = null // Ensuring exact property name match
            };
        }

        // GET: Profesion
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("Profesions");
            return await HandleResponse<List<Profesion>>(response);
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
                return View(profesion);

            var response = await PostJsonAsync("Profesions", profesion);
            return response.IsSuccessStatusCode ? RedirectToAction(nameof(Index)) : HandleError(response, profesion);
        }

        // GET: Profesion/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"Profesions/{id}");
            return await HandleResponse<Profesion>(response);
        }

        // GET: Profesion/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"Profesions/{id}");
            return await HandleResponse<Profesion>(response);
        }

        // POST: Profesion/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Profesion profesion)
        {
            if (id != profesion.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(profesion);

            var json = JsonSerializer.Serialize(profesion, _options);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"Profesions/{id}", data);
            if (response.IsSuccessStatusCode)
            {
                 return RedirectToAction(nameof(Index));
            }

            return HandleError(response, profesion);
        }

        // GET: Profesion/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"Profesions/{id}");
            return await HandleResponse<Profesion>(response);
        }

        // POST: Profesion/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"Profesions/{id}");
            return response.IsSuccessStatusCode ? RedirectToAction(nameof(Index)) : HandleError(response);
        }

        private async Task<IActionResult> HandleResponse<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<T>(content, _options);
                return View(result);
            }
            return HandleError(response);
        }

        private async Task<HttpResponseMessage> PostJsonAsync<T>(string uri, T item)
        {
            var json = JsonSerializer.Serialize(item, _options);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync(uri, data);
        }

        private IActionResult HandleError(HttpResponseMessage response, object model = null)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();
            ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
            return model == null ? View("Error") : View(model);
        }
    }
}
