using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_search_job.DatabaseClasses.ProfileFolder
{
    public class ResumeEducation
    {
        [Key]
        public int? Id { get; set; }
        public int ResumeId { get; set; }

        public string EducationProffesion { get; set; } = "";
        public int? IndustryId { get; set; }
        public int? LocationId { get; set; }
        public int EducationYear { get; set; }

        [ForeignKey("ResumeId")]
        public virtual Resume Resume { get; set; }

        [ForeignKey("IndustryId")]
        public virtual Industry? Industry { get; set; }

        [ForeignKey("LocationId")]
        public virtual Location? Location { get; set; }
    }
}
