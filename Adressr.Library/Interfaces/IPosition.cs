namespace Adressr.Library.Interfaces
{
    public interface IPosition : IModel
    {
        public string Description { get; set; }

        public string Title { get; set; }
    }
}
