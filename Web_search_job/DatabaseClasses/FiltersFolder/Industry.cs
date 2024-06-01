using System.ComponentModel.DataAnnotations;
using Web_search_job.DatabaseClasses.ProfileFolder;

namespace Web_search_job.DatabaseClasses
{
    public class Industry
    {
        [Key]
        public int? id { get; set; }

        public string industry_name { get; set; } = "";

        public virtual ICollection<Job>? Jobs { get; set; }
        public virtual ICollection<Employer>? Employers { get; set; }
        public virtual ICollection<ResumeEducation>? ResumeEducation { get; set; }

    }
}
