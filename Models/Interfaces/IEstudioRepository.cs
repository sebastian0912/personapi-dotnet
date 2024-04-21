using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Models.Interfaces
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
