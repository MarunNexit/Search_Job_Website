namespace Web_search_job.DTO.Resume
{
    public class ResumeLinksDTO
    {
        public int? Id { get; set; }
        public int ResumeId { get; set; }
        public string Type { get; set; }
        public string Link { get; set; }
        public string AccountName { get; set; }
    }
}
