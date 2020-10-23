using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Example.Api.Contracts.Requests;
using Example.Api.Contracts.Responses;
using Example.Api.Cqrs.Commands;
using Example.Api.Cqrs.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Example.Api.Controllers.v1
{
    
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IMediator mediator;
        public PostsController(IMediator mediator) => this.mediator = mediator;

        [HttpGet]
        [ProducesResponseType(typeof(List<PostResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var posts = await mediator.Send(new GetPostsQuery());
            var result = posts.Select(
                post => new PostResponse { Id = post.Id.ToString(), Title = post.Title }).ToList();
            return Ok(result);
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(PostResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var post = await mediator.Send(new GetPostByIdQuery(id));
            
            if (post == null) {
                return NotFound();
            }

            var dto = new PostResponse { Id = post.Id.ToString(), Title = post.Title };
            return Ok(dto);
        }        

        [HttpPost]
        [ProducesResponseType(typeof(PostResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest post) 
        {
            var createdPost = await mediator.Send(new CreatePostCommand(post.Title));
            var result = new PostResponse { Id = createdPost.Id.ToString(), Title = createdPost.Title };

            var baseUri = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUri + HttpContext.Request.Path.ToUriComponent() + $"/{result.Id}";

            return Created(locationUri, result);
        }
    }
}