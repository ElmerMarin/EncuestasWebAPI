using ApiRestfullSurvey.Contexts;
using ApiRestfullSurvey.Entities;
using ApiRestfullSurvey.Models;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestfullSurvey
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("ConexionBD"))
            );
            services.AddAutoMapper(Configuration =>
            {
                Configuration.CreateMap<Encuesta,EncuestaDTO>();
                Configuration.CreateMap<EncuestaCreacionDTO, Encuesta>().ReverseMap();
                Configuration.CreateMap<Pregunta,PreguntaDTO>();
                Configuration.CreateMap<PreguntaCreacionDTO, Pregunta>().ReverseMap();

                Configuration.CreateMap<Respuesta, RespuestaDTO>();
                Configuration.CreateMap<RespuestaCreacionDTO, Respuesta>().ReverseMap();
                Configuration.CreateMap<Usuario, UsuarioDTO>();
                Configuration.CreateMap<UsuarioCreacionDTO, Usuario>().ReverseMap();
                Configuration.CreateMap<Resultado, ResultadoDTO>();
                Configuration.CreateMap<ResultadoCreacionDTO,Resultado>().ReverseMap();
                Configuration.CreateMap<Encuestado, EncuestadoDTO>();
                Configuration.CreateMap<EncuestadoCreacionDTO, Encuestado>().ReverseMap();
                Configuration.CreateMap<DetalleResultado, DetalleResultadoDTO>();
                Configuration.CreateMap<DetalleResultadoCreacionDTO, DetalleResultado>().ReverseMap();
                Configuration.CreateMap<DetalleEncuesta, DetalleEncuestaDTO>();
                Configuration.CreateMap<DetalleEncuestaCreacionDTO, DetalleEncuesta>().ReverseMap();
                Configuration.CreateMap<Coordinador, CoordinadorDTO>();
                Configuration.CreateMap<CoordinadorCreacionDTO, Coordinador>().ReverseMap();
                Configuration.CreateMap<Categoria, CategoriaDTO>();
                Configuration.CreateMap<CategoriaCreacionDTO, Categoria>().ReverseMap();
                Configuration.CreateMap<Area, AreaDTO>();
                Configuration.CreateMap<AreaCreacionDTO, Area>().ReverseMap();
            },typeof(Startup));
            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddSwaggerGen(config =>
            {               
                config.SwaggerDoc("v1", new OpenApiInfo { Title = "Mi Web API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
