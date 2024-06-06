using Gestao.Data;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Gestao.Client.Libraries.Helpers
{
    public class AuthenticationHelper
    {
        public static async Task<Guid?> GetAuthenticationUserIdAsync(AuthenticationStateProvider authenticationStateProvider)
        {
            var authenticationState = await authenticationStateProvider.GetAuthenticationStateAsync();
            var userId = authenticationState.User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier);

            if (userId != null && Guid.TryParse(userId!.Value, out var authenticationUserId))
            {
                return authenticationUserId;
            }
            return null;
        }
    }
}
