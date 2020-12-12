using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestfullSurvey.Models
{
    public class DetalleEncuestaCreacionDTO
    {
        public System.DateTime FechaInicio { get; set; }
        public int EncuestaId { get; set; }
        public int CategoriaId { get; set; }
    }
}
