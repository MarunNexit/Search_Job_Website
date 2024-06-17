using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Web_search_job.DatabaseClasses.UserFolder;

namespace Web_search_job.DatabaseClasses.EmployerFolder
{
    public class CommentToEmployer
    {
        [Key]
        public int? id { get; set; }

        [ForeignKey("UserInfo")]
        public int? user_id { get; set; }

        [ForeignKey("Employer")]
        public int employer_id { get; set; }

        public long comment_stars { get; set; }
        public string comment_title { get; set; } = "";
        public string comment_text { get; set; } = "";

        public DateTime comments_to_employer_created_at { get; set; }

        public virtual UserInfo? UserInfo { get; set; }

        public virtual Employer Employer { get; set; }
    }
}
