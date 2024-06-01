using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_search_job.DatabaseClasses.ProfileFolder
{
    public class ResumeLinks
    {
        [Key]
        public int? Id { get; set; }
        public int ResumeId { get; set; }

        public string Type { get; set; } = "";
        public string Link { get; set; } = "";
        public string AccountName { get; set; } = "";

        [ForeignKey("ResumeId")]
        public virtual Resume Resume { get; set; }


    }
}
