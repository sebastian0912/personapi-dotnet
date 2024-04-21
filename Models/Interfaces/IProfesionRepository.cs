using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Models.Interfaces
{
    public interface IProfesionRepository
    {
        Task<IEnumerable<Profesion>> GetAllAsync();
        Task<Profesion> GetByIdAsync(int id);
        Task<Profesion> CreateAsync(Profesion profesion);
        Task UpdateAsync(Profesion profesion);
        Task DeleteAsync(int id);
    }
}
