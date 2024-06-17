using Web_search_job.DatabaseClasses.ProfileFolder;
using Web_search_job.DTO.User;

namespace Web_search_job.DTO.Resume
{
    public class ResumeDTO
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public string ResumeName { get; set; }
        public string? ResumeDescription { get; set; }
        public string? ResumeEmail { get; set; }
        public string? ResumePhone { get; set; }
        public string? WantedSalary { get; set; }

        public List<ResumeTagsDTO> ResumeTags { get; set; }
        public bool ResumeActive { get; set; }
        public ResumeAboutMeDTO? ResumeAboutMe { get; set; }
        public UserDTO? UserInfo { get; set; }
        public List<ActiveResumeSectionDTO>? ActiveResumeSection { get; set; }
        public List<ResumeEducationDTO>? ResumeEducation { get; set; }
        public List<ResumeLanguageDTO>? ResumeLanguage { get; set; }
        public List<ResumeLinksDTO>? ResumeLinks { get; set; }
        public List<ResumePortfolioDTO>? ResumePortfolio { get; set; }
        public List<ResumeSkillsDTO>? ResumeSkills { get; set; }
        public List<ResumeHistoryWorkDTO>? ResumeHistoryWork { get; set; }
    }
}
