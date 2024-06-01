using System.ComponentModel.DataAnnotations;

namespace Web_search_job.DatabaseClasses.ProfileFolder
{
    public class ResumeSectionType
    {
        [Key]
        public int? Id { get; set; }
        public string SectionType { get; set; } = "";

        public virtual ICollection<ActiveResumeSection>? ActiveResumeSection { get; set; }

    }
}
