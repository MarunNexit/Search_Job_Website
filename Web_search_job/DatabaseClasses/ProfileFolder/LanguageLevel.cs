using System.ComponentModel.DataAnnotations;

namespace Web_search_job.DatabaseClasses.ProfileFolder
{
    public class LanguageLevel
    {
        [Key]
        public int? id { get; set; }

        public string LangualeLevel { get; set; } = "";

        public virtual ICollection<ResumeLanguage>? ResumeLanguage { get; set; }
    }
}
