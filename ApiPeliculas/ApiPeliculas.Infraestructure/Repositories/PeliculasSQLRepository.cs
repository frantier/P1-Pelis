using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using ApiPeliculas.Infraestructure.Data;
using ApiPeliculas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ApiPeliculas.Domain.Interfaces;

namespace ApiPeliculas.Infraestructure.Repositories
{
    public class PeliculasSQLRepository : PeliculaRepository
    {
        private readonly peliculaspContext _context;

        public PeliculasSQLRepository(peliculaspContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pelitabla>> TodosLosDatos()
        {
            var encuentro = _context.Pelitablas.Select(g => g);
            return await encuentro.ToListAsync();
        }

        public async Task<Pelitabla> PorID(int id)
        {
            var poi = await _context.Pelitablas.FirstOrDefaultAsync(dn => dn.Id == id);
            return poi;
        }


        public async Task<int> create(Pelitabla pelicula)
        {
            var entity = pelicula;
            await _context.Pelitablas.AddAsync(entity);
            var rows = await _context.SaveChangesAsync();

            if(rows <= 0)
                throw new Exception("El registro no pudo ser completado");
            
            return entity.Id;
        }

        public async Task<bool> Update(int id, Pelitabla pelicula)
        {
            if(id <= 0 || pelicula == null)
                throw new ArgumentException("Falta informacion para poder realizar la modificacion");

            var entity = await PorID(id);

            entity.Titulo = pelicula.Titulo;
            entity.Director = pelicula.Director;
            entity.Genero = pelicula.Genero;
            entity.Puntuacion = pelicula.Puntuacion;
            entity.Rating = pelicula.Rating;
            entity.Fecha = pelicula.Fecha;

            _context.Update(entity);

            var rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
        
    }
}