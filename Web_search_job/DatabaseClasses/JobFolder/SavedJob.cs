using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web_search_job.DatabaseClasses.UserFolder;

namespace Web_search_job.DatabaseClasses.JobFolder
{
    public class SavedJob
    {
        [Key]
        public int? id { get; set; }

        [ForeignKey("UserInfo")]
        public int? user_id { get; set; }

        [ForeignKey("Job")]
        public int job_id { get; set; }

        public DateTime saved_job_created_at { get; set; }

        public virtual UserInfo? UserInfo { get; set; }
        public virtual Job Job { get; set; }
    }
}
