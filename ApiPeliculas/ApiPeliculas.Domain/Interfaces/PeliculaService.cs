using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPeliculas.Domain.Entities;

namespace ApiPeliculas.Domain.Interfaces
{
    public interface PeliculaService
    {
        bool ValidatedMovie(Pelitabla Apipelicula);
        bool ValidatedUpdateMovie(Pelitabla Apipelicula);
    }
}