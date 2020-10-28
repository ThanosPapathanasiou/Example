using Example.Api.Domain;
using MediatR;
using System;

namespace Example.Api.Cqrs.Commands
{
    public class UpdatePostCommand : IRequest<Post>
    {
        public UpdatePostCommand(Guid id, string title)
        {
            Id = id;
            Title = title;
        }

        public Guid Id { get; }
        public string Title { get; }
    }
}