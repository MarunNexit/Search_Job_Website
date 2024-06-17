using Web_search_job.DatabaseClasses;

namespace Web_search_job.DTO.User
{

    public class UserSetDTO
    {
        public string userId { get; set; } = "";
        public DateTime? dateOfBirth { get; set; }
        public int? locationId { get; set; }
        public string? firstName { get; set; } = "";
        public string? lastName { get; set; } = "";
        public string? phoneNumber { get; set; } = "";
        public string? userImg { get; set; } = "";
        public string? gender { get; set; } = "";
    }
}
