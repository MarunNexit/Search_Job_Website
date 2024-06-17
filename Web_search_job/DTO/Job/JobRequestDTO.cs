using Web_search_job.DTO.Resume;

namespace Web_search_job.DTO.Job
{
    public class JobRequestDTO
    {
        public int? Id { get; set; }
        public int JobId { get; set; }
        public int UserId { get; set; }
        public ResumeShortDTO? Resume { get; set; }
        public string? ResumeURL { get; set; }
        public string? CoverLetter { get; set; }
        public string? Positives { get; set; }
        public string? Projects { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
