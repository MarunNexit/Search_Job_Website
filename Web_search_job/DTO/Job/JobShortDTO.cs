using Web_search_job.DatabaseClasses;
using Web_search_job.DTO.Employer;
using Web_search_job.DTO.User;

namespace Web_search_job.DTO.Job
{
    public class JobShortDTO
    {
        public int? Id { get; set; }
        public string JobTitle { get; set; } = "";
        public string JobImg { get; set; } = "";
        public string? JobBackgroundImg { get; set; } = "";
        
        public int? JobSalaryMin { get; set; }
        public int? JobSalaryMax { get; set; }
        public string JobSalaryCurrency { get; set; } = "";
        public string JobDescription { get; set; } = "";

        public int NumberCandidates { get; set; } = 0;
        public int NumberView { get; set; } = 0;

        public bool isSavedJob { get; set; }
        public DateTime DateEnding { get; set; }
        public DateTime? DateLastEditing { get; set; }
        public DateTime? DateApproving { get; set; }
        public string Status { get; set; } = "";
        public DateTime CreatedAt { get; set; }

        public virtual JobTagsMarksDTO? JobTagsMarks { get; set; }
        public virtual List<JobTagsProsDTO>? JobTagsPros { get; set; }

        public virtual EmployerShortDTO? Employer { get; set; }
        public virtual Industry? Industry { get; set; }
        public virtual Location? Location { get; set; }
    }
}
