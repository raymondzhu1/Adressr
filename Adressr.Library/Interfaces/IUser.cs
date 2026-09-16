namespace Adressr.Library.Interfaces
{
    public interface IUser : IModel
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
