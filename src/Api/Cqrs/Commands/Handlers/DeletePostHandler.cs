using System.Threading;
using System.Threading.Tasks;
using Example.Api.Services;

namespace Example.Api.Cqrs.Commands.Handlers
{
    public class DeletePostHandler : ICommandHandler<DeletePostCommand, bool>
    {
        public DeletePostHandler(IPostRepository repository)
            => this.repository = repository;

        private readonly IPostRepository repository;

        public async Task<bool> Handle(DeletePostCommand context, CancellationToken cancellationToken)
        {
            var post = await repository.DeletePostAsync(context.Id);
            return post;
        }
    }
}