using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Web_search_job.DatabaseClasses;

namespace Web_search_job.DTO.Job
{
    public class JobTagsProsDTO
    {
        public int? Id { get; set; }
        public string JobTagsProsType { get; set; }
        public string JobTagsProsName { get; set; } = "";
    }
}
