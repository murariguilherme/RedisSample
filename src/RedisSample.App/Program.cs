using Microsoft.Extensions.DependencyInjection;
using RedisSample.DataDomain.Interfaces;
using RedisSample.DataDomain.Data;
using RedisSample.App.Commands;
using System;
using RedisSample.DataDomain.Models;
using MediatR;
using System.Threading.Tasks;
using RedisSample.DataDomain.Extensions;
using EasyCaching.Core.Configurations;
using RedisSample.App.Queries;

namespace RedisSample.App
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            RegisterServices(services);
            var serviceProvider = services.BuildServiceProvider();
            var _mediator = serviceProvider.GetService<IMediator>();
            var powQuerie = serviceProvider.GetService<IPieceOfWorkQuerie>();

            try
            {                                
                var command = new AddPeaceOfWorkCommand("Do something", DateTime.Now, new Employee("Test name"));
                await SendCommand(command, _mediator);
                
                var powList = await powQuerie.GetList();
                var pow = powList[0];
                var powCache = await powQuerie.GetById(pow.Id);
            }
            catch (DomainException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.ReadLine();
            serviceProvider.Dispose();

        }

        private static void RegisterServices(ServiceCollection services)
        {
            
            services.AddDbContext<AppDbContext>();
            services.AddScoped<AppDbContext>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IPieceOfWorkRepository, PieceOfWorkRepository>();
            services.AddScoped<IPieceOfWorkQuerie, PieceOfWorkQuerie>();
            services.AddScoped<IRequestHandler<AddPeaceOfWorkCommand, bool>, PeaceOfWorkCommandHandler>();            
            services.AddMediatR(typeof(Program));
            services.AddEasyCaching(options => options.UseRedis(config =>
            {
                config.DBConfig.Endpoints.Add(new ServerEndPoint("localhost", 6379));
                config.DBConfig.AllowAdmin = true;
            },
            "redis1"
            ));
        }

        private static async Task SendCommand(Command command, IMediator mediator)
        {            
            var result = await mediator.Send(command);

            Console.WriteLine($"Result is {result}.");            
        }
    }
}

