using System.Threading;
using System.Threading.Tasks;
using Example.Api.Domain;
using Example.Api.Services;

namespace Example.Api.Cqrs.Commands.Handlers
{
    public class UpdatePostHandler : ICommandHandler<UpdatePostCommand, Post>
    {
        public UpdatePostHandler(IPostRepository repository) 
            => this.repository = repository;

        private readonly IPostRepository repository;

        public async Task<Post> Handle(UpdatePostCommand context, CancellationToken cancellationToken)
        {
            var postToUpdate = new Post() { Id = context.Id, Title = context.Title };
            var updatedPost = await repository.UpdatePostAsync(postToUpdate, cancellationToken);
            return updatedPost;
        }
    }
}