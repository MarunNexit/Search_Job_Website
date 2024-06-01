using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        
        public string Resume { get; set; } = "";
        public string Positives { get; set; } = "";
        public string Projects { get; set; } = "";

        public virtual Job Job { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}
