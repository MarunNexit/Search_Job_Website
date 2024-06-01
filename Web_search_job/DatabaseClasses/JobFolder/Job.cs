using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web_search_job.DatabaseClasses.JobFolder;

namespace Web_search_job.DatabaseClasses
{
    public class Job
    {
        [Key]
        public int? id { get; set; }

        public int? employer_id { get; set; }

        public string job_title { get; set; } = "";
        public string job_img { get; set; } = "";
        public string job_background_img { get; set; } = "";

        public int? job_salary_min { get; set; }
        public int? job_salary_max { get; set; }
        public string job_salary_currency { get; set; } = "";

        public string job_description { get; set; } = "";
        public int? creater_user_id { get; set; }
        public int? industry_job_id { get; set; }
        public int? location_job_id { get; set; }

        public int number_candidates { get; set; }
        public int number_view { get; set; }

        public DateTime date_ending { get; set; }
        public DateTime? date_last_editing { get; set; }
        public DateTime? date_approving { get; set; }
        public string status { get; set; } = "";
        public DateTime created_at { get; set; }

        public virtual JobTagsMarks? JobTagsMarks { get; set; }
        public virtual ICollection<Report>? Reports { get; set; }
        public virtual ICollection<JobTagsPros>? JobTagsPros { get; set; }
        public virtual ICollection<SavedJob>? SavedJobs { get; set; }

        public virtual JobRequirement? JobRequirements { get; set; }
        public virtual JobRequestFields? JobRequestFields { get; set; }

        [ForeignKey("industry_job_id")]
        public virtual Industry? Industry { get; set; }

        [ForeignKey("employer_id")]
        public virtual Employer? Employer { get; set; }

        [ForeignKey("location_job_id")]
        public virtual Location? Location { get; set; }

        [ForeignKey("creater_user_id")]
        public virtual UserInfo? UserInfo { get; set; }

    }
}
