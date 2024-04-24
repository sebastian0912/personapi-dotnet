using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Text.Json;
using personapi_dotnet.Models.Entities;
using System.Text;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
                AllowTrailingCommas = true
            };
        }

        // GET: Estudios
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("Estudios");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var estudios = JsonSerializer.Deserialize<List<Estudio>>(content, _options);
                return View(estudios ?? new List<Estudio>());
            }
            return NotFound();
        }

        // GET: Estudios/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CcPer"] = new SelectList(await FetchPersonas(), "Cc", "Cc");
            ViewData["IdProf"] = new SelectList(await FetchProfesiones(), "Id", "Id");
            return View();
        }

        // POST: Estudios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Estudio estudio)
        {
            if (ModelState.IsValid)
            {
                var json = JsonSerializer.Serialize(estudio);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/Estudios", data);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, "Error al crear el estudio: " + errorResponse);
                }
            }

            // Recargar los SelectList si hay un fallo
            ViewData["CcPer"] = new SelectList(await FetchPersonas(), "Id", "Nombre", estudio.CcPer);
            ViewData["IdProf"] = new SelectList(await FetchProfesiones(), "Id", "Nom", estudio.IdProf);

            return View(estudio);
        }

        private async Task<IEnumerable<Persona>> FetchPersonas()
        {
            var response = await _httpClient.GetAsync("Persona");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var persona = JsonSerializer.Deserialize<IEnumerable<Persona>>(jsonResponse) ?? new List<Persona>();
                Console.WriteLine("------------Personas: " + JsonSerializer.Serialize(persona));
                return persona;
            }
            else
            {
                // Log the error or throw an exception
                Console.WriteLine($"Error fetching personas: {response.StatusCode}");
                return new List<Persona>();
            }
        }

        private async Task<IEnumerable<Profesion>> FetchProfesiones()
        {
            var response = await _httpClient.GetAsync("Profesions");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var profesion = JsonSerializer.Deserialize<IEnumerable<Profesion>>(jsonResponse) ?? new List<Profesion>();
                Console.WriteLine("------------Profesiones: " + JsonSerializer.Serialize(profesion));
                return profesion;
            }
            else
            {
                // Log the error or throw an exception
                Console.WriteLine($"Error fetching profesiones: {response.StatusCode}");
                return new List<Profesion>();
            }
        }









        // GET: Estudios/Details/{idProf}/{ccPer}
        public async Task<IActionResult> Details(int idProf, int ccPer)
        {
            var response = await _httpClient.GetAsync($"Estudios/{idProf}/{ccPer}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var estudio = JsonSerializer.Deserialize<Estudio>(content, _options);
                return View(estudio);
            }
            return NotFound();
        }

        // GET: Estudios/Edit/{idProf}/{ccPer}
        public async Task<IActionResult> Edit(int idProf, int ccPer)
        {
            var response = await _httpClient.GetAsync($"Estudios/{idProf}/{ccPer}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var estudio = JsonSerializer.Deserialize<Estudio>(content, _options);
                return View(estudio);
            }
            return NotFound();
        }

        // POST: Estudios/Edit/{idProf}/{ccPer}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int idProf, int ccPer, Estudio estudio)
        {
            if (idProf != estudio.IdProf || ccPer != estudio.CcPer)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(estudio);
            }

            var json = JsonSerializer.Serialize(estudio);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"Estudios/{idProf}/{ccPer}", data);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "An error occurred while updating the estudio.");
            return View(estudio);
        }

        // GET: Estudios/Delete/{idProf}/{ccPer}
        public async Task<IActionResult> Delete(int idProf, int ccPer)
        {
            var response = await _httpClient.GetAsync($"Estudios/{idProf}/{ccPer}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var estudio = JsonSerializer.Deserialize<Estudio>(content, _options);
                return View(estudio);
            }
            return NotFound();
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
            ModelState.AddModelError(string.Empty, "An error occurred while deleting the estudio.");
            return View();
        }
    }
}
