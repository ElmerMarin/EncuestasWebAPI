﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestfullSurvey.Entities
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public int AreaId { get; set; }
        public Area Area { get; set; }
    }
}
