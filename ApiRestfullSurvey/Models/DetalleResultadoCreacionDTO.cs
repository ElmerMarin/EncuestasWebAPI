using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestfullSurvey.Models
{
    public class DetalleResultadoCreacionDTO
    {
        public int EncuestaId { get; set; }
        public int ResultadoId { get; set; }
        public string Valor { get; set; }
    }
}
