using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;
using System;

namespace Action.Common.mongo
{
    public static class Extentions
    {
        public static void AddMongoDB(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoOptions>(configuration.GetSection("mongo"));
            services.AddSingleton<IMongoClient>(c =>
            {
                var options = c.GetRequiredService<IOptions<MongoOptions>>();
                return new MongoClient(options.Value.ConnectionString);
            });

            services.AddScoped<IMongoDatabase>(c => 
            {
                var options = c.GetRequiredService<IOptions<MongoOptions>>();
                var client = c.GetRequiredService<IMongoClient>();
                return client.GetDatabase(options.Value.DataBase);
            });

            services.AddScoped<IDatabaseInitializer,MongoDatabaseInitilizer>();
            services.AddScoped<IDatabaseSeeder, MongoSeeder>();
        }

        public static void InitilizeDatabase(this IApplicationBuilder builder)
        {
            using (var serviceScope = builder.ApplicationServices.CreateScope())
            {
                var service = serviceScope.ServiceProvider;

                try
                {
                    service.GetRequiredService<IDatabaseInitializer>().InitializeAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}