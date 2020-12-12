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
    public class ResultadosController : ControllerBase
    {
        // GET: api/<ResultadosController>
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public ResultadosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResultadoDTO>>> GetResultadoAll()
        {

            var resultados = await context.Resultados.ToListAsync();
            var resultadoDTO = mapper.Map<List<ResultadoDTO>>(resultados);
            return resultadoDTO;
        }

        [HttpGet("Primer")]
        public ActionResult<Resultado> GetPrimerResultado()
        {
            return context.Resultados.FirstOrDefault();
        }

        [HttpGet("{id}", Name = "ObtenerResultado")]
        public async Task<ActionResult<ResultadoDTO>> GetResultado(int id, string param2)
        {
            var resultado = await context.Resultados.FirstOrDefaultAsync(x => x.Id == id);

            if (resultado == null)
            {
                return NotFound();
            }
            var resultadoDTO = mapper.Map<ResultadoDTO>(resultado);
            return resultadoDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ResultadoCreacionDTO resultadoCreacion)
        {
            var resultado = mapper.Map<Resultado>(resultadoCreacion);
            context.Resultados.Add(resultado);
            await context.SaveChangesAsync();
            var resultadoDTO = mapper.Map<ResultadoDTO>(resultado);
            return new CreatedAtRouteResult("ObtenerUsuario", new { id = resultado.Id }, resultadoDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ResultadoCreacionDTO resultadoCreacion)
        {

            var resultado = mapper.Map<Resultado>(resultadoCreacion);
            resultado.Id = id;
            context.Entry(resultado).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<ResultadoCreacionDTO> pathDocument)
        {
            if (pathDocument == null)
            {
                return BadRequest();
            }

            var resultadoDB = await context.Resultados.FirstOrDefaultAsync(x => x.Id == id);

            if (resultadoDB == null)
            {
                return NotFound();
            }

            var resultadoDTO = mapper.Map<ResultadoCreacionDTO>(resultadoDB);

            pathDocument.ApplyTo(resultadoDTO, ModelState);

            mapper.Map(resultadoDTO, resultadoDB);

            var isValid = TryValidateModel(resultadoDB);

            if (!isValid)
            {
                return BadRequest(ModelState);

            }

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Resultado>> Delete(int id)
        {
            var resultadoId = await context.Resultados.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);

            if (resultadoId == default(int))
            {
                return NotFound();
            }
            context.Resultados.Remove(new Resultado { Id = resultadoId });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
