using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_search_job.DatabaseClasses.EmployerFolder
{
    public class CompanyTags
    {
        [Key]
        public int? id { get; set; }

        public int employer_id { get; set; }

        [ForeignKey("employer_id")]
        public virtual Employer Employer { get; set; }

        public string type_tag { get; set; } = "";

        [ForeignKey("TagsList")]
        public int? tags_list_id { get; set; }

        public virtual TagsList TagsList { get; set; }
    }
}
