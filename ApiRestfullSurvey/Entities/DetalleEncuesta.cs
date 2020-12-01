using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestfullSurvey.Entities
{
    public class DetalleEncuesta
    {
        [Key]
        public int Id { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public System.DateTime Fechafinal { get; set; }
        public string Estado { get; set; }
        public int IdEncuesta { get; set; }
        public int IdArea { get; set; }
        public int IdCategoria { get; set; }

        public virtual Area Area { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual Encuesta Encuesta { get; set; }
    }
}
