using System;
using Example.Api.Domain;
using MediatR;

namespace Example.Api.Cqrs.Queries
{
    public class GetPostByIdQuery : IRequest<Post> 
    {
        public GetPostByIdQuery(Guid postId)
        {
            PostId = postId;
        }

        public Guid PostId { get; private set; }
    }
}