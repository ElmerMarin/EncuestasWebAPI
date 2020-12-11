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
    public class AreasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public AreasController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        [HttpGet("/listado")]
        [HttpGet("listado")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AreaDTO>>> Get()
        {

            var areas = await context.Areas.ToListAsync();
            var areasDTO = mapper.Map<List<AreaDTO>>(areas);
            return areasDTO;
        }

        [HttpGet("Primer")]
        public ActionResult<Area> GetPrimeraArea()
        {
            return context.Areas.FirstOrDefault();
        }

        [HttpGet("{id}", Name = "ObtenerArea")]
        public async Task<ActionResult<AreaDTO>> GetArea(int id, string param2)
        {
            var area = await context.Areas.FirstOrDefaultAsync(x => x.Id == id);

            if (area == null)
            {
                return NotFound();
            }
            var areaDTO = mapper.Map<AreaDTO>(area);
            return areaDTO;
        }

        // POST api/<AreasController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AreaCreacionDTO areaCreacion)
        {
            var area = mapper.Map<Area>(areaCreacion);
            context.Areas.Add(area);
            await context.SaveChangesAsync();
            var areaDTO = mapper.Map<AreaDTO>(area);
            return new CreatedAtRouteResult("ObtenerArea", new { id = area.Id }, areaDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] AreaCreacionDTO areaActualizacion)
        {

            var area = mapper.Map<Area>(areaActualizacion);
            area.Id = id;
            context.Entry(area).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument <AreaCreacionDTO> pathDocument)
        {
            if (pathDocument == null)
            {
                return BadRequest();
            }

            var areaDB = await context.Areas.FirstOrDefaultAsync(x => x.Id == id);

            if (areaDB == null)
            {
                return NotFound();
            }

            var areaDTO = mapper.Map<AreaCreacionDTO>(areaDB);

            pathDocument.ApplyTo(areaDTO, ModelState);

            mapper.Map(areaDTO, areaDB);

            var isValid = TryValidateModel(areaDB);

            if (!isValid)
            {
                return BadRequest(ModelState);

            }

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Area>> Delete(int id)
        {
            var areaId = await context.Areas.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);

            if (areaId == default(int))
            {
                return NotFound();
            }
            context.Areas.Remove(new Area { Id = areaId });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
