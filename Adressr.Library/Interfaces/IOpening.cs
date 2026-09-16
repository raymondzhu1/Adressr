namespace Adressr.Library.Interfaces
{
    public interface IOpening : IModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int NumberOfOpenings { get; set; }

        public float Wage { get; set; }

        public string WageType { get; set; }

        public List<ISkill> Skills { get; set; }
    }
}
