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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiRestfullSurvey.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleEncuestasController : ControllerBase
    {
        // GET: api/<DetalleEncuestasController>
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public DetalleEncuestasController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("/listadoDetalleEncuesta")]
        [HttpGet("listadoDetalleEncuesta")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleEncuestaDTO>>> GetDetalleEncuesta()
        {

            var detalleEncuesta = await context.DetalleEncuestas.ToListAsync();
            var detalleEncuestaDTO = mapper.Map<List<DetalleEncuestaDTO>>(detalleEncuesta);
            return detalleEncuestaDTO;
        }

        [HttpGet("Primer")]
        public ActionResult<DetalleEncuesta> GetPrimerDetalleEncuesta()
        {
            return context.DetalleEncuestas.FirstOrDefault();
        }

        [HttpGet("{id}", Name = "ObtenerDetalleEncuesta")]
        public async Task<ActionResult<DetalleEncuestaDTO>> GetDetalleEncuesta(int id, string param2)
        {
            var detalleEncuesta = await context.DetalleEncuestas.FirstOrDefaultAsync(x => x.Id == id);

            if (detalleEncuesta == null)
            {
                return NotFound();
            }
            var detalleEncuestaDTO = mapper.Map<DetalleEncuestaDTO>(detalleEncuesta);
            return detalleEncuestaDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DetalleEncuestaCreacionDTO detalleEncuestaCreacion)
        {
            var detalleEncuesta = mapper.Map<DetalleEncuesta>(detalleEncuestaCreacion);
            context.DetalleEncuestas.Add(detalleEncuesta);
            await context.SaveChangesAsync();
            var detalleEncuestaDTO = mapper.Map<DetalleEncuestaDTO>(detalleEncuesta);
            return new CreatedAtRouteResult("ObtenerDetalleEncuesta", new { id = detalleEncuesta.Id }, detalleEncuestaDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] DetalleEncuestaDTO detalleEncuestaActualizacion)
        {

            var detalleEncuesta = mapper.Map<DetalleEncuesta>(detalleEncuestaActualizacion);
            detalleEncuesta.Id = id;
            context.Entry(detalleEncuesta).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<DetalleEncuestaCreacionDTO> pathDocument)
        {
            if (pathDocument == null)
            {
                return BadRequest();
            }

            var detalleEncuestaDB = await context.DetalleEncuestas.FirstOrDefaultAsync(x => x.Id == id);

            if (detalleEncuestaDB == null)
            {
                return NotFound();
            }

            var detalleEncuestaDTO = mapper.Map<DetalleEncuestaCreacionDTO>(detalleEncuestaDB);

            pathDocument.ApplyTo(detalleEncuestaDTO, ModelState);

            mapper.Map(detalleEncuestaDTO, detalleEncuestaDB);

            var isValid = TryValidateModel(detalleEncuestaDB);

            if (!isValid)
            {
                return BadRequest(ModelState);

            }

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DetalleEncuesta>> Delete(int id)
        {
            var detalleEncuestaId = await context.DetalleEncuestas.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);

            if (detalleEncuestaId == default(int))
            {
                return NotFound();
            }
            context.DetalleEncuestas.Remove(new DetalleEncuesta { Id = detalleEncuestaId });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
