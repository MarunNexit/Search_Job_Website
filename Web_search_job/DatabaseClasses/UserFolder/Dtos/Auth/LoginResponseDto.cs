namespace Web_search_job.DatabaseClasses.UserFolder.Dtos.Auth
{
    public class LoginResponseDto
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}
