using Adressr.Library.Interfaces;

namespace Adressr.Data.Model
{
    public class Skill : ISkill
    {
        public int SkillID { get; set; }

        public required string Name { get; set; }
    }
}
