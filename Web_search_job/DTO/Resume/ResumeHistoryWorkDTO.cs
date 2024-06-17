using Web_search_job.DTO.Employer;

namespace Web_search_job.DTO.Resume
{
    public class ResumeHistoryWorkDTO
    {
        public int? Id { get; set; }
        public int? ResumeId { get; set; }

        public string? WorkName { get; set; }

        public EmployerWorkExperinceShortDTO? Employer { get; set; }

        public DateTime? StartWorkDate { get; set; }
        public DateTime? EndWorkDate { get; set; }

        public string? WorkText { get; set; }
    }
}
