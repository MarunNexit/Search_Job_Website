using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web_search_job.DatabaseClasses.FiltersFolder;
using Web_search_job.DatabaseClasses.UserFolder;

namespace Web_search_job.DatabaseClasses.EmployerFolder
{
    public class SubscribeToEmployer
    {
        [Key]
        public int? Id { get; set; }

        [ForeignKey("UserInfo")]
        public int UserId { get; set; }

        [ForeignKey("Employer")]
        public int EmployerId { get; set; }

        [ForeignKey("Industry")]
        public int IndustryId { get; set; }

        [ForeignKey("Location")]
        public int LocationId { get; set; }


        public virtual UserInfo UserInfo { get; set; }

        public virtual Employer Employer { get; set; }

        public virtual Industry Industry { get; set; }

        public virtual Location Location { get; set; }

    }
}
