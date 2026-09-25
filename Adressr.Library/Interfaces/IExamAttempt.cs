namespace Adressr.Library.Interfaces
{
    public interface IExamAttempt : IModel
    {
        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}
