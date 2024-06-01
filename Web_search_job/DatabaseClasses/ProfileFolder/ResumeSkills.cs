using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_search_job.DatabaseClasses.ProfileFolder
{
    public class ResumeSkills
    {
        [Key]
        public int? Id { get; set; }
        public int ResumeId { get; set; }

        public string SkillName { get; set; } = "";

        [ForeignKey("ResumeId")]
        public virtual Resume Resume { get; set; }


    }
}
