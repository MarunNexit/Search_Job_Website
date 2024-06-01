using Web_search_job.DTO.User;

namespace Web_search_job.DTO.Employer
{
    public class ReportEmployerDTO
    {

        public int? Id { get; set; }
        public string ReportTarget { get; set; } = "";

        public DateTime ReportCreatedAt { get; set; }

        public virtual UserDTO UserDTO { get; set; }

    }
}
