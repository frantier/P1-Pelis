using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ApiPeliculas.Infraestructure.Data;
using ApiPeliculas.Infraestructure;
using ApiPeliculas.Domain.Entities;
using ApiPeliculas.Domain.Interfaces;
using ApiPeliculas.Infraestructure.Repositories;
using ApiPeliculas.Application.Services;
using Microsoft.AspNetCore.Http;
using FluentValidation;
using ApiPeliculas.Domain.DTOS.Request;
using ApiPeliculas.Infraestructure.Validators;

namespace ApiPeliculas.Api
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiPeliculas.Api", Version = "v1" });
            });

            services.AddDbContext<peliculaspContext>(options => options.UseSqlServer(Configuration.GetConnectionString("peliculasp")));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddScoped<PeliculaService, ServicePeliculas>();

            services.AddTransient<PeliculaRepository, PeliculasSQLRepository>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IValidator<PeliculasRequest>, PeliculasValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiPeliculas.Api v1"));
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
