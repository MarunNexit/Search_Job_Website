using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_search_job.DatabaseClasses
{
    public class JobTagsPros
    {
        [Key]
        public int? id { get; set; }

        public int job_id { get; set; }

        [ForeignKey("job_id")]
        public virtual Job Job { get; set; }

        [ForeignKey("JobTagsProsList")]
        public int? job_tags_pros_list { get; set; }


        public virtual JobTagsProsList JobTagsProsList { get; set; }
    }
}

