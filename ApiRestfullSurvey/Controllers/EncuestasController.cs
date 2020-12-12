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
    public class EncuestasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public EncuestasController(ApplicationDbContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EncuestaDTO>>>Get()
        {

            var encuestas = await context.Encuestas.ToListAsync();
            var encuestasDTO = mapper.Map<List<EncuestaDTO>>(encuestas);
            return encuestasDTO;
        }

        [HttpGet("Primer")]
        public ActionResult<Encuesta> GetPrimeraEncuesta()
        {
            return context.Encuestas.FirstOrDefault();
        }

        [HttpGet("{id}",Name = "ObtenerEncuesta")]
        public async Task<ActionResult<EncuestaDTO>> GetEncuesta(int id, string param2)
        {
            var encuesta = await context.Encuestas.FirstOrDefaultAsync(x => x.Id == id);

            if (encuesta == null)
            {
                return NotFound();
            }
            var encuestaDTO = mapper.Map<EncuestaDTO>(encuesta);
            return encuestaDTO;
        }

        [HttpPost]
        public async Task<ActionResult>Post([FromBody] EncuestaCreacionDTO encuestaCreacion)
        {
            var encuesta = mapper.Map<Encuesta>(encuestaCreacion);
            context.Encuestas.Add(encuesta);
            await context.SaveChangesAsync();
            var encuestaDTO = mapper.Map<EncuestaDTO>(encuesta);
            return new CreatedAtRouteResult("ObtenerEncuesta", new { id =encuesta.Id }, encuestaDTO);
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
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<EncuestaCreacionDTO> pathDocument)
        {
            if (pathDocument==null) 
            {
                return BadRequest();
            }

            var encuestaDB = await context.Encuestas.FirstOrDefaultAsync(x => x.Id == id);

            if (encuestaDB == null) 
            {
                return NotFound();
            }

            var encuestaDTO = mapper.Map<EncuestaCreacionDTO>(encuestaDB);

            pathDocument.ApplyTo(encuestaDTO, ModelState);

            mapper.Map(encuestaDTO, encuestaDB);

            var isValid = TryValidateModel(encuestaDB);

            if (!isValid) {
                return BadRequest(ModelState);
            
            }

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Encuesta>> Delete(int id)
        {
            var encuestaId = await context.Encuestas.Select(x=>x.Id).FirstOrDefaultAsync(x => x == id);

            if (encuestaId == default(int))
            {
                return NotFound();
            }
            context.Encuestas.Remove(new Encuesta {Id=encuestaId});
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
