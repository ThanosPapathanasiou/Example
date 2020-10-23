using System.Threading;
using System.Threading.Tasks;
using Example.Api.Domain;
using Example.Api.Services;

namespace Example.Api.Cqrs.Commands.Handlers
{
    public class CreatePostHandler : ICommandHandler<CreatePostCommand, Post>
    {
        public CreatePostHandler(IPostRepository repository) 
            => this.repository = repository;

        private readonly IPostRepository repository;

        public async Task<Post> Handle(CreatePostCommand context, CancellationToken cancellationToken)
        {
            var post = await repository.CreatePostAsync(context.Title);
            return post;
        }
    }
}