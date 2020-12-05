using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestfullSurvey.Models
{
    public class DetalleEncuestaDTO
    {
        [Key]
        public int Id { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public int EncuestaId { get; set; }
        public int CategoriaId { get; set; }

    }
}
