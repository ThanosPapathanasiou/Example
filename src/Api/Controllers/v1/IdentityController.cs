using Example.Api.Contracts;
using Example.Api.Contracts.v1.Requests;
using Example.Api.Contracts.v1.Responses;
using Example.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Example.Api.Controllers.v1
{
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService identityService;

        public IdentityController(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        [HttpPost(ApiRoutes.Register)]
        public async Task<IActionResult> Register([FromBody]UserRegistrationRequest request)
        {
            var authResponse = await identityService.RegisterAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new RegistrationFailureResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new RegistrationSuccessResponse
            {
                Token = authResponse.Token
            });
        }
    }
}
