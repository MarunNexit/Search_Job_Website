using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_search_job.DatabaseClasses.ProfileFolder
{
    public class ActiveResumeSection
    {
        [Key]
        public int? Id { get; set; }
        public int ResumeId { get; set; }
        public int SectionId { get; set; }

        [ForeignKey("ResumeId")]
        public virtual Resume Resume { get; set; }

        [ForeignKey("SectionId")]
        public virtual ResumeSectionType ResumeSectionType { get; set; }
    }
}
