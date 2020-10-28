using System.Collections.Generic;

namespace Example.Api.Contracts.v1.Responses
{
    public class RegistrationFailureResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}