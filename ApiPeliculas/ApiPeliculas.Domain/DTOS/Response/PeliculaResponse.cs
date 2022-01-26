using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPeliculas.Domain.DTOS.Response
{
    public class PeliculaResponse
    {
        public int Id {get; set;}
        public string InfoPelicula {get; set;}
        public string Reseñas {get; set;}
        public string Fecha { get; set; }
        
    }
}