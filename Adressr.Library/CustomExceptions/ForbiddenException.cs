namespace Adressr.Library.CustomExceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string name) : base($"You do not have access to {name}'s employee chart.")
        {

        }

        public ForbiddenException(string message, string name) : base($"{message}, {name}")
        {

        }
    }
}
