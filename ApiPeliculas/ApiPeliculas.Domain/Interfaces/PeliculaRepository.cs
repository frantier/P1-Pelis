using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPeliculas.Domain.Entities;

namespace ApiPeliculas.Domain.Interfaces
{
    public interface PeliculaRepository
    {
        Task<IEnumerable<Pelitabla>> TodosLosDatos();
        Task<Pelitabla> PorID(int id);
        Task<int> create(Pelitabla pelicula);
        Task<bool> Update(int id, Pelitabla pelicula);
        
    }
}