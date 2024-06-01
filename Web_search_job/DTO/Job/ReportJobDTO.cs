using Web_search_job.DTO.User;

namespace Web_search_job.DTO.Job
{
    public class ReportJobDTO
    {
        public int? Id { get; set; }

        public DateTime ReportCreatedAt { get; set; }

        public UserDTO UserDTO { get; set; }

        // ADD 
        // public Job Job { get; set; }
    }
}
