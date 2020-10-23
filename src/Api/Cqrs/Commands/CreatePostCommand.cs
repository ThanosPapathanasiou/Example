using Example.Api.Domain;
using MediatR;

namespace Example.Api.Cqrs.Commands
{
    public class CreatePostCommand : IRequest<Post>
    {
        public CreatePostCommand(string title) => Title = title;

        public string Title { get; }
    }
}