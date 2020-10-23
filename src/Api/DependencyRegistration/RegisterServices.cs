using Example.Api.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Api.DependencyRegistration
{
    public class RegisterServices : IRegisterDependency
    {
        public void RegisterDependency(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IPostRepository>(_ => new PostRepository());
        }
    }
}