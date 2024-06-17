using System.ComponentModel.DataAnnotations;
using Web_search_job.DatabaseClasses.JobFolder;
using Web_search_job.DatabaseClasses.ProfileFolder;

namespace Web_search_job.DatabaseClasses.FiltersFolder
{

    public class Location
    {
        [Key]
        public int? id { get; set; }

        public string location_city { get; set; } = "";
        public string location_region { get; set; } = "";
        public string location_country { get; set; } = "";

        public virtual ICollection<Job>? Jobs { get; set; }

        public virtual ICollection<ResumeEducation>? ResumeEducation { get; set; }
    }
}


