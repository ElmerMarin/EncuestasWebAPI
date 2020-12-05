using ApiRestfullSurvey.Contexts;
using ApiRestfullSurvey.Entities;
using ApiRestfullSurvey.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestfullSurvey.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncuestasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public EncuestasController(ApplicationDbContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("/listado")]
        [HttpGet("listado")]
        [HttpGet]
        public ActionResult<IEnumerable<Encuesta>> Get()
        {

            return context.Encuestas.ToList();
        }

        [HttpGet("Primer")]
        public ActionResult<Encuesta> GetPrimeraEncuesta()
        {
            return context.Encuestas.FirstOrDefault();
        }

        [HttpGet("{id}/{param2=Param}")]
        public async Task<ActionResult<EncuestaDTO>> GetEncuesta(int id, string param2)
        {
            var encuesta = await context.Encuestas.FirstOrDefaultAsync(x => x.Id == id);

            if (encuesta == null)
            {
                return NotFound();
            }
            var EncuestaDTO = mapper.Map<EncuestaDTO>(encuesta);
            return EncuestaDTO;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Encuesta encuesta)
        {
            context.Encuestas.Add(encuesta);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerAutor", new { id =encuesta.Id }, encuesta);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Encuesta value)
        {
            if (id != value.Id)
            {
                return BadRequest();
            }

            context.Entry(value).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Encuesta> Delete(int id)
        {
            var autor = context.Encuestas.FirstOrDefault(x => x.Id == id);

            if (autor == null)
            {
                return NotFound();
            }

            context.Encuestas.Remove(autor);
            context.SaveChanges();
            return autor;
        }
    }
}
