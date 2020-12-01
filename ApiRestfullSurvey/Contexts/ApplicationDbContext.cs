using ApiRestfullSurvey.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestfullSurvey.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Area> Areas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Coordinador> Coordinadores { get; set; }

        public DbSet<DetalleEncuesta> DetalleEncuestas { get; set; }

        public DbSet<DetalleResultado> DetalleResultados { get; set; }

        public DbSet<Encuesta> Encuestas { get; set; }

        public DbSet<Encuestado> Encuestados { get; set; }

        public DbSet<Pregunta> Preguntas { get; set; }

        public DbSet<Respuesta> Respuestas { get; set; }

        public DbSet<Resultado> Resultados { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }



    }
}
