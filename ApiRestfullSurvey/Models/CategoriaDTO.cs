﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestfullSurvey.Models
{
    public class CategoriaDTO
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int AreaId { get; set; }
    }
}
