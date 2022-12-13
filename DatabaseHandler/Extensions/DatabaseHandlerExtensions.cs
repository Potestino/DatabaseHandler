using DatabaseHandler.Core;
using DatabaseHandler.Domain.Enums;
using DatabaseHandler.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace DatabaseHandler.Extensions
{
    public static class DatabaseHandlerExtensions
    {
        public static void AddDatabaseHandler(this IServiceCollection services, IConfiguration config, EDbHandlerInjectionType injectionType = EDbHandlerInjectionType.Scoped)
        {
            var configurations = new ConcurrentDictionary<string, DbHandlerConfigurationModel>();

            var connections = config.GetSection("DatabaseHandler").GetChildren();
            foreach (var connection in connections)
            {
                var type = connection["Type"];
                var connectionString = connection["ConnectionString"];

                configurations.TryAdd(connection.Key, new DbHandlerConfigurationModel(connectionString, type.ToEnum<EDbHandlerConnectionType>()));
            }

            switch (injectionType)
            {
                case EDbHandlerInjectionType.Scoped:
                    services.AddScoped<IDbHandlerFactory, DbHandlerFactory>(x => new DbHandlerFactory(configurations));
                    break;

                case EDbHandlerInjectionType.Transient:
                    services.AddTransient<IDbHandlerFactory, DbHandlerFactory>(x => new DbHandlerFactory(configurations));
                    break;

                case EDbHandlerInjectionType.Singleton:
                    services.AddSingleton<IDbHandlerFactory, DbHandlerFactory>(x => new DbHandlerFactory(configurations));
                    break;                
            }            
        }
    }
}
