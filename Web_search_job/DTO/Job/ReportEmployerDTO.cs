using Web_search_job.DTO.Employer;
using Web_search_job.DTO.User;

namespace Web_search_job.DTO.Job
{
    public class ReportEmployerDTO
    {
        public int? Id { get; set; }

        public DateTime ReportCreatedAt { get; set; }

        public EmployerDTO Employer { get; set; }
    }
}
