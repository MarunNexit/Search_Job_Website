using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_search_job.DatabaseClasses.JobFolder
{
    public class JobRequirement
    {
        [Key]
        public int? id { get; set; }

        [ForeignKey("Job")]
        public int job_id { get; set; }

        public string? experience { get; set; } = "";
        public string? language_name { get; set; } = "";
        public string? language_level { get; set; } = "";

        public virtual Job Job { get; set; }
    }
}
