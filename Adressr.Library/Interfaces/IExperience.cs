namespace Adressr.Library.Interfaces
{
    public interface IExperience : IModel
    {
        public string CompanyName { get; set; }

        public string RoleTitle { get; set; }

        public string Description { get; set; }

        public bool Private { get; set; }
    }
}
