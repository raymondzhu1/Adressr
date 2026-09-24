using Adressr.Library.Enums;

namespace Adressr.Library.Interfaces
{
    public interface IOpening : IModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Wage { get; set; }

        public WageType WageType { get; set; }
    }
}
