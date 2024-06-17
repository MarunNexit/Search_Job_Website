using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web_search_job.DatabaseClasses.FiltersFolder
{
    public class Filters
    {
        [Key]
        public int? id { get; set; }

        public string filter_name { get; set; } = "";

        public int filter_type_id { get; set; }

        [ForeignKey("filter_type_id")]
        public virtual FiltersTypes FilterType { get; set; }
    }
}
