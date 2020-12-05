using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestfullSurvey.Models
{
    public class RespuestaDTO
    {
        [Key]
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int PreguntaId { get; set; }

    }
}
