using Web_search_job.DatabaseClasses.FiltersFolder;

namespace Web_search_job.DatabaseClasses.UserFolder.Entities.GetData
{
    public class UserData
    {
        public string? Id { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string? FirstName { get; set; } = null!;
        public string? LastName { get; set; } = null!;
        public int? UserAge { get; set; }
        public string? UserImg { get; set; } = null!;
        public Location? Location { get; set; } = null!;
        public string? NumberPhone { get; set; } = null!;
        public string? Gender { get; set; } // Додана властивість для дати народження
    }
}
