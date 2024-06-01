﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_search_job.DatabaseClasses.EmployerFolder
{
    public class SubscriveToEmployer
    {
        [Key]
        public int? Id { get; set; }

        [ForeignKey("UserInfo")]
        public int UserId { get; set; }

        [ForeignKey("Employer")]
        public int EmployerId { get; set; }

        [ForeignKey("Industry")]
        public int IndustryId { get; set; }

        [ForeignKey("Location")]
        public int LocationId { get; set; }

        public virtual Industry Industry { get; set; }

        public virtual Location Location { get; set; }

    }
}
