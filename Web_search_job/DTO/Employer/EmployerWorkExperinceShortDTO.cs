using Web_search_job.DatabaseClasses;
using Web_search_job.DTO.Job;
using Web_search_job.DTO.User;

namespace Web_search_job.DTO.Employer
{
    public class EmployerWorkExperinceShortDTO
    {
        public int? Id { get; internal set; }
        public string CompanyName { get; internal set; }
        public string CompanyShortDescription { get; internal set; }
        public string CompanyURL { get; set; } = "";

    }
}
