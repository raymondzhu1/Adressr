namespace Adressr.Library.Models
{
    public class SkillRequest
    {
        //Currently the same as SkillResponse, but if in the future you have more stuff you wanna add to Skill, then there could be differences in how you handle 'em. Remember me then, and thank me later.
        public required string Name { get; set; }
    }
}
