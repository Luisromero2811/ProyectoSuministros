using System;
using Microsoft.AspNetCore.Identity;
using ProyectoSuministros.Server.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace ProyectoSuministros.Server.Helpers
{
	public class VerifyUserId
	{
        public async Task<string> GetId(HttpContext httpContext, UserManager<IdentityUsuario> userManager)
        {
            string Id = string.Empty;

            var authHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (authHeader != null && authHeader.StartsWith("Bearer "))
            {
                Id = authHeader.Substring("Bearer ".Length);
            }

            var handler = new JwtSecurityTokenHandler();

            if (handler.CanReadToken(Id))
            {
                var token = handler.ReadJwtToken(Id);

                var userId = token.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.UniqueName)?.Value;
                var user = await userManager.FindByNameAsync(userId);

                Id = user!.Id;
                return Id;
            }

            return Id;
        }
    }
}

