using System.ComponentModel.DataAnnotations;
using Web_search_job.DatabaseClasses.JobFolder;
using Web_search_job.DatabaseClasses.ProfileFolder;

namespace Web_search_job.DatabaseClasses.EmployerFolder
{
    public class TagsList
    {
        [Key]
        public int? id { get; set; }

        public string tags_name { get; set; } = "";

        public virtual ICollection<CompanyTags>? CompanyTags { get; set; }
        public virtual ICollection<JobTagsPros>? JobTagsPros { get; set; }
        public virtual ICollection<ResumeTags>? ResumeTags { get; set; }

    }
}
