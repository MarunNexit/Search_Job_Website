using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web_search_job.DatabaseClasses.EmployerFolder
{
    public class EmployerSettings
    {

        [Key]
        public int? Id { get; set; }

        [ForeignKey("Employer")]
        public int EmployerId { get; set; }

        public virtual Employer Employer { get; set; }
    }
}
