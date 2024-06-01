using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web_search_job.DatabaseClasses.EmployerFolder;
using Web_search_job.DatabaseClasses.ProfileFolder;

namespace Web_search_job.DatabaseClasses
{
    public class UserInfo
    {
        [Key]
        public int? Id { get; set; }

        [ForeignKey("ApplicationUser")]
        public string user_Id { get; set; } = "";

        public int UserAge { get; set; }

        [ForeignKey("Location")]
        public int? LocationId { get; set; }

        public string? FirstName { get; set; } = "";
        public string? LastName { get; set; } = "";

        public string? PhoneNumber { get; set; } = "";

        public string? UserImg { get; set; } = "";

        public DateTime ActionCreatedAt { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Location? Location { get; set; }

        public virtual ICollection<SavedJob>? SavedJobs { get; set; }
        public virtual UsersOfEmployer? UsersOfEmployer { get; set; }

        //public virtual Employer? Employer { get; set; }
    }
}
