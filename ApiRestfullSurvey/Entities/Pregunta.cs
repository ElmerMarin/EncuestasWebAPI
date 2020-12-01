using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestfullSurvey.Entities
{
    public class Pregunta
    {
        [Key]
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public int IdEncuesta { get; set; }

        public virtual Encuesta Encuesta { get; set; }
        public virtual ICollection<Respuesta> Respuesta { get; set; }
    }
}
