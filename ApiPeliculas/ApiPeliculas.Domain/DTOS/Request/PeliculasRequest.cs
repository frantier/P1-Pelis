using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPeliculas.Domain.DTOS.Request
{
    public class PeliculasRequest
    {
        public string Titulo { get; set; }
        public string Director { get; set; }
        public string Genero { get; set; }
        public int Puntuacion { get; set; }
        public decimal Rating { get; set; }
        public string Fecha { get; set; }
    }
}