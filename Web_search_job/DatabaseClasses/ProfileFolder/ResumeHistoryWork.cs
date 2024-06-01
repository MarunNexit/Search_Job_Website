using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_search_job.DatabaseClasses.ProfileFolder
{
    public class ResumeHistoryWork
    {
        [Key]
        public int? Id { get; set; }
        public int ResumeId { get; set; }

        public int? CompanyId { get; set; }
        public string CompanyName { get; set; } = "";
        public string CompanyDescription { get; set; } = "";
        public string? CompanyLink { get; set; } = "";

        public DateTime? StartWorkDate { get; set; }
        public DateTime? EndWorkDate { get; set; }

        public string? WorkText { get; set; } = "";

        [ForeignKey("CompanyId")]
        public virtual Employer? Employer { get; set; }

        //public virtual Employer Employer { get; set; } Якщо потірбно видаляти елемент історії якщо компанія видаляється

    }
}
