using ApiRestfullSurvey.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestfullSurvey.Models
{
    public class ResultadoDTO
    {
        [Key]
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public System.DateTime Fecha { get; set; }
        public List<DetalleResultado> DetalleResultado { get; set; }

    }
}
