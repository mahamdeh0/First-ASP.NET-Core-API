using Azure.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Sockets;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.Encodings.Web;

namespace FirstASP.NETCoreAPI.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization")) 
                return Task.FromResult(AuthenticateResult.NoResult());

            var authHeader = Request.Headers["Authorization"].ToString(); 
            if (!authHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase)) 
                    return Task.FromResult(AuthenticateResult.Fail("Unknown scheme")); 

            var encodedCredentials = authHeader["Basic ".Length..]; 
            var decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));
            var userNameAndPassword = decodedCredentials.Split(':');
            
            if (userNameAndPassword[0] != "admin" || userNameAndPassword[1] != "password") 
                return Task.FromResult(AuthenticateResult.Fail("Invalid username or password"));

            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"), 
                new Claim(ClaimTypes.Name, userNameAndPassword[0])


            }, "Basic");

            var principal = new ClaimsPrincipal(identity); 
            var ticket = new AuthenticationTicket(principal, "Basic");
            return Task.FromResult(AuthenticateResult.Success(ticket)); 
        }
    }
}
