using ApiRestfullSurvey.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestfullSurvey.Models
{
    public class UsuarioDTO
    {
        [Key]
        public int Id { get; set; }
        public string TipoUsuario { get; set; }
        public string Correo { get; set; }
        public string NombreUsuario { get; set; }

        public List<Coordinador> Coordinador { get; set; }
        public List<Encuestado> Encuestado { get; set; }
        public List<Resultado> Resultado { get; set; }
    }
}
