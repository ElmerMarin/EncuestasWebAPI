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
        public int EncuestaId { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public Encuesta Encuesta { get; set; }
    }
}
