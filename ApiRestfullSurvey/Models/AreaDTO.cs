using ApiRestfullSurvey.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestfullSurvey.Models
{
    public class AreaDTO
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<CategoriaDTO> Categoria { get; set; }
    }
}
