using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web_search_job.DatabaseClasses
{
    public class FiltersTypes
    {
        [Key]
        public int? id { get; set; }

        public string filter_type_name { get; set; } = "";

        public virtual ICollection<Filters>? Filters { get; set; }
    }
}
