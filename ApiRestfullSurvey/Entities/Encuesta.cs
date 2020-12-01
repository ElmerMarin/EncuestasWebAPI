using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestfullSurvey.Entities
{
    public class Encuesta
    {
        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }

        public virtual ICollection<DetalleEncuesta> DetalleEncuesta { get; set; }
        public virtual ICollection<DetalleResultado> DetalleResultado { get; set; }
        public virtual ICollection<Pregunta> Preguntas { get; set; }
    }
}
