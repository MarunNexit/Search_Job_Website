using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_search_job.DatabaseClasses.ProfileFolder
{
    public class ResumeLanguage
    {
        [Key]
        public int? Id { get; set; }
        public int ResumeId { get; set; }

        public string Languale { get; set; } = "";
        public int LevelId { get; set; }

        [ForeignKey("ResumeId")]
        public virtual Resume Resume { get; set; }

        [ForeignKey("LevelId")]
        public virtual LanguageLevel LanguageLevel { get; set; }


    }
}
