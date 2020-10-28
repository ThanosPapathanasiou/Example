using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Example.Api.Domain;
using Example.Api.Services;

namespace Example.Api.Cqrs.Queries.Handlers
{
    public class GetPostsHandler : IQueryHandler<GetPostsQuery, List<Post>>
    {
        public GetPostsHandler(IPostRepository repository) 
            => this.repository = repository;

        private readonly IPostRepository repository;

        public async Task<List<Post>> Handle(GetPostsQuery context, CancellationToken cancellationToken)
        {
           return await repository.GetAllPostsAsync(cancellationToken);
        }
    }
}