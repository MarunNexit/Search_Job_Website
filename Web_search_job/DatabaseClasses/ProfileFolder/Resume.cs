using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web_search_job.DatabaseClasses.UserFolder.Entities.GetData;

namespace Web_search_job.DatabaseClasses.ProfileFolder
{
    public class Resume
    {
        [Key]
        public int? Id { get; set; }
        public int? UserId { get; set; }

        public string ResumeName { get; set; } = "";

        [ForeignKey("UserId")]
        public virtual UserInfo? UserInfo { get; set; }

        [ForeignKey("LocationResumeId")]
        public virtual Location? Location { get; set; }


        public virtual ICollection<ActiveResumeSection>? ActiveResumeSection { get; set; }
        public virtual ICollection<ResumeEducation>? ResumeEducation { get; set; }
        public virtual ICollection<ResumeLanguage>? ResumeLanguage { get; set; }
        public virtual ICollection<ResumeLinks>? ResumeLinks { get; set; }
        public virtual ICollection<ResumePortfolio>? ResumePortfolio { get; set; }
        public virtual ICollection<ResumeSkills>? ResumeSkills { get; set; }

    }
}
