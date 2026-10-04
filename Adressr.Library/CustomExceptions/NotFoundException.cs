namespace Adressr.Library.CustomExceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string resourceName) : base($"{resourceName} was not found.") { }

        public NotFoundException(string resourceName, int id) : base($"{resourceName} was not found with the ID {id}") { }
    }
}
