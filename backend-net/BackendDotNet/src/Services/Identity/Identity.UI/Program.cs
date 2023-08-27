// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using AppLogic.Events;
using Identity.UI.Events;
using Infrastructure.EventBus.RabbitMQ;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;

namespace Identity.UI
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()
                // uncomment to write to Azure diagnostics stream
                //.WriteTo.File(
                //    @"D:\home\LogFiles\Application\identityserver.txt",
                //    fileSizeLimitBytes: 1_000_000,
                //    rollOnFileSizeLimit: true,
                //    shared: true,
                //    flushToDiskInterval: TimeSpan.FromSeconds(1))
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
                .CreateLogger();

            //static classe maybe
            Console.WriteLine("test"); //code graakt hier dus maybe via dit de rabbit mq

            //rabbit mq test
            //SendCustomers.sendCustomers();
            


            try
            {
                Log.Information("Starting host...");
                var host = CreateHostBuilder(args).Build();//.Run();
                var eventBus = host.Services.GetService<IEventBus>();
                //SendCustomers.sendCustomers(eventBus);

                //---

                var @event = new UserRegisteredIntegrationEvent
                {
                    CustomerId = "1f974d9f-41d3-4b86-b8d5-058859808534",
                    Name = "alice"
                };
                eventBus.Publish(@event);

                @event = new UserRegisteredIntegrationEvent
                {
                    CustomerId = "dba5b341-aadb-4fba-9824-8850db4bc5b5",
                    Name = "bob"
                };
                eventBus.Publish(@event);

                //--


                host.Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly.");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


    }
}