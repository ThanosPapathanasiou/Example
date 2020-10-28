using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Example.Api.Contracts;
using Example.Api.Contracts.v1.Responses;
using Example.Api.Contracts.v1.Requests;
using Example.Api.Cqrs.Commands;
using Example.Api.Cqrs.Queries;
using Example.Api.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Example.Api.Controllers.v1
{
    [ApiController]
    [Route(ApiRoutes.PostsV1)]
    public class PostsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public PostsController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<PostResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var posts = await mediator.Send(new GetPostsQuery());
            var result = posts.Select(mapper.Map<Post, PostResponse>);
            return Ok(result);
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(PostResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var post = await mediator.Send(new GetPostByIdQuery(id));

            if (post == null)
            {
                return NotFound();
            }

            var result = mapper.Map<Post, PostResponse>(post);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(PostResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest post)
        {
            var createdPost = await mediator.Send(new CreatePostCommand(post.Title));
            var result = mapper.Map<Post, PostResponse>(createdPost);

            var baseUri = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUri + HttpContext.Request.Path.ToUriComponent() + $"/{result.Id}";

            return Created(locationUri, result);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletedSuccessfully = await mediator.Send(new DeletePostCommand(id));

            if (!deletedSuccessfully)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}