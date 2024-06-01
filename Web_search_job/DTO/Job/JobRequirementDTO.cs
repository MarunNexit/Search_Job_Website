using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web_search_job.DTO.Job
{
    public class JobRequirementDTO
    {
        public int? id { get; set; }

        public string? experience { get; set; } = "";
        public string? language_name { get; set; } = "";
        public string? language_level { get; set; } = "";
    }
}
