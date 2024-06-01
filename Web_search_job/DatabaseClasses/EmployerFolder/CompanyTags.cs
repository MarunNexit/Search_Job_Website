using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_search_job.DatabaseClasses
{
    public class CompanyTags
    {
        [Key]
        public int? id { get; set; }

        public int employer_id { get; set; }

        [ForeignKey("employer_id")]
        public virtual Employer Employer { get; set; }

        public string type_tag { get; set; } = "";

        public int? company_tags_list { get; set; }

        [ForeignKey("company_tags_list")]
        public virtual CompanyTagsList CompanyTagsList { get; set; }
    }
}
