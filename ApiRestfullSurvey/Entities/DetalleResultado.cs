﻿using System;
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
        public int EncuestaId { get; set; }
        public int ResultadoId { get; set; }
        public string Valor { get; set; }

        public Encuesta Encuesta { get; set; }
        public Resultado Resultado { get; set; }
    }
}
