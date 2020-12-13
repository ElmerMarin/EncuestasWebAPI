using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestfullSurvey.Models
{
    public class PreguntaCreacionDTO
    {

        public string Descripcion { get; set; }
        public int EncuestaId { get; set; }
        public List<RespuestaCreacionDTO> Respuesta { get; set; }

    }
}
