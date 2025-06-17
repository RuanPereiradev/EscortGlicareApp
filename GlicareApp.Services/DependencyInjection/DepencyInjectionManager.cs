using DataConnection.Repositories;
using GlicareApp.Domain.Interfaces.Repositories;
using GlicareApp.Repositories.DataConnection;
using GlicareApp.Repositories.Mongo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GlicareApp.Services.DependencyInjection
{
    public static class DepencyInjectionManager
    {
        public static void RegisterDIServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterServices(configuration);
            services.RegisterDataConnectionServices(configuration);

        }
        private static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {

        }
        private static void RegisterDataConnectionServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMongoConnection, MongoConnection>();
            services.AddScoped<PostgreDbSession>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
