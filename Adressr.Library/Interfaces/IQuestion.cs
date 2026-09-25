using Adressr.Library.Enums;

namespace Adressr.Library.Interfaces
{
    public interface IQuestion : IModel
    {
        public string Description { get; set; }

        public AnswerType AnswerType { get; set; }
    }
}
