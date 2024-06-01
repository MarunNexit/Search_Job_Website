namespace Web_search_job.DatabaseClasses.UserFolder.ConfigurationModels
{
    public class SocialLoginConfiguration
    {
        public const string Position = "SocialLogin";

        public FacebookConfiguration? Facebook { get; set; }
        public GoogleConfiguration? Google { get; set; }
    }
}
