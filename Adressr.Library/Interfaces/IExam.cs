namespace Adressr.Library.Interfaces
{
    public interface IExam : IModel
    {
        public List<IQuestion> Questions { get; set; }

        public float TimeLength { get; set; }

        public bool Private { get; set; }

        public List<IUser>? Participants { get; set; }
    }
}
