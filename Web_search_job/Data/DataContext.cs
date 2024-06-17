using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web_search_job.DatabaseClasses;
using Web_search_job.DatabaseClasses.EmployerFolder;
using Web_search_job.DatabaseClasses.FiltersFolder;
using Web_search_job.DatabaseClasses.JobFolder;
using Web_search_job.DatabaseClasses.ProfileFolder;
using Web_search_job.DatabaseClasses.Searcher;
using Web_search_job.DatabaseClasses.UserFolder;

namespace Web_search_job.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Audit> Audit => Set<Audit>();
        public DbSet<CompanyTags> CompanyTags => Set<CompanyTags>();
        public DbSet<TagsList> TagsList => Set<TagsList>();
        public DbSet<UsersOfEmployer> UsersOfEmployer => Set<UsersOfEmployer>();
        public DbSet<Employer> Employers => Set<Employer>();
        public DbSet<Filters> Filters => Set<Filters>();
        public DbSet<FiltersTypes> FiltersTypes => Set<FiltersTypes>();
        public DbSet<Industry> Industry => Set<Industry>();
        public DbSet<Location> Locations => Set<Location>();
        public DbSet<Job> Jobs => Set<Job>();
        public DbSet<JobRequirement> JobRequirements => Set<JobRequirement>();
        public DbSet<JobRequest> JobRequests => Set<JobRequest>();
        public DbSet<JobRequestFields> JobRequestFields => Set<JobRequestFields>();
        public DbSet<JobRecommendationList> JobRecommendationList => Set<JobRecommendationList>();
        public DbSet<JobTagsMarks> JobTagsMarks => Set<JobTagsMarks>();
        public DbSet<JobTagsPros> JobTagsPros => Set<JobTagsPros>();
        public DbSet<Report> Reports => Set<Report>();
        public DbSet<SavedJob> SavedJobs => Set<SavedJob>();
        public DbSet<UserInfo> UsersInfo => Set<UserInfo>();
        public DbSet<Notification> Notifications => Set<Notification>();
        public DbSet<ActiveResumeSection> ActiveResumeSections => Set<ActiveResumeSection>();
        public DbSet<LanguageLevel> LanguageLevels => Set<LanguageLevel>();
        public DbSet<Resume> Resume => Set<Resume>();
        public DbSet<ResumeTags> ResumeTags => Set<ResumeTags>();
        public DbSet<ResumeAboutMe> ResumeAboutMe => Set<ResumeAboutMe>();
        public DbSet<ResumeEducation> ResumeEducations => Set<ResumeEducation>();
        public DbSet<ResumeHistoryWork> ResumeHistoryWorks => Set<ResumeHistoryWork>();
        public DbSet<ResumeLanguage> ResumeLanguages => Set<ResumeLanguage>();
        public DbSet<ResumeLinks> ResumeLinks => Set<ResumeLinks>();
        public DbSet<ResumePortfolio> ResumePortfolio => Set<ResumePortfolio>();
        public DbSet<ResumeSectionType> ResumeSectionType => Set<ResumeSectionType>();
        public DbSet<ResumeSkills> ResumeSkills => Set<ResumeSkills>();
        public DbSet<SubscribeToEmployer> SubscribeToEmployer => Set<SubscribeToEmployer>();
        public DbSet<SearcherSettings> SearcherSettings => Set<SearcherSettings>();
        public DbSet<EmployerSettings> EmployerSettings => Set<EmployerSettings>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyTags>()
                .HasOne(ct => ct.TagsList)
                .WithMany(tl => tl.CompanyTags)
                .HasForeignKey(ct => ct.tags_list_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JobTagsPros>()
                .HasOne(jtp => jtp.TagsList)
                .WithMany(tl => tl.JobTagsPros)
                .HasForeignKey(jtp => jtp.tags_list_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ResumeTags>()
                .HasOne(rt => rt.TagsList)
                .WithMany(tl => tl.ResumeTags)
                .HasForeignKey(rt => rt.tags_list_id)
                .OnDelete(DeleteBehavior.Restrict);

            // If there are additional configurations for other entities, include them here

            base.OnModelCreating(modelBuilder);
        }
    }
}
