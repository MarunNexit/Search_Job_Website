using Web_search_job.DatabaseClasses;

namespace Web_search_job.DTO.User
{
    public class CommentUserDTO
    {
        public int? Id { get; set; }
        public string UserId { get; set; } = "";
        public string UserName { get; set; } = "";
        public string WorkNow { get; set; } = "";
    }
}
