using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPeliculas.Domain.DTOS.Request;
using FluentValidation;

namespace ApiPeliculas.Infraestructure.Validators
{
    public class PeliculasValidator : AbstractValidator<PeliculasRequest>
    {
         public PeliculasValidator()
        {
            RuleFor(p => p.Titulo).NotNull().NotEmpty().Length(5,30);
            RuleFor(p => p.Director).NotNull().NotEmpty();
            RuleFor(p => p.Genero).NotNull().NotEmpty().Length(5,20);
            RuleFor(p => p.Puntuacion).NotNull().NotEmpty();
            RuleFor(p => p.Rating).NotNull().NotEmpty();
            RuleFor(p => p.Fecha).NotNull().NotEmpty();
        }
        
    }
}