using System;
using System.Linq;
using Example.Api.DependencyRegistration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            
            // this registration method works only if there is no interdependency between the things that are being registered.
            // i.e. if there is ever a case of class A requiring class B to be resolved as part of its registration
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
                option.SwaggerEndpoint("/swagger/v1/swagger.json", "Example API"); 
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
