using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyTODOList.Data;
using MyTODOList.Entities;
using MyTODOList.Repository;
using MyTODOList.Repository.Repositories;
using MyTODOList.RepositoryImpl.Repositories;
using MyTODOList.Services;
using MyTODOList.Services.Services;
using MyTODOList.ServicesImpl;
using MyTODOList.ServicesImpl.Services;
using System;

namespace MyTODOList.WebAPI
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
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My TODO List API",
                    Version = "v1",
                    Description = "My TODO List API.",
                    Contact = new OpenApiContact
                    {
                        Name = "Deyvid Santana",
                        Email = string.Empty
                    },
                });
            });

            ConfigureDependencies(services);

            services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Database"));
            services.AddScoped<DataContext, DataContext>();
            
            services.AddCors();
        }        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Zomato API V1");
                c.RoutePrefix = string.Empty;
            });
        }

        private void ConfigureDependencies(IServiceCollection services)
        {
            ConfigureRepositoryDependencies(services);
            ConfigureServiceDependencies(services);
        }

        private void ConfigureRepositoryDependencies(IServiceCollection services)
        {
            services.AddScoped<IRepository<IEntity>, RepositoryImpl.Repository<IEntity>>();
            services.AddScoped<ITarefaRepository, TarefaRepository>();
        }

        private void ConfigureServiceDependencies(IServiceCollection services)
        {
            services.AddScoped<IService, Service>();
            services.AddScoped<ITarefaService, TarefaService>();
        }
    }
}
