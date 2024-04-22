using personapi_dotnet.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using personapi_dotnet.Models.Interfaces;

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
            return await _context.Estudios
                .Include(e => e.IdProfNavigation)
                .Include(e => e.CcPerNavigation)
                .FirstOrDefaultAsync(e => e.IdProf == idProf && e.CcPer == ccPer);
        }



        public async Task<Estudio> CreateAsync(Estudio estudio)
        {
            try
            {
                // Solo necesitas asegurarte de que las IDs existen, no necesitas cargar las entidades completas
                var existsPersona = await _context.Personas.AnyAsync(p => p.Cc == estudio.CcPer);
                if (!existsPersona)
                {
                    throw new Exception("La persona especificada no existe.");
                }

                var existsProfesion = await _context.Profesions.AnyAsync(p => p.Id == estudio.IdProf);
                if (!existsProfesion)
                {
                    throw new Exception("La profesión especificada no existe.");
                }

                _context.Estudios.Add(estudio);
                await _context.SaveChangesAsync();
                return estudio;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"An error occurred when updating the database: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw;
            }
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

        public async Task<bool> EstudioExists(int idProf, int ccPer)
        {
            return await _context.Estudios.AnyAsync(e => e.IdProf == idProf && e.CcPer == ccPer);
        }


    }
}
