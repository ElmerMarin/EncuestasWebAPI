using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestfullSurvey.Entities
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string TipoUsuario { get; set; }
        public string Correo { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public string Token { get; set; }

        public virtual ICollection<Coordinador> Coordinador { get; set; }
        public virtual ICollection<Encuestado> Encuestado { get; set; }
        public virtual ICollection<Resultado> Resultado { get; set; }
    }
}
