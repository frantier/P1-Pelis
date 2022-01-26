using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ApiPeliculas.Infraestructure.Repositories;
using ApiPeliculas.Domain.Entities;
using ApiPeliculas.Domain.DTOS;
using ApiPeliculas.Domain.DTOS.Response;
using ApiPeliculas.Domain.DTOS.Request;
using System.Security.AccessControl;
using System.Runtime.InteropServices;
using ApiPeliculas.Domain.Interfaces;
using AutoMapper;
using FluentValidation;

namespace ApiPeliculas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeliculasController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMapper _mapper;
        private readonly PeliculaService _service;
        private readonly IValidator<PeliculasRequest> _createValidator;
        private readonly PeliculaRepository _repository;
        public PeliculasController(PeliculaRepository repository, 
        IHttpContextAccessor httpContext, 
        IMapper mapper, 
        PeliculaService service, 
        IValidator<PeliculasRequest> createValidator)
        {
            this._repository = repository;
            this._httpContext = httpContext;
            this._mapper = mapper;
            this._service = service;
            this._createValidator = createValidator;
        }


         //Retorna todos los pois
        //Ejemplo para Thunder client: https://localhost:5001/api/Poi/Todos
        [HttpGet]
        [Route("Todos")]
        public async  Task<IActionResult> TodosLosDatos()
        {
            var peliculas = await _repository.TodosLosDatos();
            //var Respuesta = Garbages.Select(g => CreateDtoFromObject(g));
            var Respuestapeliculas = _mapper.Map<IEnumerable<Pelitabla>,IEnumerable<PeliculaResponse>>(peliculas);
            return Ok(Respuestapeliculas);
        } 

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _repository.PorID(id);
            //entity.Status = false;
            var rows = _repository.Update(id, entity);
            return NoContent();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> PorID(int id)
        {
            var pelicula = await _repository.PorID(id);

            if(pelicula == null)
                return NotFound("No se encontro el archivo.");

            var respuesta = _mapper.Map<Pelitabla, PeliculaResponse>(pelicula);

            return Ok(respuesta);
        }

        [HttpPost]
        
        public async Task<IActionResult> create(PeliculasRequest pelicula)
        {
            var Val = await _createValidator.ValidateAsync(pelicula);
            

            //var Val = _service.ValidatedPOI(entity);

            if(!Val.IsValid)
                return UnprocessableEntity (Val.Errors.Select(d => $"{d.PropertyName} => Error: {d.ErrorMessage}"));

            var entity = _mapper.Map<PeliculasRequest, Pelitabla>(pelicula);

            var id = await _repository.create(entity);
            
            if(id <= 0)
                return Conflict("Intentar nuevamente");

            var host = _httpContext.HttpContext.Request.Host.Value;
            var urlResult = $"https://{host}/api/Eventos/{id}";
            return Created(urlResult, id);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update (int id,[FromBody]Pelitabla pelicula)
        {
            if(id <= 0)
                return NotFound("Archivo perdido.");
            
            pelicula.Id = id;

            var Validated = _service.ValidatedUpdateMovie(pelicula);

            if(!Validated)
                UnprocessableEntity("No se puede actualizar.");
            
            var updated = await _repository.Update(id, pelicula);

            if(!updated)
                Conflict("Hubo un error.");
            
            return NoContent();
        }

        #region"Request"
        private Pelitabla CreateObjectFromDto(PeliculasRequest dto)
        {
            var pelicula = new Pelitabla {
                Id = 0,
                Titulo = string.Empty,
                Director = string.Empty,
                Genero = string.Empty,
                Fecha = string.Empty
            
            };
            return pelicula;
        }
        #endregion
    }
}
