namespace Web_search_job.DTO.Resume
{
    public class ActiveResumeSectionDTO
    {
        public int? Id { get; set; }
        public int ResumeId { get; set; }
        public int SectionId { get; set; }
        public ResumeSectionTypeDTO? ResumeSectionType { get; set; }
    }
}
