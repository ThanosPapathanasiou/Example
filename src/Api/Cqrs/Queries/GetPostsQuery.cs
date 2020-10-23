using System.Collections.Generic;
using Example.Api.Domain;
using MediatR;

namespace Example.Api.Cqrs.Queries
{
    public class GetPostsQuery : IRequest<List<Post>> { }
}