namespace Adressr.Library.Interfaces
{
    public interface ITask : IModel
    {
        public string Description { get; set; }

        public bool IsDone { get; set; }
    }
}
