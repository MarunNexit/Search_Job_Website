using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web_search_job.DatabaseClasses.EmployerFolder
{
    public class UsersOfEmployer
    {
        [Key]
        public int? id { get; set; }

        [ForeignKey("UserInfo")]
        public int user_id { get; set; }

        [ForeignKey("Employer")]
        public int employer_id { get; set; }

        public string? employer_role { get; set; } = "";

        public virtual UserInfo UserInfo { get; set; }
        public virtual Employer Employer { get; set; }
    }
}
