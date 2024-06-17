using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Web_search_job.DatabaseClasses.UserFolder;

namespace Web_search_job.DatabaseClasses.ProfileFolder
{
    public class Notification
    {
        //CHANGE
        [Key]
        public int? Id { get; set; }
        public int? UserId { get; set; }

        public string NotificationType { get; set; } = "";

        [ForeignKey("UserId")]
        public virtual UserInfo? UserInfo { get; set; }

        
    }
}
