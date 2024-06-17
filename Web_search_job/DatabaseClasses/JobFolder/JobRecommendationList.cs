using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Web_search_job.DatabaseClasses.UserFolder;

namespace Web_search_job.DatabaseClasses.JobFolder
{
    public class JobRecommendationList
    {
        [Key]
        public int? Id { get; set; }

        public int? JobId { get; set; }

        public int? UserId { get; set; }

        public DateTime? date_creating { get; set; }

        [ForeignKey("JobId")]
        public virtual Job? Job { get; set; }

        [ForeignKey("UserId")]
        public virtual UserInfo? UserInfo { get; set; }
    }
}
