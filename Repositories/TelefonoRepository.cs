﻿using personapi_dotnet.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using personapi_dotnet.Controllers.Interfaces;

namespace personapi_dotnet.Repositories
{
    public class TelefonoRepository : ITelefonoRepository
    {
        private readonly PersonaDbContext _context;

        public TelefonoRepository(PersonaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Telefono>> GetAllAsync()
        {
            return await _context.Telefonos.ToListAsync();
        }

        public async Task<Telefono> GetByIdAsync(string num)
        {
            return await _context.Telefonos.FindAsync(num);
        }

        public async Task<Telefono> CreateAsync(Telefono telefono)
        {
            _context.Telefonos.Add(telefono);
            await _context.SaveChangesAsync();
            return telefono;
        }

        public async Task UpdateAsync(Telefono telefono)
        {
            var existingTelefono = await _context.Telefonos
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Num == telefono.Num);

            if (existingTelefono == null)
            {
                _context.Telefonos.Add(telefono);
            }
            else
            {
                _context.Entry(telefono).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(string num)
        {
            var telefono = await _context.Telefonos.FindAsync(num);
            if (telefono != null)
            {
                _context.Telefonos.Remove(telefono);
                await _context.SaveChangesAsync();
            }
        }
    }
}
