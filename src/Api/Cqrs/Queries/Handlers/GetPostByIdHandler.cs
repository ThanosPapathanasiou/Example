using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Example.Api.Domain;
using Example.Api.Services;

namespace Example.Api.Cqrs.Queries.Handlers
{
    public class GetPostByIdHandler : IQueryHandler<GetPostByIdQuery, Post>
    {
        public GetPostByIdHandler(IPostRepository repository) 
            => this.repository = repository;

        private readonly IPostRepository repository;

        public async Task<Post> Handle(GetPostByIdQuery context, CancellationToken cancellationToken)
        {
            var posts = await repository.GetAllPostsAsync();
            var post = posts.SingleOrDefault(p => p.Id == context.PostId);
            return post;
        }
    }
}