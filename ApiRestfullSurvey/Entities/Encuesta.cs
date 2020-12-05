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

        public List<DetalleEncuesta> DetalleEncuesta { get; set; }
        public List<DetalleResultado> DetalleResultado { get; set; }
        public List<Pregunta> Preguntas { get; set; }
    }
}
