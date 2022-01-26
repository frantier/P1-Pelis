using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPeliculas.Domain.Entities;
using ApiPeliculas.Domain.Interfaces;

namespace ApiPeliculas.Application.Services
{
    public class ServicePeliculas : PeliculaService
    {
        public bool ValidatedMovie (Pelitabla pelicula)
        {
            if(string.IsNullOrEmpty(pelicula.Titulo))
                return false;

            if(string.IsNullOrEmpty(pelicula.Director))
                return false;

            if(string.IsNullOrEmpty(pelicula.Genero))
                return false;

            if(string.IsNullOrEmpty(pelicula.Fecha))
                return false;

            return true;
        }

        public bool ValidatedUpdateMovie (Pelitabla pelicula)
        {
            if(string.IsNullOrEmpty(pelicula.Titulo))
                return false;

            if(string.IsNullOrEmpty(pelicula.Director))
                return false;

            if(string.IsNullOrEmpty(pelicula.Genero))
                return false;

            if(string.IsNullOrEmpty(pelicula.Fecha))
                return false;

            return true;
        }
        
    }
}