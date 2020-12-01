using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestfullSurvey.Entities
{
    public class DetalleResultado
    {
        [Key]
        public int Id { get; set; }
        public int IdEncuesta { get; set; }
        public int IdResultado { get; set; }
        public string Valor { get; set; }

        public virtual Encuesta Encuesta { get; set; }
        public virtual Resultado Resultado { get; set; }
    }
}
