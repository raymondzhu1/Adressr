namespace Adressr.Library.Interfaces
{
    public interface IWorkTask : IModel
    {
        public string Description { get; set; }

        public bool IsDone { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? CompletedDate { get; set; }
    }
}
