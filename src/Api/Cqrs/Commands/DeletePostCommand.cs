using MediatR;
using System;

namespace Example.Api.Cqrs.Commands
{
    public class DeletePostCommand : IRequest<bool>
    {
        public DeletePostCommand(Guid id) => Id = id;

        public Guid Id { get; }
    }
}