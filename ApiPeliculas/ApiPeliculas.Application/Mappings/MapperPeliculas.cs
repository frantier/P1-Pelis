using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ApiPeliculas.Domain.Entities;
using ApiPeliculas.Domain.DTOS.Response;
using ApiPeliculas.Domain.DTOS.Request;

namespace ApiPeliculas.Application.Mappings
{
    public class MapperPeliculas : Profile
    {
        public MapperPeliculas()
        {
            CreateMap<Pelitabla, PeliculaResponse>()

            .ForMember(Inf => Inf.InfoPelicula, opt => opt.MapFrom(src => $"Titulo: {src.Titulo} Director: {src.Director}"))
            .ForMember(Inf => Inf.Reseñas, opt => opt.MapFrom(src => $"Puntuacion: {src.Puntuacion} Rating:  {src.Rating}"));

            CreateMap<PeliculasRequest, Pelitabla>();
        }
    }
}