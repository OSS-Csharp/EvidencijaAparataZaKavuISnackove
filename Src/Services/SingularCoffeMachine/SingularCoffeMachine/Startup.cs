using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SingularCoffeMachine.Controllers;

namespace SingularCoffeMachine
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public SequentialUpdateSimulation simp=new SequentialUpdateSimulation();

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SingularCoffeMachine", Version = "v1" });
            });
            services.AddSingleton<SequentialUpdateSimulationSingleton>();
            services.AddHostedService<SequentialUpdateSimulationSingleton>(provider =>provider.GetService<SequentialUpdateSimulationSingleton>());
            
            services.Configure<HostOptions>(x => x.ShutdownTimeout = TimeSpan.FromSeconds(10));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SingularCoffeMachine v1"));
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            SequentialUpdateSimulation.MachineHandshake();
            
        }
    }
}
