using Web_search_job.DatabaseClasses;
using Web_search_job.DatabaseClasses.FiltersFolder;
using Web_search_job.DTO.Job;
using Web_search_job.DTO.User;

namespace Web_search_job.DTO.Employer
{
    public class EmployerDTO
    {
        public int? Id { get; internal set; }
        public string CompanyName { get; internal set; }
        public string CompanyShortDescription { get; internal set; }
        public string CompanyIndustryDescription { get; internal set; }
        public bool CompanyChecked { get; internal set; }
        public string CompanyImg { get; internal set; }
        public string CompanyBackgroundImg { get; internal set; }
        public string CompanyURL { get; set; } = "";

        public int? CompanyYear { get; set; }
        public int? NumberWorkers { get; set; }

        public int? JobsCount { get; set; }

        public string CompanyDescription { get; set; } = "";
        public string CompanyStatus { get; set; } = "";

        public double? CompanyRating { get; set; }
        public int? CommentCount { get; set; }

        public DateTime EmployerCreatedAt { get; set; }

        public List<CommentDTO>? Comments { get; set; }
        public List<EmployerTagDTO>? Tags { get; set; }
        public List<JobEmployerShortDTO>? Jobs { get; set; }

        //public List<ReportEmployerDTO>? Report { get; set; }

        //public virtual ICollection<ResumeHistoryWork>? ResumeHistoryWork { get; set; }

        public Industry? Industry { get; set; }

        public Location? Location { get; set; }

        public List<UserDTO>? Users { get; set; }

    }
}
