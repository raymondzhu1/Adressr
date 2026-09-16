namespace Adressr.Library.Interfaces
{
    public interface IProfile : IModel
    {
        public string Name { get; set; }

        public string Education { get; set; }
    }
}
