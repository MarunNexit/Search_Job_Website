using Web_search_job.DTO.User;

namespace Web_search_job.DTO.Employer
{
    public class CommentDTO
    {
        public int? Id { get; set; }
        public long? CommentStars { get; set; }
        public string CommentTitle { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentsToEmployerCreatedAt { get; set; }

        public CommentUserDTO User { get; set; }
    }
}
