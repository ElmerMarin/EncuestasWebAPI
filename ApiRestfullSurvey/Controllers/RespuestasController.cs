using ApiRestfullSurvey.Contexts;
using ApiRestfullSurvey.Entities;
using ApiRestfullSurvey.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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
    public class RespuestasController : Controller
    {
        // GET: RespuestasController
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public RespuestasController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RespuestaDTO>>> GetRespuestaAll()
        {

            var respuestas = await context.Respuestas.ToListAsync();
            var respuestasDTO = mapper.Map<List<RespuestaDTO>>(respuestas);
            return respuestasDTO;
        }

        [HttpGet("{id}", Name = "ObtenerRespuesta")]
        public async Task<ActionResult<RespuestaDTO>> GetRespuesta(int id, string param2)
        {
            var respuesta = await context.Respuestas.FirstOrDefaultAsync(x => x.Id == id);

            if (respuesta == null)
            {
                return NotFound();
            }
            var respuestaDTO = mapper.Map<RespuestaDTO>(respuesta);
            return respuestaDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] RespuestaCreacionDTO respuestaCreacion)
        {
            var respuesta = mapper.Map<Respuesta>(respuestaCreacion);
            context.Respuestas.Add(respuesta);
            await context.SaveChangesAsync();
            var respuestaDTO = mapper.Map<RespuestaDTO>(respuesta);
            return new CreatedAtRouteResult("ObtenerRespuesta", new { id = respuesta.Id }, respuestaDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] RespuestaCreacionDTO respuestaActualizacion)
        {

            var respuesta = mapper.Map<Respuesta>(respuestaActualizacion);
            respuesta.Id = id;
            context.Entry(respuesta).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<RespuestaCreacionDTO> pathDocument)
        {
            if (pathDocument == null)
            {
                return BadRequest();
            }

            var respuestaDB = await context.Preguntas.FirstOrDefaultAsync(x => x.Id == id);

            if (respuestaDB == null)
            {
                return NotFound();
            }

            var respuestaDTO = mapper.Map<RespuestaCreacionDTO>(respuestaDB);

            pathDocument.ApplyTo(respuestaDTO, ModelState);

            mapper.Map(respuestaDTO, respuestaDB);

            var isValid = TryValidateModel(respuestaDB);

            if (!isValid)
            {
                return BadRequest(ModelState);

            }

            await context.SaveChangesAsync();


            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Respuesta>> Delete(int id)
        {
            var respuestaId = await context.Respuestas.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);

            if (respuestaId == default(int))
            {
                return NotFound();
            }
            context.Respuestas.Remove(new Respuesta { Id = respuestaId });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
