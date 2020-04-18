using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Action.Common.Commands;
using Action.Common.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Action.Services.Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceHost.Create<Startup>(args)
            .UserRabbitMq()
            .SubscribeToCommand<CreateUserCommand>()
            .Build()
            .Run();
            // CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
