namespace Adressr.Library.Interfaces
{
    public interface IExam : IModel
    {
        public string Title { get; set; }
        public TimeSpan TimeLimit { get; set; }

        public bool Private { get; set; }
    }
}
