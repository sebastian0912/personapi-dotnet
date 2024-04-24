using personapi_dotnet.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using personapi_dotnet.Controllers.Interfaces;

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
            var estudio = await _context.Estudios
                .Include(e => e.IdProfNavigation)
                .Include(e => e.CcPerNavigation)
                .FirstOrDefaultAsync(e => e.IdProf == idProf && e.CcPer == ccPer);

            if (estudio == null)
            {
                throw new InvalidOperationException("Estudio no encontrado.");
            }

            return estudio;
        }


        public async Task<Estudio> CreateAsync(Estudio estudio)
        {
            var persona = await _context.Personas.FindAsync(estudio.CcPer);
            var profesion = await _context.Profesions.FindAsync(estudio.IdProf);

            if (persona == null || profesion == null)
            {
                throw new Exception("La Persona o la Profesión especificada no existe.");
            }

            // Asignar las entidades encontradas a las propiedades de navegación
            estudio.CcPerNavigation = persona;
            estudio.IdProfNavigation = profesion;

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

        public async Task<bool> EstudioExists(int idProf, int ccPer)
        {
            return await _context.Estudios.AnyAsync(e => e.IdProf == idProf && e.CcPer == ccPer);
        }


    }
}
