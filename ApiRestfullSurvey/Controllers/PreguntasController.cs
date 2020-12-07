using ApiRestfullSurvey.Contexts;
using ApiRestfullSurvey.Entities;
using ApiRestfullSurvey.Models;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
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
    public class PreguntasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public PreguntasController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("/listado")]
        [HttpGet("listado")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PreguntaDTO>>> Get()
        {

            var preguntas = await context.Preguntas.ToListAsync();
            var preguntaDTO = mapper.Map<List<PreguntaDTO>>(preguntas);
            return preguntaDTO;
        }

        [HttpGet("{id}", Name = "ObtenerAutor")]
        public async Task<ActionResult<PreguntaDTO>> GetPregunta(int id, string param2)
        {
            var pregunta = await context.Preguntas.FirstOrDefaultAsync(x => x.Id == id);

            if (pregunta == null)
            {
                return NotFound();
            }
            var preguntaDTO = mapper.Map<PreguntaDTO>(pregunta);
            return preguntaDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EncuestaCreacionDTO encuestaCreacion)
        {
            var encuesta = mapper.Map<Encuesta>(encuestaCreacion);
            context.Encuestas.Add(encuesta);
            await context.SaveChangesAsync();
            var encuestaDTO = mapper.Map<EncuestaDTO>(encuesta);
            return new CreatedAtRouteResult("ObtenerAutor", new { id = encuesta.Id }, encuestaDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] EncuestaCreacionDTO encuestaActualizacion)
        {

            var encuesta = mapper.Map<Encuesta>(encuestaActualizacion);
            encuesta.Id = id;
            context.Entry(encuesta).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<Pregunta> pathDocument)
        {
            if (pathDocument == null)
            {
                return BadRequest();
            }

            var preguntaDB = await context.Preguntas.FirstOrDefaultAsync(x => x.Id == id);

            if (preguntaDB == null)
            {
                return NotFound();
            }

            pathDocument.ApplyTo(preguntaDB, ModelState);

            var isValid = TryValidateModel(preguntaDB);

            if (!isValid)
            {
                return BadRequest(ModelState);

            }

            await context.SaveChangesAsync();


            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Pregunta>> Delete(int id)
        {
            var autorId = await context.Preguntas.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);

            if (autorId == default(int))
            {
                return NotFound();
            }
            context.Preguntas.Remove(new Pregunta { Id = autorId });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
