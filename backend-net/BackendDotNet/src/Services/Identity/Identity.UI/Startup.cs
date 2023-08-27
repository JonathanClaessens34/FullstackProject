// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using AppLogic.Events;
using Identity.UI.Events;
using IdentityServer4;
using IdentityServerHost.Quickstart.UI;
using Infrastructure.EventBus.RabbitMQ;
using Infrastructure.EventBus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using Microsoft.Extensions.Logging;
using System;

namespace Identity.UI
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                options.EmitStaticAudienceClaim = true;
            })
                .AddTestUsers(TestUsers.Users);

            // in-memory, code config
            builder.AddInMemoryIdentityResources(Config.IdentityResources);
            builder.AddInMemoryApiScopes(Config.ApiScopes);
            builder.AddInMemoryClients(Config.Clients);
            builder.AddInMemoryApiResources(Config.ApiResources);

            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();

            //

            //builder.Services.AddRabbitMQEventBus(configuration); Dit aanpassen naar deze manier
            //wil toevoegen met dependecy injection maar het lukt nie idk why atm---------

            RabbitMQEventBusOptions options = Configuration.GetSection(RabbitMQEventBusOptions.SectionName)
               .Get<RabbitMQEventBusOptions>();

            //Register singleton that manages the event bus subscriptions -> in-memory
            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            //Register the connection with the event bus (Dees mess terug me options doen ma idk)
            var factory = new ConnectionFactory
            {
                HostName = "rabbitmq",
                DispatchConsumersAsync = true,
                UserName = "guest",
                Password = "guest"
            };
            services.AddSingleton<IRabbitMQPersistentConnection>(provider =>
            {
                var logger = provider.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
                return new DefaultRabbitMQPersistentConnection(factory, logger, 5);
            });

            //Register the event bus
            services.AddSingleton<IEventBus>(provider =>
            {
                System.Threading.Thread.Sleep(10000);

                var connection = provider.GetRequiredService<IRabbitMQPersistentConnection>();
                var logger = provider.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var subscriptionsManager = provider.GetRequiredService<IEventBusSubscriptionsManager>();
                string queueName = "identity-service";
                return new EventBusRabbitMQ(connection, logger, provider, subscriptionsManager, queueName, 5);
            });

            //---------------------------

            services.AddAuthentication();

            //  .AddGoogle(options =>
            //  {
            //      options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
            //
            //      // register your IdentityServer with Google at https://console.developers.google.com
            //      // enable the Google+ API
            //      // set the redirect URI to https://localhost:5001/signin-google
            //      options.ClientId = "copy client ID from Google here";
            //      options.ClientSecret = "copy client secret from Google here";
            //  });



        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            


            // Make identity server redirections over http possible in Edge and latest versions of browsers.
            // WARNING: Not valid in a production environment.
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Content-Security-Policy", "script-src 'unsafe-inline'");
                await next();
            });

            // Fix a problem with chrome. Chrome enabled a new feature "Cookies without SameSite must be secure",
            // the cookies should be expired from https, but in Order, the internal communication in docker compose is http.
            // To avoid this problem, the policy of cookies should be in Lax mode.
            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });

            

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseAuthentication();
            //app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            


        }
    }
}