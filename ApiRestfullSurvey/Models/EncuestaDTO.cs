using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestfullSurvey.Models
{
    public class EncuestaDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public List<DetalleEncuestaDTO> DetalleEncuesta { get; set; }
        public List<DetalleResultadoDTO> DetalleResultado { get; set; }
        public List<PreguntaDTO> Preguntas { get; set; }
    }
}
