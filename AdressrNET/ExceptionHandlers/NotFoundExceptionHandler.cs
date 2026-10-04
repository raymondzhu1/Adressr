using Microsoft.AspNetCore.Diagnostics;
using Adressr.Library.CustomExceptions;
using Microsoft.AspNetCore.Mvc;

namespace AdressrNET.ExceptionHandlers
{
    public class NotFoundExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not NotFoundException)
            {
                return false;
            }

            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = exception.Message
            }, cancellationToken);

            return true;
        }
    }
}
