using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_search_job.DatabaseClasses
{
    public class Audit
    {
        [Key]
        public int? id { get; set; }

        [ForeignKey("UserInfo")]
        public int user_id { get; set; }

        public string action { get; set; } = "";
        public DateTime action_created_at { get; set; }

        public virtual UserInfo UserInfo { get; set; }
    }
}
