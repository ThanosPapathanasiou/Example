using MediatR;

namespace Example.Api.Cqrs.Commands.Handlers
{
    public interface ICommandHandler<TRequest> : IRequestHandler<TRequest,Unit> 
        where TRequest : IRequest<Unit>{ }
    
    public interface ICommandHandler<TRequest,TResponse> : IRequestHandler<TRequest,TResponse> 
        where TRequest : IRequest<TResponse>{ }
}