﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestfullSurvey.Entities
{
    public class Respuesta
    {
        [Key]
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public int PreguntaId { get; set; }

        public Pregunta Pregunta { get; set; }
    }
}
