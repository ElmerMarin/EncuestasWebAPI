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
    public class DetalleResultadosController : ControllerBase
    {
        // GET: api/<DetalleResultadosController>
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public DetalleResultadosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleResultadoDTO>>> GetDetalleResultado()
        {

            var detalleResultado = await context.DetalleResultados.ToListAsync();
            var detalleResultadoDTO = mapper.Map<List<DetalleResultadoDTO>>(detalleResultado);
            return detalleResultadoDTO;
        }

        [HttpGet("Primer")]
        public ActionResult<DetalleResultado> GetPrimerDetalleResultado()
        {
            return context.DetalleResultados.FirstOrDefault();
        }

        [HttpGet("{id}", Name = "ObtenerDetalleResultado")]
        public async Task<ActionResult<DetalleResultadoDTO>> GetDetalleResultado(int id, string param2)
        {
            var detalleResultado = await context.DetalleResultados.FirstOrDefaultAsync(x => x.Id == id);

            if (detalleResultado == null)
            {
                return NotFound();
            }
            var detalleResultadoDTO = mapper.Map<DetalleResultadoDTO>(detalleResultado);
            return detalleResultadoDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DetalleResultadoCreacionDTO detalleResultadoCreacion)
        {
            var detalleResultado = mapper.Map<DetalleResultado>(detalleResultadoCreacion);
            context.DetalleResultados.Add(detalleResultado);
            await context.SaveChangesAsync();
            var detalleResultadoDTO = mapper.Map<DetalleResultadoDTO>(detalleResultado);
            return new CreatedAtRouteResult("ObtenerDetalleResultado", new { id = detalleResultado.Id }, detalleResultadoDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] DetalleResultadoCreacionDTO detalleResultadoActualizacion)
        {

            var detalleResultado = mapper.Map<DetalleResultado>(detalleResultadoActualizacion);
            detalleResultado.Id = id;
            context.Entry(detalleResultado).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<DetalleResultadoCreacionDTO> pathDocument)
        {
            if (pathDocument == null)
            {
                return BadRequest();
            }

            var detalleResultadoDB = await context.DetalleResultados.FirstOrDefaultAsync(x => x.Id == id);

            if (detalleResultadoDB == null)
            {
                return NotFound();
            }

            var detalleResultadoDTO = mapper.Map<DetalleResultadoCreacionDTO>(detalleResultadoDB);

            pathDocument.ApplyTo(detalleResultadoDTO, ModelState);

            mapper.Map(detalleResultadoDTO, detalleResultadoDB);

            var isValid = TryValidateModel(detalleResultadoDB);

            if (!isValid)
            {
                return BadRequest(ModelState);

            }

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DetalleResultado>> Delete(int id)
        {
            var detalleResultadoId = await context.DetalleResultados.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);

            if (detalleResultadoId == default(int))
            {
                return NotFound();
            }
            context.DetalleResultados.Remove(new DetalleResultado { Id = detalleResultadoId });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
