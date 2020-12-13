using ApiRestfullSurvey.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestfullSurvey.Models
{
    public class ResultadoCreacionDTO
    {

        public int UsuarioId { get; set; }
        public System.DateTime Fecha { get; set; }
        public List<DetalleResultadoCreacionDTO> DetalleResultado { get; set; }
    }
}
