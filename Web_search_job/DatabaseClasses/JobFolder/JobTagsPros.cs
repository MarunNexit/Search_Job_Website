using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web_search_job.DatabaseClasses.EmployerFolder;

namespace Web_search_job.DatabaseClasses.JobFolder
{
    public class JobTagsPros
    {
        [Key]
        public int? id { get; set; }

        public int job_id { get; set; }

        [ForeignKey("job_id")]
        public virtual Job Job { get; set; }

        public int? tags_list_id { get; set; }

        [ForeignKey("tags_list_id")]
        public virtual TagsList TagsList { get; set; }
    }
}

