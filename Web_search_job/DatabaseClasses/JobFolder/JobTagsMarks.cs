
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_search_job.DatabaseClasses.JobFolder
{
    public class JobTagsMarks
    {
        [Key]
        public int? id { get; set; }

        [ForeignKey("Job")]
        public int job_id { get; set; }

        public bool tag_hot { get; set; } = false;
        public bool tag_new { get; set; } = false;

/*        public bool tag_recommend { get; set; } = false;
*/
        public virtual Job Job { get; set; }
    }
}
