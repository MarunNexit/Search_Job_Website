using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Web_search_job.DatabaseClasses.EmployerFolder;

namespace Web_search_job.DatabaseClasses.ProfileFolder
{
    public class ResumeTags
    {
        [Key]
        public int? id { get; set; }

        public int? resume_id { get; set; }

        public int? tags_list_id { get; set; }

        [ForeignKey("resume_id")]
        public virtual Resume Resume { get; set; }

        [ForeignKey("tags_list_id")]
        public virtual TagsList TagsList { get; set; }
    }
}
