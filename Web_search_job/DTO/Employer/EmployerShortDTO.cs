namespace Web_search_job.DTO.Employer
{
    public class EmployerShortDTO
    {
        public int? Id { get; set; }
        public string CompanyName { get; set; }
        public string? CompanyIndustryDescription { get; set; }
        public string? CompanyShortDescription { get; set; }
        public string? CompanyURL { get; set; }
        public int? CompanyYear { get; set; }
        public double? CompanyRating { get; set; }
        public bool CompanyChecked { get; set; }
        public string CompanyImg { get; set; }
        public int CommentCount { get; set; }
        public DateTime EmployerCreatedAt { get; set; }

        public List<CommentStarsDTO>? Comments { get; set; }
        public List<EmployerTagDTO>? Tags { get; set; }

    }
}
