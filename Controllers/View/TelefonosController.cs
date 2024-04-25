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
                AllowTrailingCommas = true,
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve, // Adding this to handle potential circular references
                PropertyNamingPolicy = null // Ensuring exact property name match
            };
        }

        // GET: Telefonos
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("Telefonos");
            return await HandleResponse<List<Telefono>>(response);
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
                return View(telefono);

            var response = await PostJsonAsync("Telefonos", telefono);
            return response.IsSuccessStatusCode ? RedirectToAction(nameof(Index)) : HandleError(response, telefono);
        }

        private async Task<HttpResponseMessage> PutJsonAsync<T>(string uri, T item)
        {
            var json = JsonSerializer.Serialize(item, _options);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            return await _httpClient.PutAsync(uri, data);
        }

        // GET: Telefonos/Edit/{num}
        public async Task<IActionResult> Edit(string num)
        {
            var response = await _httpClient.GetAsync($"Telefonos/{num}");
            return await HandleResponse<Telefono>(response);
        }

        // POST: Telefonos/Edit/{num}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string num, Telefono telefono)
        {
            if (num != telefono.Num)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(telefono);

            var response = await PutJsonAsync($"Telefonos/{num}", telefono);
            return response.IsSuccessStatusCode ? RedirectToAction(nameof(Index)) : HandleError(response, telefono);
        }

        // GET: Telefonos/Details/{num}
        public async Task<IActionResult> Details(string num)
        {
            var response = await _httpClient.GetAsync($"Telefonos/{num}");
            return await HandleResponse<Telefono>(response);
        }

        // GET: Telefonos/Delete/{num}
        public async Task<IActionResult> Delete(string num)
        {
            var response = await _httpClient.GetAsync($"Telefonos/{num}");
            return await HandleResponse<Telefono>(response);
        }

        // POST: Telefonos/Delete/{num}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string num)
        {
            var response = await _httpClient.DeleteAsync($"Telefonos/{num}");
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
