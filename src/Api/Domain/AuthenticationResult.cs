using System;
using System.Collections.Generic;

namespace Example.Api.Domain
{
    public class AuthenticationResult
    {
        public bool Success { get; set; }
        
        public string Token { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}