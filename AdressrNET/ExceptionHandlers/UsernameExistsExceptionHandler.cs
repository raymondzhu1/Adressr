using Adressr.Library.CustomExceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace AdressrNET.ExceptionHandlers
{
    public class UsernameExistsExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            if(exception is not UsernameExistsException)
            {
                return false;
            }

            context.Response.StatusCode = StatusCodes.Status409Conflict;
            await context.Response.WriteAsJsonAsync(new ProblemDetails { Status = 409, Title = exception.Message }, cancellationToken);
            return true;
        }
    }
}
