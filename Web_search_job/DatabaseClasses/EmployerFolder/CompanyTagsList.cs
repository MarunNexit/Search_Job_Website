using System.ComponentModel.DataAnnotations;

namespace Web_search_job.DatabaseClasses
{
    public class CompanyTagsList
    {
        [Key]
        public int? id { get; set; }

        public string company_tags_name { get; set; } = "";

        public virtual ICollection<CompanyTags>? CompanyTags { get; set; }
    }
}
