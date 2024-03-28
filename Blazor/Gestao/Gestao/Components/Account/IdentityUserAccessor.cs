using Gestao.Models;
using Microsoft.AspNetCore.Identity;

namespace Gestao.Components.Account
{
    internal sealed class IdentityUserAccessor(UserManager<Usuario> userManager, IdentityRedirectManager redirectManager)
    {
        public async Task<Usuario> GetRequiredUserAsync(HttpContext context)
        {
            var user = await userManager.GetUserAsync(context.User);

            if (user is null)
            {
                redirectManager.RedirectToWithStatus("Account/InvalidUser", $"Error: Unable to load user with ID '{userManager.GetUserId(context.User)}'.", context);
            }

            return user;
        }
    }
}
