using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web_search_job.DatabaseClasses.ProfileFolder;
using Web_search_job.DatabaseClasses.UserFolder;

namespace Web_search_job.DatabaseClasses.JobFolder
{
    public class JobRequest
    {
        [Key]
        public int? Id { get; set; }

        [ForeignKey("Job")]
        public int JobId { get; set; }

        [ForeignKey("UserInfo")]
        public int UserId { get; set; }

        [ForeignKey("Resume")]
        public int? ResumeId { get; set; }

        public string? ResumeURL { get; set; } = "";
        public string? CoverLetter { get; set; } = "";
        public string? Positives { get; set; } = "";
        public string? Projects { get; set; } = "";

        public string Status { get; set; } = "";
        public DateTime CreatedAt { get; set; }

        public virtual Job Job { get; set; }
        public virtual UserInfo UserInfo { get; set; }
        public virtual Resume? Resume { get; set; }
    }
}
