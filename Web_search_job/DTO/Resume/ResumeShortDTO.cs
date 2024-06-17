using Web_search_job.DTO.User;

namespace Web_search_job.DTO.Resume
{
    public class ResumeShortDTO
    {
        public int? Id { get; set; }
        public string ResumeName { get; set; }
        public string? ResumeDescription { get; set; }
        public bool ResumeActive { get; set; }
    }
}
