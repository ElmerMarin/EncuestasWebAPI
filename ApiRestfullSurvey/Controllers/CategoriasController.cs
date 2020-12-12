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
    public class CategoriasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public CategoriasController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("/listadoCategoria")]
        [HttpGet("listadoCategoria")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriaAll()
        {

            var categorias = await context.Categorias.ToListAsync();
            var categoriasDTO = mapper.Map<List<CategoriaDTO>>(categorias);
            return categoriasDTO;
        }

        [HttpGet("Primer")]
        public ActionResult<Categoria> GetPrimeraCategoria()
        {
            return context.Categorias.FirstOrDefault();
        }

        [HttpGet("{id}", Name = "ObtenerCategoria")]
        public async Task<ActionResult<CategoriaDTO>> GetCategoria(int id, string param2)
        {
            var categoria = await context.Categorias.FirstOrDefaultAsync(x => x.Id == id);

            if (categoria == null)
            {
                return NotFound();
            }
            var categoriaDTO = mapper.Map<CategoriaDTO>(categoria);
            return categoriaDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoriaCreacionDTO categoriaCreacion)
        {
            var categoria = mapper.Map<Categoria>(categoriaCreacion);
            context.Categorias.Add(categoria);
            await context.SaveChangesAsync();
            var categoriaDTO = mapper.Map<CategoriaDTO>(categoria);
            return new CreatedAtRouteResult("ObtenerCategoria", new { id = categoria.Id }, categoriaDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CategoriaCreacionDTO categoriaActualizacion)
        {

            var categoria = mapper.Map<Categoria>(categoriaActualizacion);
            categoria.Id = id;
            context.Entry(categoria).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<CategoriaCreacionDTO> pathDocument)
        {
            if (pathDocument == null)
            {
                return BadRequest();
            }

            var categoriaDB = await context.Categorias.FirstOrDefaultAsync(x => x.Id == id);

            if (categoriaDB == null)
            {
                return NotFound();
            }

            var categoriaDTO = mapper.Map<CategoriaCreacionDTO>(categoriaDB);

            pathDocument.ApplyTo(categoriaDTO, ModelState);

            mapper.Map(categoriaDTO, categoriaDB);

            var isValid = TryValidateModel(categoriaDB);

            if (!isValid)
            {
                return BadRequest(ModelState);

            }

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Categoria>> Delete(int id)
        {
            var categoriaId = await context.Categorias.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);

            if (categoriaId == default(int))
            {
                return NotFound();
            }
            context.Categorias.Remove(new Categoria { Id = categoriaId });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
