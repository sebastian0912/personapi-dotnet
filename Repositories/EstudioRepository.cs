using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace personapi_dotnet.Repositories
{
    public class EstudioRepository : IEstudioRepository
    {
        private readonly PersonaDbContext _context;

        public EstudioRepository(PersonaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Estudio>> GetAllAsync()
        {
            return await _context.Estudios.ToListAsync();
        }

        public async Task<Estudio> GetByIdAsync(int idProf, int ccPer)
        {
            return await _context.Estudios.FindAsync(idProf, ccPer);
        }

        public async Task<Estudio> CreateAsync(Estudio estudio)
        {
            _context.Estudios.Add(estudio);
            await _context.SaveChangesAsync();
            return estudio;
        }

        public async Task UpdateAsync(Estudio estudio)
        {
            _context.Entry(estudio).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int idProf, int ccPer)
        {
            var estudio = await _context.Estudios.FindAsync(idProf, ccPer);
            if (estudio != null)
            {
                _context.Estudios.Remove(estudio);
                await _context.SaveChangesAsync();
            }
        }
    }
}
