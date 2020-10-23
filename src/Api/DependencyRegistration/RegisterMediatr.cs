using Example.Api.PipelineBehaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Api.DependencyRegistration
{
    public class RegisterMediatr : IRegisterDependency
    {
        public void RegisterDependency(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddMediatR(typeof(Program).Assembly);
            services.AddValidatorsFromAssembly(typeof(Program).Assembly);
        }
    }
}