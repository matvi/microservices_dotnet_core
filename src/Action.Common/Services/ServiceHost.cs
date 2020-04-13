using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using RawRabbit;
using Action.Common.Commands;
using Action.Common.Events;
using Action.Common.RabbitMq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Action.Common.Services
{
    public class ServiceHost : IServiceHost
    {
        private readonly IWebHost _webHost;
        public void Run()
        {
            _webHost.Run();
        }

        public ServiceHost(IWebHost webHost){
            _webHost = webHost;
        }

        public static HostBuilder Create<TStartup>(string[] args) where TStartup : class
        {
            Console.Title = typeof(TStartup).Namespace;
            var config = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddCommandLine(args)
            .Build();

            var webHostBilder = WebHost
                .CreateDefaultBuilder(args)
                .UseConfiguration(config)
                .UseStartup<TStartup>();
        

            return new HostBuilder(webHostBilder.Build());

        }

        public abstract class BuilderBase{
            public abstract ServiceHost Build();
        }

        public class HostBuilder : BuilderBase
        {
            private readonly IWebHost _webHost;
            private IBusClient _bus;
            public HostBuilder(IWebHost webHost)
            {
                _webHost = webHost;
            }

            public BusBuilder UserRabbitMq()
            {
                _bus = (IBusClient)_webHost.Services.GetService(typeof(IBusClient));
                return new BusBuilder(_webHost, _bus);
            }

            public override ServiceHost Build()
            {
                return new ServiceHost(_webHost);
            }
        }

        public class BusBuilder : BuilderBase
        {
            private readonly IWebHost _webHost;
            private IBusClient _bus;

            public BusBuilder(IWebHost webHost, IBusClient bus)
            {
                _webHost = webHost;
                _bus = bus;
            }

            public BusBuilder SubscribeToCommand<TCommand>() where TCommand: ICommand
            {
                using (var serviceScope = _webHost.Services.CreateScope())
                {
                    var service = serviceScope.ServiceProvider;

                    try
                    {
                        var handler = service.GetRequiredService<ICommandHandler<TCommand>>();
                        _bus.WithCommandHandlerAsync(handler);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }

            return this;
            //     var handler = (ICommandHandler<TCommand>) _webHost.Services
            //     .GetService(typeof(ICommandHandler<TCommand>));
            //     _bus.WithCommandHandlerAsync(handler);
            //     return this;
            }

            public BusBuilder SubscribeToEvent<TEvemt>() where TEvemt: IEvent
            {
                using (var serviceScope = _webHost.Services.CreateScope())
                {
                    var service = serviceScope.ServiceProvider;

                    try
                    {
                        var handler = service.GetRequiredService<IEventHandler<TEvemt>>();
                        _bus.WithEventHandlerAsync(handler);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }

                return this;
                // var handler = (IEventHandler<TEvemt>) _webHost.Services
                // .GetService(typeof(IEventHandler<TEvemt>));
                // _bus.WithEventHandlerAsync(handler);
                // return this;
            }

            public override ServiceHost Build()
            {
                return new ServiceHost(_webHost);
            }
        }
    }
}