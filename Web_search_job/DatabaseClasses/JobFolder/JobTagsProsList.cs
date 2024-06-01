using System.ComponentModel.DataAnnotations;

namespace Web_search_job.DatabaseClasses
{
    public class JobTagsProsList
    {
        [Key]
        public int? id { get; set; }

        public string job_tags_pros_name { get; set; } = "";

        public virtual ICollection<JobTagsPros>? JobTagsPros { get; set; }
    }
}
