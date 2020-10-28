using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Api.DependencyRegistration
{
    public class RegisterEndpoints : IRegisterDependency
    {
        public void RegisterDependency(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
        }
    }
}