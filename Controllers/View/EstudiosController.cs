using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Text.Json;
using personapi_dotnet.Models.Entities;
using System.Text;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace personapi_dotnet.Controllers.View
{
    public class EstudiosController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public EstudiosController(IHttpClientFactory httpClientFactory)
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

        // GET: Estudios
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("Estudios");
            return await HandleResponse<List<Estudio>>(response);
        }

        // GET: Estudios/Create
        public async Task<IActionResult> Create()
        {
            await LoadRelatedData();
            return View(new Estudio());
        }

        // POST: Estudios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Estudio estudio)
        {
            if (!ModelState.IsValid)
            {
                await LoadRelatedData();
                return View(estudio);
            }

            var response = await PostJsonAsync("Estudios", estudio);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            await LoadRelatedData(); // Recargar datos si hay un error
            return HandleError(response, estudio);
        }

        // GET: Estudios/Details/{idProf}/{ccPer}
        public async Task<IActionResult> Details(int idProf, int ccPer)
        {
            var response = await _httpClient.GetAsync($"Estudios/{idProf}/{ccPer}");
            return await HandleResponse<Estudio>(response);
        }

        // GET: Estudios/Edit/{idProf}/{ccPer}
        public async Task<IActionResult> Edit(int idProf, int ccPer)
        {
            var response = await _httpClient.GetAsync($"Estudios/{idProf}/{ccPer}");
            return await HandleResponse<Estudio>(response);
        }

        // POST: Estudios/Edit/{idProf}/{ccPer}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int idProf, int ccPer, Estudio estudio)
        {
            ModelState.Remove("CcPerNavigation");
            ModelState.Remove("IdProfNavigation");

            if (idProf != estudio.IdProf || ccPer != estudio.CcPer || !ModelState.IsValid)
            {
                return View(estudio);
            }

            var response = await PostJsonAsync($"Estudios/{idProf}/{ccPer}", estudio);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return HandleError(response, estudio);
        }

        // GET: Estudios/Delete/{idProf}/{ccPer}
        public async Task<IActionResult> Delete(int idProf, int ccPer)
        {
            var response = await _httpClient.GetAsync($"Estudios/{idProf}/{ccPer}");
            return await HandleResponse<Estudio>(response);
        }

        // POST: Estudios/Delete/{idProf}/{ccPer}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int idProf, int ccPer)
        {
            var response = await _httpClient.DeleteAsync($"Estudios/{idProf}/{ccPer}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return HandleError(response);
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

        private async Task LoadRelatedData()
        {
            ViewData["CcPer"] = new SelectList(await FetchPersonas(), "Cc", "Nombre");
            ViewData["IdProf"] = new SelectList(await FetchProfesiones(), "Id", "Nom");
        }


        private async Task<IEnumerable<Persona>> FetchPersonas()
        {
            var response = await _httpClient.GetAsync("Persona");
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var personas = JsonSerializer.Deserialize<IEnumerable<Persona>>(jsonResponse, _options);
                    return personas ?? new List<Persona>();
                }
                catch (JsonException ex)
                {
                    // Log the error or handle it accordingly
                    Console.WriteLine($"JSON Error: {ex.Message}");
                    return new List<Persona>();
                }
            }
            return new List<Persona>();
        }

        private async Task<IEnumerable<Profesion>> FetchProfesiones()
        {
            var response = await _httpClient.GetAsync("Profesions");
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var profesiones = JsonSerializer.Deserialize<IEnumerable<Profesion>>(jsonResponse, _options);
                    System.Console.WriteLine("--------------"+profesiones);
                    return profesiones ?? new List<Profesion>();
                }
                catch (JsonException ex)
                {
                    // Log the error or handle it accordingly
                    Console.WriteLine($"JSON Error: {ex.Message}");
                    return new List<Profesion>();
                }
            }

            return new List<Profesion>();
        }



        // buscar profesiones por id
        private async Task<Profesion> FetchProfesion(int id)
        {
            var response = await _httpClient.GetAsync($"Profesions/{id}");
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var profesion = JsonSerializer.Deserialize<Profesion>(jsonResponse, _options);
                    return profesion;
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"JSON Error: {ex.Message}");
                    return null;
                }
            }
            return null;
        }
           

        

        // buscar personas por cc
        private async Task<Persona> FetchPersona(int cc)
        {
            var response = await _httpClient.GetAsync($"Persona/{cc}");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var persona = JsonSerializer.Deserialize<Persona>(jsonResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return persona;
            }
            return null;
        }





    }
}
