using AutoMapper;
using Example.Api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Api.DependencyRegistration
{
    public class RegisterServices : IRegisterDependency
    {
        public void RegisterDependency(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IPostRepository>(_ => new PostRepository());

            // TODO: if we want to make this a proper application then we should switch the UserStore for a proper DB
            // this InMemoryUser store is a minimum user store implementation with lots of NotImplementedExceptions :)
            // it works for our use case and doesn't require any kind of database connection / file.
            services.AddSingleton<IUserStore<IdentityUser>, InMemoryUserStore>();
            services.AddDefaultIdentity<IdentityUser>();

            services.AddScoped<IIdentityService, IdentityService>();

            services.AddAutoMapper(typeof(Startup));
        }
    }
}