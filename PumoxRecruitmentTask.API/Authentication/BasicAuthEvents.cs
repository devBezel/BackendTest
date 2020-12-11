using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ZNetCS.AspNetCore.Authentication.Basic.Events;

namespace PumoxRecruitmentTask.API.Authentication
{
    public class BasicAuthEvents : BasicAuthenticationEvents
    {
        public override Task ValidatePrincipalAsync(ValidatePrincipalContext context)
        {
            if ((context.UserName == "admin") && (context.Password == "admin"))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, context.UserName, context.Options.ClaimsIssuer)
                };

                var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, context.Scheme.Name));
                context.Principal = principal;
            }

            return Task.CompletedTask;
        }
    }
}