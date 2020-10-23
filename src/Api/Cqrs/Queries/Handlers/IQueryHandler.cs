using MediatR;

namespace Example.Api.Cqrs.Queries.Handlers
{
    public interface IQueryHandler<TRequest,TResponse> : IRequestHandler<TRequest,TResponse> 
        where TRequest : IRequest<TResponse>{ }    
}