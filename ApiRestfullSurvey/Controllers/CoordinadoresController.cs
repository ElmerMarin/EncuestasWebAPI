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
    public class CoordinadoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public CoordinadoresController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("/listadoCoordinador")]
        [HttpGet("listadoCoordinador")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CoordinadorDTO>>> GetCoordinadorAll()
        {

            var coordinadores = await context.Coordinadores.ToListAsync();
            var coordinadoresDTO = mapper.Map<List<CoordinadorDTO>>(coordinadores);
            return coordinadoresDTO;
        }

        [HttpGet("Primer")]
        public ActionResult<Coordinador> GetPrimerCoordinador()
        {
            return context.Coordinadores.FirstOrDefault();
        }

        [HttpGet("{id}", Name = "ObtenerCoordinador")]
        public async Task<ActionResult<CoordinadorDTO>> GetCoordinador(int id, string param2)
        {
            var coordinador = await context.Coordinadores.FirstOrDefaultAsync(x => x.IdCoordinador == id);

            if (coordinador == null)
            {
                return NotFound();
            }
            var coordinadorDTO = mapper.Map<CoordinadorDTO>(coordinador);
            return coordinadorDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CoordinadorCreacionDTO coordinadorCreacion)
        {
            var coordinador = mapper.Map<Coordinador>(coordinadorCreacion);
            context.Coordinadores.Add(coordinador);
            await context.SaveChangesAsync();
            var coordinadorDTO = mapper.Map<CoordinadorDTO>(coordinador);
            return new CreatedAtRouteResult("ObtenerCoordinador", new { id = coordinador.IdCoordinador }, coordinadorDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CoordinadorCreacionDTO coordinadorActualizacion)
        {

            var coordinador = mapper.Map<Coordinador>(coordinadorActualizacion);
            coordinador.IdCoordinador = id;
            context.Entry(coordinador).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<CoordinadorCreacionDTO> pathDocument)
        {
            if (pathDocument == null)
            {
                return BadRequest();
            }

            var coordinadorDB = await context.Coordinadores.FirstOrDefaultAsync(x => x.IdCoordinador == id);

            if (coordinadorDB == null)
            {
                return NotFound();
            }

            var coordinadorDTO = mapper.Map<CoordinadorCreacionDTO>(coordinadorDB);

            pathDocument.ApplyTo(coordinadorDTO, ModelState);

            mapper.Map(coordinadorDTO, coordinadorDB);

            var isValid = TryValidateModel(coordinadorDB);

            if (!isValid)
            {
                return BadRequest(ModelState);

            }

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Coordinador>> Delete(int id)
        {
            var coordinadorId = await context.Coordinadores.Select(x => x.IdCoordinador).FirstOrDefaultAsync(x => x == id);

            if (coordinadorId == default(int))
            {
                return NotFound();
            }
            context.Coordinadores.Remove(new Coordinador { IdCoordinador = coordinadorId });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
