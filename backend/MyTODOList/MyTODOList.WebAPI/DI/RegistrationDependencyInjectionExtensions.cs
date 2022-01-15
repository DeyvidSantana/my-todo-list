using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyTODOList.Data;
using MyTODOList.Entities;
using MyTODOList.Repository;
using MyTODOList.Repository.Repositories;
using MyTODOList.RepositoryImpl;
using MyTODOList.RepositoryImpl.Repositories;
using MyTODOList.Services;
using MyTODOList.Services.Services;
using MyTODOList.ServicesImpl;
using MyTODOList.ServicesImpl.Services;

namespace MyTODOList.WebAPI.DI
{
    public static class RegistrationDependencyInjectionExtensions
    {
        public static void AddRegistrationDependencies(this IServiceCollection services)
        {
            RegisterDbContext(services);
            RegisterRepositories(services);
            RegisterServices(services);
            RegisterSwagger(services);
            RegisterCors(services);
        }
        private static void RegisterDbContext(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Database"));
            services.AddScoped<DataContext, DataContext>();
        }
        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IRepository<IEntity>, Repository<IEntity>>();
            services.AddScoped<ITarefaRepository, TarefaRepository>();
        }
        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IService, Service>();
            services.AddScoped<ITarefaService, TarefaService>();
            services.AddScoped<IValidacaoTarefaService, ValidacaoTarefaService>();
        }
        private static void RegisterSwagger(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        private static void RegisterCors(IServiceCollection services)
        {
            services.AddCors();
        }
    }
}
