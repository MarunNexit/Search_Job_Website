using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web_search_job.DatabaseClasses.EmployerFolder;
using Web_search_job.DatabaseClasses.ProfileFolder;

namespace Web_search_job.DatabaseClasses
{
    public class Employer
    {
        [Key]
        public int? id { get; set; }

        public string company_name { get; set; } = "";
        public string company_industry_description { get; set; } = "";

        [ForeignKey("Industry")]
        public int? industry_id { get; set; }


        public string company_short_description { get; set; } = "";
        public bool company_checked { get; set; } = false;

        public string company_img { get; set; } = "";
        public string company_background_img { get; set; } = "";

        [ForeignKey("Location")]
        public int? location_company_id { get; set; }

        public string company_url { get; set; } = "";

        public int? company_year_experience { get; set; }
        public int? number_workers { get; set; }

        public string company_description { get; set; } = "";

        public string company_status { get; set; } = "";

        public DateTime employer_created_at { get; set; }

        public virtual ICollection<CommentToEmployer>? CommentToEmployer { get; set; }
        public virtual ICollection<CompanyTags>? CompanyTags { get; set; }
        public virtual ICollection<Report>? Report { get; set; }
        public virtual ICollection<Job>? Jobs { get; set; }

        public virtual ICollection<ResumeHistoryWork>? ResumeHistoryWork { get; set; }

        public virtual Location? Location { get; set; }
        public virtual Industry? Industry { get; set; }

        public virtual ICollection<UsersOfEmployer>? UsersOfEmployer { get; set; }
    }
}
