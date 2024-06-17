namespace Web_search_job.DTO.Job
{
    public class CreateJobRequestDTO
    {
        public int JobId { get; set; }
        public int UserId { get; set; }
        public int? ResumeId { get; set; }
        public string? ResumeURL { get; set; }
        public string? CoverLetter { get; set; }
        public string? Positives { get; set; }
        public string? Projects { get; set; }
        public string? Status { get; set; }
    }
}
