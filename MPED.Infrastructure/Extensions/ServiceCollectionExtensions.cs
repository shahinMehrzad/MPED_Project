using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MPED.Application.Interfaces.Repositories;
using MPED.Infrastructure.Repositories;
using System.Reflection;

namespace MPED.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPersistenceContexts(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            #region Repositories

            services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));

            services.AddTransient<IRoomsRepository, RoomsRepository>();
            
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            #endregion Repositories
        }
    }
}
