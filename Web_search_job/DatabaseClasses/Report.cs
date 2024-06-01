using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_search_job.DatabaseClasses
{
    public class Report
    {
        [Key]
        public int? id { get; set; }

        [ForeignKey("Employer")]
        public int? employer_id { get; set; }

        [ForeignKey("Job")]
        public int? job_id { get; set; }

        [ForeignKey("UserInfo")]
        public int user_id { get; set; }

        public string report_target { get; set; } = "";

        public DateTime report_created_at { get; set; }

        public virtual Employer? Employer { get; set; }
        public virtual Job? Job { get; set; }
        public virtual UserInfo? UserInfo { get; set; }

    }
}
