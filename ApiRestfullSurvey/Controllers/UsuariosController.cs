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
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public UsuariosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetUsuarioAll()
        {

            var usuarios = await context.Usuarios.ToListAsync();
            var usuariosDTO = mapper.Map<List<UsuarioDTO>>(usuarios);
            return usuariosDTO;
        }

        [HttpGet("Primer")]
        public ActionResult<Usuario> GetPrimerUsuario()
        {
            return context.Usuarios.FirstOrDefault();
        }

        [HttpGet("{id}", Name = "ObtenerUsuario")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuario(int id, string param2)
        {
            var usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);

            if (usuario == null)
            {
                return NotFound();
            }
            var usuarioDTO = mapper.Map<UsuarioDTO>(usuario);
            return usuarioDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UsuarioCreacionDTO usuarioCreacion)
        {
            var usuario = mapper.Map<Usuario>(usuarioCreacion);
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();
            var usuarioDTO = mapper.Map<UsuarioDTO>(usuario);
            return new CreatedAtRouteResult("ObtenerUsuario", new { id = usuario.Id }, usuarioDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UsuarioCreacionDTO usuarioActualizacion)
        {

            var usuario = mapper.Map<Usuario>(usuarioActualizacion);
            usuario.Id = id;
            context.Entry(usuario).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<UsuarioCreacionDTO> pathDocument)
        {
            if (pathDocument == null)
            {
                return BadRequest();
            }

            var usuarioDB = await context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);

            if (usuarioDB == null)
            {
                return NotFound();
            }

            var usuarioDTO = mapper.Map<UsuarioCreacionDTO>(usuarioDB);

            pathDocument.ApplyTo(usuarioDTO, ModelState);

            mapper.Map(usuarioDTO,usuarioDB);

            var isValid = TryValidateModel(usuarioDB);

            if (!isValid)
            {
                return BadRequest(ModelState);

            }

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> Delete(int id)
        {
            var usuarioId = await context.Usuarios.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);

            if (usuarioId == default(int))
            {
                return NotFound();
            }
            context.Usuarios.Remove(new Usuario { Id = usuarioId });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
