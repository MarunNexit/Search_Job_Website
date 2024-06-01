using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web_search_job.DatabaseClasses;
using Web_search_job.DatabaseClasses.EmployerFolder;
using Web_search_job.DatabaseClasses.ProfileFolder;

namespace Web_search_job.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Audit> Audit => Set<Audit>();

        public DbSet<CompanyTags> CompanyTags => Set<CompanyTags>();
        public DbSet<CompanyTagsList> CompanyTagsList => Set<CompanyTagsList>();

        public DbSet<UsersOfEmployer> UsersOfEmployer => Set<UsersOfEmployer>();

        public DbSet<Employer> Employers => Set<Employer>();
        public DbSet<Filters> Filters => Set<Filters>();
        public DbSet<FiltersTypes> FiltersTypes => Set<FiltersTypes>();

        public DbSet<Industry> Industry => Set<Industry>();

        public DbSet<Job> Jobs => Set<Job>();

        public DbSet<JobRequirement> JobRequirements => Set<JobRequirement>();
        public DbSet<JobTagsMarks> JobTagsMarks => Set<JobTagsMarks>();
        public DbSet<JobTagsPros> JobTagsPros => Set<JobTagsPros>();
        public DbSet<JobTagsProsList> JobTagsProsList => Set<JobTagsProsList>();

        public DbSet<Location> Locations => Set<Location>();

        public DbSet<Report> Reports => Set<Report>();
        public DbSet<SavedJob> SavedJobs => Set<SavedJob>();

        public DbSet<UserInfo> UsersInfo => Set<UserInfo>();

        public DbSet<ActiveResumeSection> ActiveResumeSections => Set<ActiveResumeSection>();
        public DbSet<LanguageLevel> LanguageLevels => Set<LanguageLevel>();
        public DbSet<Resume> Resume => Set<Resume>();
        public DbSet<ResumeEducation> ResumeEducations => Set<ResumeEducation>();
        public DbSet<ResumeHistoryWork> ResumeHistoryWorks => Set<ResumeHistoryWork>();
        public DbSet<ResumeLanguage> ResumeLanguages => Set<ResumeLanguage>();
        public DbSet<ResumeLinks> ResumeLinks => Set<ResumeLinks>();
        public DbSet<ResumePortfolio> ResumePortfolio => Set<ResumePortfolio>();
        public DbSet<ResumeSectionType> ResumeSectionType => Set<ResumeSectionType>();
        public DbSet<ResumeSkills> ResumeSkills => Set<ResumeSkills>();


    }
}
