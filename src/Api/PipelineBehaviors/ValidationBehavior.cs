using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace Example.Api.PipelineBehaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            this.validators = validators;
        }

        public Task<TResponse> Handle(
            TRequest request, 
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var context = new FluentValidation.ValidationContext<TRequest>(request);

            var failures = validators
                .Select(x => x.Validate(context))
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .ToList();
            
            if (failures.Any())
            {
                throw new ValidationException(failures);
            }

            return next();
        }
    }
}