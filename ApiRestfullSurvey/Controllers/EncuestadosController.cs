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
    public class EncuestadosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public EncuestadosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EncuestadoDTO>>> GetEncuestado()
        {

            var encuestados = await context.Encuestados.ToListAsync();
            var encuestadosDTO = mapper.Map<List<EncuestadoDTO>>(encuestados);
            return encuestadosDTO;
        }

        [HttpGet("Primer")]
        public ActionResult<Encuestado> GetPrimerEncuestado()
        {
            return context.Encuestados.FirstOrDefault();
        }

        [HttpGet("{id}", Name = "ObtenerEncuestado")]
        public async Task<ActionResult<EncuestadoDTO>> GetEncuestado(int id, string param2)
        {
            var encuestado = await context.Encuestados.FirstOrDefaultAsync(x => x.IdEncuestado == id);

            if (encuestado == null)
            {
                return NotFound();
            }
            var encuestadoDTO = mapper.Map<EncuestadoDTO>(encuestado);
            return encuestadoDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EncuestadoCreacionDTO encuestadoCreacion)
        {
            var encuestado = mapper.Map<Encuestado>(encuestadoCreacion);
            context.Encuestados.Add(encuestado);
            await context.SaveChangesAsync();
            var encuestadoDTO = mapper.Map<EncuestadoDTO>(encuestado);
            return new CreatedAtRouteResult("ObtenerEncuestado", new { id = encuestado.IdEncuestado }, encuestadoDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] EncuestadoCreacionDTO encuestadoActualizacion)
        {

            var encuestado = mapper.Map<Encuestado>(encuestadoActualizacion);
            encuestado.IdEncuestado = id;
            context.Entry(encuestado).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<EncuestadoCreacionDTO> pathDocument)
        {
            if (pathDocument == null)
            {
                return BadRequest();
            }

            var encuestadoDB = await context.Encuestados.FirstOrDefaultAsync(x => x.IdEncuestado == id);

            if (encuestadoDB == null)
            {
                return NotFound();
            }

            var encuestadoDTO = mapper.Map<EncuestadoCreacionDTO>(encuestadoDB);

            pathDocument.ApplyTo(encuestadoDTO, ModelState);

            mapper.Map(encuestadoDTO, encuestadoDB);

            var isValid = TryValidateModel(encuestadoDB);

            if (!isValid)
            {
                return BadRequest(ModelState);

            }

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Encuestado>> Delete(int id)
        {
            var encuestadoId = await context.Encuestados.Select(x => x.IdEncuestado).FirstOrDefaultAsync(x => x == id);

            if (encuestadoId == default(int))
            {
                return NotFound();
            }
            context.Encuestados.Remove(new Encuestado { IdEncuestado = encuestadoId });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
