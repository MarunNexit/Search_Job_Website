using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_search_job.DatabaseClasses.JobFolder
{
    public class JobRequestFields
    {
        [Key]
        public int? Id { get; set; }

        [ForeignKey("Job")]
        public int JobId { get; set; }

        public bool NeedAdditionalResume { get; set; }
        public bool NeedResume { get; set; }
        public bool PositiveField { get; set; }
        public bool ProjectField { get; set; }

        public virtual Job Job { get; set; }
    }
}
