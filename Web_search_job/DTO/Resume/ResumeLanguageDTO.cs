namespace Web_search_job.DTO.Resume
{
    public class ResumeLanguageDTO
    {
        public int? Id { get; set; }
        public int ResumeId { get; set; }
        public string Languale { get; set; }
        public int LevelId { get; set; }
        public LanguageLevelDTO? LanguageLevel { get; set; }
    }
}
