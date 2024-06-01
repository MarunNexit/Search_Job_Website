namespace Web_search_job.DTO.Job
{
    public class JobRequestFieldsDTO
    {
        public int? Id { get; set; }

        public bool NeedAdditionalResume { get; set; }
        public bool NeedResume { get; set; }
        public bool PositiveField { get; set; }
        public bool ProjectField { get; set; }
    }
}
