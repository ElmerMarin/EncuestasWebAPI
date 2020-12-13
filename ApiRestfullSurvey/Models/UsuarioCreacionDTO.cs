using ApiRestfullSurvey.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestfullSurvey.Models
{
    public class UsuarioCreacionDTO
    {
        public string TipoUsuario { get; set; }
        public string Correo { get; set; }
        public string NombreUsuario { get; set; }

        public List<CoordinadorCreacionDTO> Coordinador { get; set; }
        public List<EncuestadoCreacionDTO> Encuestado { get; set; }
        public List<ResultadoCreacionDTO> Resultado { get; set; }
    }
}
