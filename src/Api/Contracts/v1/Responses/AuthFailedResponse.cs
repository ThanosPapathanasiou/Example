using System.Collections.Generic;

namespace Example.Api.Contracts.v1.Responses
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}