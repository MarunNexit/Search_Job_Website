using Web_search_job.DTO.Other;

namespace Web_search_job.DTO.Resume
{
    public class ResumeEducationDTO
    {
        public int? Id { get; set; }
        public int ResumeId { get; set; }
        public string EducationProffesion { get; set; }
        public int? IndustryId { get; set; }
        public int? LocationId { get; set; }
        public int EducationYear { get; set; }
        public IndustryDataDTO? Industry { get; set; }
        public LocationDataDTO? Location { get; set; }
    }
}
