using Microsoft.Extensions.DependencyInjection;
using RedisSample.DataDomain.Interfaces;
using RedisSample.DataDomain.Data;
using RedisSample.App.Commands;
using System;
using RedisSample.DataDomain.Models;
using MediatR;
using System.Threading.Tasks;
using RedisSample.DataDomain.Extensions;
using RedisSample.App.Queries;
using System.Diagnostics;
using StackExchange.Redis;

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
                
                var sw = new Stopwatch();
                sw.Start();
                var listWithoutCache = await powQuerie.GetListWithoutCache();
                sw.Stop();
                Console.WriteLine($"Time to get list from database where cache is not implemented: {sw.ElapsedMilliseconds}ms");

                sw.Restart();
                var listWithCache = await powQuerie.GetList();                
                sw.Stop();
                Console.WriteLine($"Time to get list from database where cache is implemented: {sw.ElapsedMilliseconds}ms");                

                Console.ReadLine();

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
            services.AddStackExchangeRedisCache(action =>
            {
                action.InstanceName = "WhatYouWantToNameIt";
                action.Configuration = "127.0.0.1:6379";
            });
        }

        private static async Task SendCommand(Command command, IMediator mediator)
        {            
            var result = await mediator.Send(command);

            Console.WriteLine($"Result is {result}.");            
        }
    }
}

