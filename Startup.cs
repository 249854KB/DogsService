using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogsService.AsyncDataServices;
using DogsService.Data;
using DogsService.EventProcessing;
using DogsService.SyncDataServices.Grpc;
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
            services.AddHostedService<MessageBusSubscriber>();
            services.AddSingleton<IEventProcessor, EventProcessor>(); //Singletone ->> all time alongside 
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IUserDataClient, UserDataClient>();  //Registering it
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DogsService", Version = "v1" });
            });

     

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DogsService v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                
            });
            PrepDb.PrepPopulation(app);
        }
    }
}