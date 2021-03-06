﻿using ApiRestfullSurvey.Contexts;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PreguntaDTO>>> GetPreguntaAll()
        {

            var preguntas = await context.Preguntas.ToListAsync();
            var preguntaDTO = mapper.Map<List<PreguntaDTO>>(preguntas);
            return preguntaDTO;
        }

        [HttpGet("{id}", Name = "ObtenerPregunta")]
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
        public async Task<ActionResult> Post([FromBody] PreguntaCreacionDTO preguntaCreacion)
        {
            var pregunta = mapper.Map<Pregunta>(preguntaCreacion);
            context.Preguntas.Add(pregunta);
            await context.SaveChangesAsync();
            var preguntaDTO = mapper.Map<PreguntaDTO>(pregunta);
            return new CreatedAtRouteResult("ObtenerPregunta", new { id = pregunta.Id }, preguntaDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] PreguntaCreacionDTO preguntaActualizacion)
        {

            var pregunta = mapper.Map<Pregunta>(preguntaActualizacion);
            pregunta.Id = id;
            context.Entry(pregunta).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<PreguntaCreacionDTO> pathDocument)
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

            var preguntaDTO = mapper.Map<PreguntaCreacionDTO>(preguntaDB);

            pathDocument.ApplyTo(preguntaDTO, ModelState);

            mapper.Map(preguntaDTO, preguntaDB);

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
            var preguntaId = await context.Preguntas.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);

            if (preguntaId == default(int))
            {
                return NotFound();
            }
            context.Preguntas.Remove(new Pregunta { Id = preguntaId });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
