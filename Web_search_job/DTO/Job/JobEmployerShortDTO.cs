using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Web_search_job.DatabaseClasses;
using Web_search_job.DatabaseClasses.JobFolder;
using Web_search_job.DTO.User;
using Web_search_job.DatabaseClasses.FiltersFolder;

namespace Web_search_job.DTO.Job
{
    public class JobEmployerShortDTO
    {
        public int? Id { get; set; }
        public string JobTitle { get; set; } = "";

        public int? JobSalaryMin { get; set; }
        public int? JobSalaryMax { get; set; }
        public string JobSalaryCurrency { get; set; } = "";

        public DateTime DateEnding { get; set; }
        public DateTime? DateLastEditing { get; set; }
        public DateTime? DateApproving { get; set; }
        public string Status { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public bool isSavedJob { get; set; }

        public virtual JobTagsMarksDTO? JobTagsMarksDTO { get; set; }

        public virtual Industry? Industry { get; set; }
        public virtual Location? Location { get; set; }     
    }
}
