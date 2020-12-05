using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestfullSurvey.Entities
{
    public class Resultado
    {
        [Key]
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Hora_Inicio { get; set; }
        public string Hora_Final { get; set; }
        public System.DateTime Fecha { get; set; }

        public List<DetalleResultado> DetalleResultado { get; set; }
        public Usuario Usuario { get; set; }
    }
}
