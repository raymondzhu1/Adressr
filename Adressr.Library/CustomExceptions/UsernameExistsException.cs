namespace Adressr.Library.CustomExceptions
{
    public class UsernameExistsException : Exception
    {
        public UsernameExistsException(string username) : base($"The username/email '{username}' is already taken.")
        {

        }
    }
}
