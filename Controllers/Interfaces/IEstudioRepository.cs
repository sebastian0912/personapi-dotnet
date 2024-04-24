using personapi_dotnet.Models.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;

namespace personapi_dotnet.Controllers.Interfaces
{
    public interface IEstudioRepository
    {
        Task<IEnumerable<Estudio>> GetAllAsync();
        Task<Estudio> GetByIdAsync(int idProf, int ccPer);
        Task<Estudio> CreateAsync(Estudio estudio);
        Task UpdateAsync(Estudio estudio);
        Task DeleteAsync(int idProf, int ccPer);
    }
}