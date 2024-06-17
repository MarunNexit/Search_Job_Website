using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Web_search_job.DatabaseClasses;
using Web_search_job.DatabaseClasses.FiltersFolder;

namespace Web_search_job.DTO.User
{
    public class UserDTO
    {
        public int? Id { get; set; }
        public string UserId { get; set; } = "";
        public string Email { get; set; } = "";

        public string? FirstName { get; set; } = "";
        public string? LastName { get; set; } = "";

        public int? UserAge { get; set; }
        public string? PhoneNumber { get; set; } = "";
        public string? UserImg { get; set; } = "";
        public DateTime UserCreatedAt { get; set; }

        public string? Gender { get; set; } = "";

        public Location? Location{ get; set; }
    }
}
