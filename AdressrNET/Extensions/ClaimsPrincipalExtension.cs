using System.Security.Claims;

namespace AdressrNET.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static int GetUserId(this ClaimsPrincipal principal)
        {
            string? idValue = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(idValue, out int userId))
            {
                throw new InvalidOperationException("Authenticated principal has some weird value.");
            }

            return userId;
        }
    }
}
