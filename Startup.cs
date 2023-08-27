using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogsService.AsyncDataServices;
using DogsService.Data;
using DogsService.EventProcessing;
using DogsService.SyncDataServices.Grpc;
using DogsService.SyncDataServices.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace DogsService
{
    public class Startup
    {
     
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddDbContext<AppDbContext> (optionsAction =>
            optionsAction.UseInMemoryDatabase("InMemnam"));
            services.AddScoped<IDogRepo, DogRepo>(); //IF they ask for IUser Repo we give them user repo
            services.AddControllers();
            services.AddHttpClient<IPhotoDataClient, HttpPhotoDataClient>();
            services.AddHostedService<MessageBusSubscriber>();
            services.AddSingleton<IMessageBusClient, MessageBusClient>();
            services.AddSingleton<IEventProcessor, EventProcessor>(); //Singletone ->> all time alongside 
            services.AddGrpc();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IUserDataClient, UserDataClient>();  //Registering it
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DogsService", Version = "v1" });
            });

        Console.WriteLine($"--> Photo Service Endpoint is {Configuration["PhotoService"]}");

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
                app.UseSwagger(c=>
                c.RouteTemplate = "api/d/swagger/{documentName}/swagger.json");
            

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                 endpoints.MapGrpcService<GrpcDogService>();
                endpoints.MapGet("/protocols/dogs.proto", async context =>
                {
                    await context.Response.WriteAsync(File.ReadAllText("Protocols/dogs.proto"));
                });
                
            });
            PrepDb.PrepPopulation(app);
        }
    }
}