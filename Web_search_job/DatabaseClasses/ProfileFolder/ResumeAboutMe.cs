using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web_search_job.DatabaseClasses.ProfileFolder
{
    public class ResumeAboutMe
    {
        [Key]
        public int? Id { get; set; }
        public int ResumeId { get; set; }

        public string ResumeAboutMeText { get; set; } = "";

        [ForeignKey("ResumeId")]
        public virtual Resume Resume { get; set; }

    }
}
