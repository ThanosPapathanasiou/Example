using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Api.DependencyRegistration
{
    /// Startup.ConfigureServices will scan the assebly and find all the classes implementing this interface and run RegisterDepencency.
    public interface IRegisterDependency
    {
        void RegisterDependency(IServiceCollection services, IConfiguration configuration);
    }
}