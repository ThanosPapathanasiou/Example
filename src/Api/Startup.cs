using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Example.Api.DependencyRegistration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

namespace Example.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var dependencyRegistrants = 
                typeof(Startup).Assembly.ExportedTypes
                .Where(type => typeof(IRegisterDependency).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                .Select(type => Activator.CreateInstance(type))
                .Cast<IRegisterDependency>()
                .ToList();
            
            dependencyRegistrants.ForEach(x => x.RegisterDependency(services, Configuration));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(option => { 
                option.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplicationArchitectureExample API"); 
            });

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
