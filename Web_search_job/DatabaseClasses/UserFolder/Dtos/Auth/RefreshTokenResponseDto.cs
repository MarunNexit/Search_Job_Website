namespace Web_search_job.DatabaseClasses.UserFolder.Dtos.Auth
{
    public class RefreshTokenResponseDto
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
