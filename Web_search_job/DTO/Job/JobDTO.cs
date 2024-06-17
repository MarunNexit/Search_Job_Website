using Web_search_job.DatabaseClasses;
using Web_search_job.DatabaseClasses.FiltersFolder;
using Web_search_job.DTO.Employer;
using Web_search_job.DTO.User;

namespace Web_search_job.DTO.Job
{
    public class JobDTO
    {
        public int? Id { get; set; }
        public string JobTitle { get; set; } = "";
        public string JobImg { get; set; } = "";
        public string JobBackgroundImg { get; set; } = "";

        public int? JobSalaryMin { get; set; }
        public int? JobSalaryMax { get; set; }
        public string JobSalaryCurrency { get; set; } = "";

        public string JobDescription { get; set; } = "";

        public int NumberCandidates { get; set; }
        public int NumberView { get; set; }

        public DateTime DateEnding { get; set; }
        public DateTime? DateLastEditing { get; set; }
        public DateTime? DateApproving { get; set; }
        public string Status { get; set; } = "";
        public DateTime CreatedAt { get; set; }

        //CUSTOM
        public bool isExistJobRequest { get; set; }
        public bool isSavedJob { get; set; }

        //

        public virtual JobTagsMarksDTO? JobTagsMarks { get; set; }

        //public virtual ICollection<Report>? Reports { get; set; }

        public virtual ICollection<JobTagsProsDTO>? JobTagsPros { get; set; }

        //public virtual ICollection<SavedJob>? SavedJobs { get; set; }

        public virtual JobRequirementDTO? JobRequirement { get; set; }
        public virtual JobRequestFieldsDTO? JobRequestFields { get; set; }

        public virtual EmployerDTO? Employer { get; set; }

        public virtual Industry? Industry { get; set; }

        public virtual Location? Location { get; set; }

        public virtual UserDTO? User { get; set; }
    }
}
