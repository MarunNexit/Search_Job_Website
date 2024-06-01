namespace Web_search_job.DatabaseClasses.UserFolder.ConfigurationModels
{
    public class JwtConfiguration
    {
        public const string Position = "JWT";

        public string? ValidAudience { get; set; }
        public string? ValidIssuer { get; set; }
        public string? Secret { get; set; }
    }
}
