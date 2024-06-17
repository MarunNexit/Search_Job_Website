using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Web_search_job.DatabaseClasses.UserFolder;

namespace Web_search_job.DatabaseClasses.Searcher
{
    public class SearcherSettings
    {
        [Key]
        public int? Id { get; set; }

        [ForeignKey("UserInfo")]
        public int UserId { get; set; }

        public virtual UserInfo UserInfo { get; set; }
    }
}
