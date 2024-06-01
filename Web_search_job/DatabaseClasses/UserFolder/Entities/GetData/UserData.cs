namespace Web_search_job.DatabaseClasses.UserFolder.Entities.GetData
{
    public class UserData
    {
        public string Id { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Age { get; set; }
        public string Img { get; set; } = null!;
        public Location Location { get; set; } = null!;
        public string Phone { get; set; } = null!;
    }
}
