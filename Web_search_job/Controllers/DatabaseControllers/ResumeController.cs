using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;
using Web_search_job.Data;
using Web_search_job.DatabaseClasses;
using Web_search_job.DatabaseClasses.ProfileFolder;
using Web_search_job.DatabaseClasses.UserFolder.Entities.GetData;
using Web_search_job.DTO.Employer;
using Web_search_job.DTO.Other;
using Web_search_job.DTO.Resume;
using Web_search_job.DTO.User;
using static System.Collections.Specialized.BitVector32;
using static Web_search_job.Controllers.DatabaseControllers.JobController;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using Azure.Storage.Blobs;
using Web_search_job.DatabaseClasses.FiltersFolder;
using RestSharp;
using static System.Net.WebRequestMethods;
using static Web_search_job.Controllers.DatabaseControllers.ResumeController;

namespace Web_search_job.Controllers.DatabaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly UserController _userController;
        private readonly IConfiguration _configuration;

        public ResumeController(DataContext context, UserController userController, IConfiguration configuration)
        {
            _context = context;
            _userController = userController;
            _configuration = configuration;
        }


        public class UpdateActiveResumeRequest
        {
            public string UserId { get; set; }
            public int ResumeId { get; set; }
        }

        [HttpPost("set_active_resume")]
        public async Task<IActionResult> SetActiveResume([FromBody] UpdateActiveResumeRequest request)
        {

            int? intId = 0;

            if (request.UserId != null && request.UserId != "" && request.UserId != "NULL")
            {
                var user = await _userController.GetUserById(request.UserId);

                if (user == null)
                {
                    return NotFound($"Помилка користувача {request.UserId}");
                }

                intId = user.UserInfo.Id;
            }

            var resumes = await _context.Resume
                .Where(r => r.UserId == intId)
                .ToListAsync();

            if (!resumes.Any())
            {
                return NotFound("Не знайдено резюме цього користувача");
            }

            // Позначаємо всі резюме як неактивні
            foreach (var resume in resumes)
            {
                resume.ResumeActive = false;
            }

            // Знаходимо резюме за його ID та робимо його активним
            var resumeToActivate = resumes.FirstOrDefault(r => r.Id == request.ResumeId);
            if (resumeToActivate != null)
            {
                resumeToActivate.ResumeActive = true;
                Console.WriteLine(resumeToActivate.ResumeActive);
                await _context.SaveChangesAsync();
            }
            else
            {
                return NotFound("Не знайдено резюме.");
            }



            return Ok();
        }



        [HttpGet("resume_name_list")]
        public async Task<IActionResult> GetResumeNameListByUserId(string userId)
        {

            int? intId = 0;

            if (userId != null && userId != "" && userId != "NULL")
            {
                var user = await _userController.GetUserById(userId);

                if (user == null)
                {
                    return NotFound($"Помилка користувача {userId}");
                }

                intId = user.UserInfo.Id;
            }

            // Get all resumes of the user
            var resumes = await _context.Resume
                .Include(r => r.UserInfo)
                .Where(r => r.UserId == intId)
                .ToListAsync();

            if (!resumes.Any())
            {
                return NotFound("No resumes found for the user.");
            }

            // Sort the list so that the active resume is first
            var sortedResumes = resumes.OrderBy(r => r.Id).ToList();

            // Map the resumes to DTOs
            var resumeDTOs = sortedResumes.Select(resume => new ResumeShortDTO
            {
                Id = resume.Id,
                ResumeName = resume.ResumeName,
                ResumeDescription = resume.ResumeDescription,
                ResumeActive = resume.ResumeActive,
            }).ToList();

            return Ok(resumeDTOs);
        }


        [HttpGet("resume_list")]
        public async Task<IActionResult> GetResumeListByUserId(string userId)
        {
            int? intId = 0;

            if (userId != null && userId != "" && userId != "NULL")
            {
                var user = await _userController.GetUserById(userId);

                if (user == null)
                {
                    return NotFound($"Помилка користувача {userId}");
                }

                intId = user.UserInfo.Id;
            }

            // Get all resumes of the user
            var resumes = await _context.Resume
                .Include(r => r.UserInfo)
                .Include(r => r.ActiveResumeSection)
                    .ThenInclude(ar => ar.ResumeSectionType)
                .Include(r => r.ResumeEducation)
                    .ThenInclude(re => re.Industry)
                .Include(r => r.ResumeEducation)
                    .ThenInclude(re => re.Location)
                .Include(r => r.ResumeLanguage)
                    .ThenInclude(rl => rl.LanguageLevel)
                .Include(r => r.ResumeLinks)
                .Include(r => r.ResumePortfolio)
                .Include(r => r.ResumeSkills)
                .Include(r => r.ResumeTags)
                    .ThenInclude(rl => rl.TagsList)
                .Where(r => r.UserId == intId)
                .ToListAsync();

            if (!resumes.Any())
            {
                return NotFound("No resumes found for the user.");
            }

            // Sort the list so that the active resume is first
            var sortedResumes = resumes.OrderByDescending(r => r.ResumeActive).ThenBy(r => r.Id).ToList();

            // Check if there is no active resume
            var activeResume = sortedResumes.FirstOrDefault(r => r.ResumeActive);

            // If there is no active resume, make the first one active
            if (activeResume == null)
            {
                sortedResumes.First().ResumeActive = true;
                await _context.SaveChangesAsync();
            }

            // Map the resumes to DTOs
            var resumeDTOs = sortedResumes.Select(resume => new ResumeDTO
            {
                Id = resume.Id,
                UserId = resume.UserId,
                ResumeName = resume.ResumeName,
                ResumeDescription = resume.ResumeDescription,
                ResumeEmail = resume.ResumeEmail,
                ResumePhone = resume.ResumePhone,
                WantedSalary = resume.WantedSalary,

                ResumeActive = resume.ResumeActive,

                ResumeTags = resume.ResumeTags != null ? resume.ResumeTags.Select(t => new ResumeTagsDTO
                {
                    Id = t.id,
                    ResumeTagName = t.TagsList.tags_name,
                }).ToList() : null,

                UserInfo = resume.UserInfo != null ? new UserDTO
                {
                    Id = resume.UserInfo.Id,
                    UserId = resume.UserInfo.user_Id,
                    Email = resume.UserInfo.ApplicationUser != null ? resume.UserInfo.ApplicationUser.UserName : null,
                    FirstName = resume.UserInfo.FirstName,
                    LastName = resume.UserInfo.LastName,
                    UserAge = CalculateAge(resume.UserInfo.DateOfBirth),
                    PhoneNumber = resume.UserInfo.PhoneNumber,
                    UserCreatedAt = resume.UserInfo.ActionCreatedAt,
                    UserImg = resume.UserInfo.UserImg,
                    Location = resume.UserInfo.Location != null ? new Location
                    {
                        id = resume.UserInfo.Location.id,
                        location_city = resume.UserInfo.Location.location_city,
                        location_region = resume.UserInfo.Location.location_region,
                        location_country = resume.UserInfo.Location.location_country,
                    } : null,
                } : null,

                ActiveResumeSection = resume.ActiveResumeSection.Select(ar => new ActiveResumeSectionDTO
                {
                    Id = ar.Id,
                    ResumeId = ar.ResumeId,
                    SectionId = ar.SectionId,
                    ResumeSectionType = ar.ResumeSectionType != null ? new ResumeSectionTypeDTO
                    {
                        Id = ar.ResumeSectionType.Id,
                        SectionType = ar.ResumeSectionType.SectionType,
                    } : null,
                }).ToList(),

                ResumeEducation = resume.ActiveResumeSection.Any(ar => ar.ResumeSectionType.SectionType == "Education")
                    ? resume.ResumeEducation.Select(re => new ResumeEducationDTO
                    {
                        Id = re.Id,
                        ResumeId = re.ResumeId,
                        EducationProffesion = re.EducationProffesion,
                        IndustryId = re.IndustryId,
                        LocationId = re.LocationId,
                        EducationYear = re.EducationYear,
                        Industry = re.Industry != null ? new IndustryDataDTO
                        {
                            id = re.Industry.id,
                            industry_name = re.Industry.industry_name,
                        } : null,
                        Location = re.Location != null ? new LocationDataDTO
                        {
                            id = re.Location.id,
                            location_city = re.Location.location_city,
                            location_region = re.Location.location_region,
                            location_country = re.Location.location_country,
                        } : null,
                    }).ToList()
                    : null,

                ResumeLanguage = resume.ActiveResumeSection.Any(ar => ar.ResumeSectionType.SectionType == "Languages")
                    ? resume.ResumeLanguage.Select(rl => new ResumeLanguageDTO
                    {
                        Id = rl.Id,
                        ResumeId = rl.ResumeId,
                        Languale = rl.Languale,
                        LevelId = rl.LevelId,
                        LanguageLevel = rl.LanguageLevel != null ? new LanguageLevelDTO
                        {
                            Id = rl.LanguageLevel.id,
                            Level = rl.LanguageLevel.LangualeLevel,
                        } : null,
                    }).ToList()
                    : null,

                ResumeLinks = resume.ActiveResumeSection.Any(ar => ar.ResumeSectionType.SectionType == "Links")
                    ? resume.ResumeLinks.Select(rl => new ResumeLinksDTO
                    {
                        Id = rl.Id,
                        ResumeId = rl.ResumeId,
                        Type = rl.Type,
                        Link = rl.Link,
                        AccountName = rl.AccountName,
                    }).ToList()
                    : null,

                ResumePortfolio = resume.ActiveResumeSection.Any(ar => ar.ResumeSectionType.SectionType == "Portfolio")
                    ? resume.ResumePortfolio.Select(rp => new ResumePortfolioDTO
                    {
                        Id = rp.Id,
                        ResumeId = rp.ResumeId,
                        PortfolioImg = rp.PortfolioImg,
                        PortfolioLink = rp.PortfolioLink,
                    }).ToList()
                    : null,

                ResumeSkills = resume.ActiveResumeSection.Any(ar => ar.ResumeSectionType.SectionType == "Skills")
                    ? resume.ResumeSkills.Select(rs => new ResumeSkillsDTO
                    {
                        Id = rs.Id,
                        ResumeId = rs.ResumeId,
                        SkillName = rs.SkillName,
                    }).ToList()
                    : null,
            }).ToList();

            return Ok(resumeDTOs);
        }


        [HttpGet("active_resume")]
        public async Task<IActionResult> GetActiveResumeByUserId([FromQuery] string userId)
        {
            Console.WriteLine("Start GetActiveResumeByUserId");
            int? intId = 0;

            if (!string.IsNullOrEmpty(userId) && userId != "NULL")
            {
                var user = await _userController.GetUserById(userId);

                if (user == null)
                {
                    return NotFound($"Помилка користувача {userId}");
                }

                if (user.UserInfo == null) 
                {
                    return NotFound($"Помилка користувача {userId}");
                }

                intId = user.UserInfo.Id;
            }

            // Get all resumes of the user
            var resumes = await _context.Resume
                .Include(r => r.UserInfo)
                .Include(r => r.ActiveResumeSection)
                    .ThenInclude(ar => ar.ResumeSectionType)
                .Include(r => r.ResumeEducation)
                    .ThenInclude(re => re.Industry)
                .Include(r => r.ResumeEducation)
                    .ThenInclude(re => re.Location)
                .Include(r => r.ResumeLanguage)
                    .ThenInclude(rl => rl.LanguageLevel)
                .Include(r => r.ResumeLinks)
                .Include(r => r.ResumePortfolio)
                .Include(r => r.ResumeSkills)
                .Include(r => r.ResumeAboutMe)
                .Include(r => r.ResumeHistoryWork)
                     .ThenInclude(rl => rl.Employer)
                .Include(r => r.ResumeTags)
                    .ThenInclude(rl => rl.TagsList)
                .Where(r => r.UserId == intId)
                .ToListAsync();

            if (!resumes.Any())
            {
                return NotFound("No resumes found for the user.");
            }

            // Find the active resume
            var activeResume = resumes.FirstOrDefault(r => r.ResumeActive);

            // If there is no active resume, make the first one active
            if (activeResume == null)
            {
                activeResume = resumes.First();
                activeResume.ResumeActive = true;
                await _context.SaveChangesAsync();
            }

            // Map the active resume to DTO
            var resumeDTO = new ResumeDTO
            {
                Id = activeResume.Id,
                UserId = activeResume.UserId,
                ResumeName = activeResume.ResumeName,
                ResumeDescription = activeResume.ResumeDescription,
                ResumeEmail = activeResume.ResumeEmail,
                ResumePhone = activeResume.ResumePhone,
                WantedSalary = activeResume.WantedSalary,

                ResumeActive = activeResume.ResumeActive,

                ResumeTags = activeResume.ResumeTags != null ? activeResume.ResumeTags.Select(t => new ResumeTagsDTO
                {
                    Id = t.id,
                    ResumeTagName = t.TagsList.tags_name,
                }).ToList() : null,

                ResumeAboutMe = activeResume.ResumeAboutMe != null ? new ResumeAboutMeDTO
                {
                    Id = activeResume.ResumeAboutMe.Id,
                    ResumeAboutMeText = activeResume.ResumeAboutMe.ResumeAboutMeText,
                } : null,

                UserInfo = activeResume.UserInfo != null ? new UserDTO
                {
                    Id = activeResume.UserInfo.Id,
                    UserId = activeResume.UserInfo.user_Id,
                    Email = activeResume.UserInfo.ApplicationUser != null ? activeResume.UserInfo.ApplicationUser.UserName : null,
                    FirstName = activeResume.UserInfo.FirstName,
                    LastName = activeResume.UserInfo.LastName,
                    UserAge = CalculateAge(activeResume.UserInfo.DateOfBirth),
                    PhoneNumber = activeResume.UserInfo.PhoneNumber,
                    UserCreatedAt = activeResume.UserInfo.ActionCreatedAt,
                    UserImg = activeResume.UserInfo.UserImg,
                    Location = activeResume.UserInfo.Location != null ? new Location
                    {
                        id = activeResume.UserInfo.Location.id,
                        location_city = activeResume.UserInfo.Location.location_city,
                        location_region = activeResume.UserInfo.Location.location_region,
                        location_country = activeResume.UserInfo.Location.location_country,
                    } : null,
                } : null,

                ActiveResumeSection = activeResume.ActiveResumeSection.Select(ar => new ActiveResumeSectionDTO
                {
                    Id = ar.Id,
                    ResumeId = ar.ResumeId,
                    SectionId = ar.SectionId,
                    ResumeSectionType = ar.ResumeSectionType != null ? new ResumeSectionTypeDTO
                    {
                        Id = ar.ResumeSectionType.Id,
                        SectionType = ar.ResumeSectionType.SectionType,
                    } : null,
                }).ToList(),

                ResumeEducation = activeResume.ActiveResumeSection.Any(ar => ar.ResumeSectionType.SectionType == "Освіта")
                    ? activeResume.ResumeEducation.Select(re => new ResumeEducationDTO
                    {
                        Id = re.Id,
                        ResumeId = re.ResumeId,
                        EducationProffesion = re.EducationProffesion,
                        IndustryId = re.IndustryId,
                        LocationId = re.LocationId,
                        EducationYear = re.EducationYear,
                        Industry = re.Industry != null ? new IndustryDataDTO
                        {
                            id = re.Industry.id,
                            industry_name = re.Industry.industry_name,
                        } : null,
                        Location = re.Location != null ? new LocationDataDTO
                        {
                            id = re.Location.id,
                            location_city = re.Location.location_city,
                            location_region = re.Location.location_region,
                            location_country = re.Location.location_country,
                        } : null,
                    }).ToList()
                    : null,

                ResumeLanguage = activeResume.ActiveResumeSection.Any(ar => ar.ResumeSectionType.SectionType == "Мови")
                    ? activeResume.ResumeLanguage.Select(rl => new ResumeLanguageDTO
                    {
                        Id = rl.Id,
                        ResumeId = rl.ResumeId,
                        Languale = rl.Languale,
                        LevelId = rl.LevelId,
                        LanguageLevel = rl.LanguageLevel != null ? new LanguageLevelDTO
                        {
                            Id = rl.LanguageLevel.id,
                            Level = rl.LanguageLevel.LangualeLevel,
                        } : null,
                    }).ToList()
                    : null,

                ResumeLinks = activeResume.ActiveResumeSection.Any(ar => ar.ResumeSectionType.SectionType == "Поєднані акаунти")
                    ? activeResume.ResumeLinks.Select(rl => new ResumeLinksDTO
                    {
                        Id = rl.Id,
                        ResumeId = rl.ResumeId,
                        Type = rl.Type,
                        Link = rl.Link,
                        AccountName = rl.AccountName,
                    }).ToList()
                    : null,

                ResumePortfolio = activeResume.ActiveResumeSection.Any(ar => ar.ResumeSectionType.SectionType == "Портфоліо")
                    ? activeResume.ResumePortfolio.Select(rp => new ResumePortfolioDTO
                    {
                        Id = rp.Id,
                        ResumeId = rp.ResumeId,
                        PortfolioName = rp.PortfolioName,
                        PortfolioImg = rp.PortfolioImg,
                        PortfolioLink = rp.PortfolioLink,
                    }).ToList()
                    : null,

                ResumeSkills = activeResume.ActiveResumeSection.Any(ar => ar.ResumeSectionType.SectionType == "Навички")
                    ? activeResume.ResumeSkills.Select(rs => new ResumeSkillsDTO
                    {
                        Id = rs.Id,
                        ResumeId = rs.ResumeId,
                        SkillName = rs.SkillName,
                    }).ToList()
                    : null,


                ResumeHistoryWork = activeResume.ActiveResumeSection.Any(ar => ar.ResumeSectionType.SectionType == "Історія роботи")
                    ? activeResume.ResumeHistoryWork.Select(rl => new ResumeHistoryWorkDTO
                    {
                        Id = rl.Id,
                        ResumeId = rl.ResumeId,
                        WorkName = rl.WorkName,
                        Employer = new EmployerWorkExperinceShortDTO
                        {
                            Id = rl.Employer != null && rl.Employer.id != null ? rl.Employer.id : 0,
                            CompanyName = rl.Employer != null && rl.Employer.company_name != null ? rl.Employer.company_name : rl.CompanyName,
                            CompanyShortDescription = rl.Employer != null && rl.Employer.company_short_description != null ? rl.Employer.company_short_description : rl.CompanyDescription,
                            CompanyURL = rl.Employer != null && rl.Employer.company_url != null ? rl.Employer.company_url : rl.CompanyLink,
                        },
                        StartWorkDate = rl.StartWorkDate,
                        EndWorkDate = rl.EndWorkDate,
                        WorkText = rl.WorkText,
                    }).ToList()
                    : null,
            };

            return Ok(resumeDTO);
        }


        public class CreateResumeRequest
        {
            public string? UserId { get; set; }
            public string? ResumeName { get; set; }
        }



        public class Result
        {
            public string? phone { get; set; }
            public string? email { get; set; }
            public string? address { get; set; }
            public string[]? skills { get; set; }

        }

        public class CreateResumeRequestWithData
        {
            public string? UserId { get; set; }
            public string? name { get; set; }
            public Result? result { get; set; }

        }



        [HttpPost("create_resume_with_data")]
        public async Task<IActionResult> CreateEmptyResume([FromBody] CreateResumeRequestWithData CreateResumeRequestWithData)
        {
            Console.WriteLine(CreateResumeRequestWithData.name);
            Console.WriteLine(CreateResumeRequestWithData.result.phone);

            Console.WriteLine(CreateResumeRequestWithData.result.email);
            Console.WriteLine(CreateResumeRequestWithData.result.address);
            Console.WriteLine(CreateResumeRequestWithData.result.skills);
            Console.WriteLine("Start");

            if (string.IsNullOrEmpty(CreateResumeRequestWithData.name))
            {
                return BadRequest("Назва резюме не може бути порожньою.");
            }

            // Отримуємо користувача за його ID
            var user = await _userController.GetUserById(CreateResumeRequestWithData.UserId);

            if (user == null)
            {
                return NotFound($"Помилка користувача {CreateResumeRequestWithData.UserId}");
            }

            if (user.UserInfo == null)
            {
                return NotFound($"Помилка користувача {CreateResumeRequestWithData.UserId}");
            }

            // Створюємо новий об'єкт резюме
            var newResume = new Resume
            {
                UserId = user.UserInfo.Id,
                ResumeName = CreateResumeRequestWithData.name,
                ResumeDescription = null,
                ResumeEmail = CreateResumeRequestWithData.result.email != null && CreateResumeRequestWithData.result.email != "" ? CreateResumeRequestWithData.result.email : null ,
                ResumePhone = CreateResumeRequestWithData.result.phone != null && CreateResumeRequestWithData.result.phone != "" ? CreateResumeRequestWithData.result.phone : null,
                WantedSalary = null,
                ResumeActive = true,
                ResumeTags = null,
                ResumeAboutMe = null,
                ActiveResumeSection = new List<ActiveResumeSection>(),
                ResumeEducation = new List<ResumeEducation>(),
                ResumeLanguage = new List<ResumeLanguage>(),
                ResumeLinks = new List<ResumeLinks>(),
                ResumePortfolio = new List<ResumePortfolio>(),
                ResumeSkills = new List<ResumeSkills>(),
                ResumeHistoryWork = new List<ResumeHistoryWork>()
            };

            // Зберігаємо резюме у базі даних
            _context.Resume.Add(newResume);
            await _context.SaveChangesAsync();



            // Додаємо навички до ResumeSkills
            if (CreateResumeRequestWithData.result.skills != null && CreateResumeRequestWithData.result.skills.Length > 0)
            {
                foreach (var skill in CreateResumeRequestWithData.result.skills)
                {
                    var newResumeSkill = new ResumeSkills
                    {
                        ResumeId = (int)newResume.Id,
                        SkillName = skill
                    };
                    _context.ResumeSkills.Add(newResumeSkill);
                }
                await _context.SaveChangesAsync();
            }



            var newActiveSection = new ActiveResumeSection
            {
                ResumeId = (int)newResume.Id,
                SectionId = 1 // ID розділу для навичок
            };
            _context.ActiveResumeSections.Add(newActiveSection);
            await _context.SaveChangesAsync();


            // Додаємо активний розділ для навичок (за припущенням, що ID розділу для навичок - 1)
            if (CreateResumeRequestWithData.result.skills != null && CreateResumeRequestWithData.result.skills.Length > 0)
            {
                var newActiveSection2 = new ActiveResumeSection
                {
                    ResumeId = (int)newResume.Id,
                    SectionId = 3 // ID розділу для навичок
                };
                _context.ActiveResumeSections.Add(newActiveSection2);
                await _context.SaveChangesAsync();
            }


            if (CreateResumeRequestWithData.result.phone != null || CreateResumeRequestWithData.result.phone != null)
            {
                var newActiveSection2 = new ActiveResumeSection
                {
                    ResumeId = (int)newResume.Id,
                    SectionId = 2 // ID розділу для навичок
                };
                _context.ActiveResumeSections.Add(newActiveSection2);
                await _context.SaveChangesAsync();
            }


            // Маппимо нове резюме на DTO для відповіді
            var resumeDTO = new ResumeDTO
            {
                Id = newResume.Id,
                UserId = newResume.UserId,
                ResumeName = newResume.ResumeName,
                ResumeDescription = newResume.ResumeDescription,
                ResumeEmail = newResume.ResumeEmail,
                ResumePhone = newResume.ResumePhone,
                WantedSalary = newResume.WantedSalary,
                ResumeActive = newResume.ResumeActive,
                ResumeTags = null,
                ResumeAboutMe = null,
                UserInfo = new UserDTO
                {
                    Id = user.UserInfo.Id,
                    UserId = user.UserInfo.user_Id,
                    Email = user.UserInfo.ApplicationUser?.UserName,
                    FirstName = user.UserInfo.FirstName,
                    LastName = user.UserInfo.LastName,
                    UserAge = CalculateAge(user.UserInfo.DateOfBirth),
                    PhoneNumber = user.UserInfo.PhoneNumber,
                    UserCreatedAt = user.UserInfo.ActionCreatedAt,
                    UserImg = user.UserInfo.UserImg,
                    Location = user.UserInfo.Location != null ? new Location
                    {
                        id = user.UserInfo.Location.id,
                        location_city = user.UserInfo.Location.location_city,
                        location_region = user.UserInfo.Location.location_region,
                        location_country = user.UserInfo.Location.location_country,
                    } : null,
                },
                ActiveResumeSection = new List<ActiveResumeSectionDTO>(),
                ResumeEducation = new List<ResumeEducationDTO>(),
                ResumeLanguage = new List<ResumeLanguageDTO>(),
                ResumeLinks = new List<ResumeLinksDTO>(),
                ResumePortfolio = new List<ResumePortfolioDTO>(),
                ResumeSkills = new List<ResumeSkillsDTO>(),
                ResumeHistoryWork = new List<ResumeHistoryWorkDTO>()
            };

            return Ok(resumeDTO);
        }



        [HttpPost("create_empty_resume")]
        public async Task<IActionResult> CreateEmptyResume([FromBody] CreateResumeRequest createResumeRequest)
        {
            if (string.IsNullOrEmpty(createResumeRequest.ResumeName))
            {
                return BadRequest("Назва резюме не може бути порожньою.");
            }

            Console.WriteLine(createResumeRequest);

            // Отримуємо користувача за його ID
            var user = await _userController.GetUserById(createResumeRequest.UserId);

            if (user == null)
            {
                return NotFound($"Помилка користувача {createResumeRequest.UserId}");
            }

            if (user.UserInfo == null)
            {
                return NotFound($"Помилка користувача {createResumeRequest.UserId}");
            }

            // Створюємо новий об'єкт резюме
            var newResume = new Resume
            {
                UserId = user.UserInfo.Id,
                ResumeName = createResumeRequest.ResumeName,
                ResumeDescription = null,
                ResumeEmail = null,
                ResumePhone = null,
                WantedSalary = null,
                ResumeActive = true,
                ResumeTags = null,
                ResumeAboutMe = null,
                ActiveResumeSection = new List<ActiveResumeSection>(),
                ResumeEducation = new List<ResumeEducation>(),
                ResumeLanguage = new List<ResumeLanguage>(),
                ResumeLinks = new List<ResumeLinks>(),
                ResumePortfolio = new List<ResumePortfolio>(),
                ResumeSkills = new List<ResumeSkills>(),
                ResumeHistoryWork = new List<ResumeHistoryWork>()
            };

            // Зберігаємо резюме у базі даних
            _context.Resume.Add(newResume);
            await _context.SaveChangesAsync();



            var newActiveSection = new ActiveResumeSection
            {
                ResumeId = (int)newResume.Id,
                SectionId = 1 // ID розділу для навичок
            };
            _context.ActiveResumeSections.Add(newActiveSection);
            await _context.SaveChangesAsync();


            // Маппимо нове резюме на DTO для відповіді
            var resumeDTO = new ResumeDTO
            {
                Id = newResume.Id,
                UserId = newResume.UserId,
                ResumeName = newResume.ResumeName,
                ResumeDescription = newResume.ResumeDescription,
                ResumeEmail = newResume.ResumeEmail,
                ResumePhone = newResume.ResumePhone,
                WantedSalary = newResume.WantedSalary,
                ResumeActive = newResume.ResumeActive,
                ResumeTags = null,
                ResumeAboutMe = null,
                UserInfo = new UserDTO
                {
                    Id = user.UserInfo.Id,
                    UserId = user.UserInfo.user_Id,
                    Email = user.UserInfo.ApplicationUser?.UserName,
                    FirstName = user.UserInfo.FirstName,
                    LastName = user.UserInfo.LastName,
                    UserAge = CalculateAge(user.UserInfo.DateOfBirth),
                    PhoneNumber = user.UserInfo.PhoneNumber,
                    UserCreatedAt = user.UserInfo.ActionCreatedAt,
                    UserImg = user.UserInfo.UserImg,
                    Location = user.UserInfo.Location != null ? new Location
                    {
                        id = user.UserInfo.Location.id,
                        location_city = user.UserInfo.Location.location_city,
                        location_region = user.UserInfo.Location.location_region,
                        location_country = user.UserInfo.Location.location_country,
                    } : null,
                },
                ActiveResumeSection = new List<ActiveResumeSectionDTO>(),
                ResumeEducation = new List<ResumeEducationDTO>(),
                ResumeLanguage = new List<ResumeLanguageDTO>(),
                ResumeLinks = new List<ResumeLinksDTO>(),
                ResumePortfolio = new List<ResumePortfolioDTO>(),
                ResumeSkills = new List<ResumeSkillsDTO>(),
                ResumeHistoryWork = new List<ResumeHistoryWorkDTO>()
            };

            return Ok(resumeDTO);
        }





        [HttpGet("sections_list")]
        public async Task<IActionResult> GetResumeSectionList()
        {

            // Get all resumes of the user
            var sections = await _context.ResumeSectionType.ToListAsync();

            if (!sections.Any())
            {
                return NotFound("Помилка");
            }

            // Мапінг секцій до DTO
            var sectionsDTO = sections.Select(section => new ResumeSectionTypeDTO
            {
                Id = section.Id,
                SectionType = section.SectionType
            }).ToList();

            return Ok(sectionsDTO);
        }

        public class SectionRequest
        {
            public int ResumeId { get; set; }
            public int SectionId { get; set; }
        }


        [HttpPost("add_active_section")]
        public async Task<IActionResult> AddSection([FromBody] SectionRequest request)
        {
            Console.WriteLine("ADD", request.ResumeId, request.SectionId);

            await AddSectionAsync(request.ResumeId, request.SectionId);
            return Ok();
        }

        [HttpDelete("remove_active_section")]
        public async Task<IActionResult> RemoveSection([FromBody] SectionRequest request)
        {
            Console.WriteLine("DElete", request.ResumeId, request.SectionId);

            await RemoveSectionAsync(request.ResumeId, request.SectionId);
            return Ok();
        }


        private async Task AddSectionAsync(int resumeId, int sectionId)
        {
            Console.WriteLine("ADD", resumeId.ToString(), sectionId);

            var activeResumeSection = new ActiveResumeSection
            {
                ResumeId = resumeId,
                SectionId = sectionId
            };

            _context.ActiveResumeSections.Add(activeResumeSection);
            await _context.SaveChangesAsync();
        }


        private async Task RemoveSectionAsync(int resumeId, int sectionId)
        {
            Console.WriteLine("DElete", resumeId.ToString(), sectionId);

            var activeResumeSection = await _context.ActiveResumeSections
                .FirstOrDefaultAsync(x => x.ResumeId == resumeId && x.SectionId == sectionId);

            if (activeResumeSection != null)
            {
                _context.ActiveResumeSections.Remove(activeResumeSection);
                await _context.SaveChangesAsync();
            }
        }



        private int? CalculateAge(DateTime? dateOfBirth)
        {
            if (dateOfBirth.HasValue)
            {
                var today = DateTime.Today;
                var birthDate = dateOfBirth.Value;
                var age = today.Year - birthDate.Year;
                if (birthDate > today.AddYears(-age)) age--;
                return age;
            }
            else
            {
                return null;
            }
        }








        /*[HttpPost("old-upload-resume-parser")]
        public async Task<IActionResult> OldUpload(IFormFile file)
        {
            Console.WriteLine("Start");
            if (file == null || file.Length == 0)
                return BadRequest("Upload a file");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "uploads", file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Читання тексту з PDF
            string text = ExtractTextFromPdf(path);

            Console.WriteLine(text);
            // Відправка тексту до ChatGPT та отримання результатів

*//*            var structuredData = await SendTextToChatGPT(text);
*//*
            return Ok(text);
        }*/

    /*    [HttpGet("upload-resume-parser")]
        public async Task<IActionResult> Upload([FromQuery] string fileUrl)
        {
            Console.WriteLine("Start");
            string blobUrl = fileUrl; // Замініть на реальний URL-адрес вашого файлу у Blob Storage

            // Читання тексту з PDF (якщо це потрібно)
            string text = ExtractTextFromPdf(blobUrl);

            Console.WriteLine(text);

            var client = new RestClient("https://api.apilayer.com/resume_parser/url?url=" + fileUrl);

            var request = new RestRequest(Method.GET);
            request.Timeout = TimeSpan.FromSeconds(50); // Встановлення таймауту як об'єкта TimeSpan

            request.AddHeader("apikey", "ZYmkCUSoJy3iZpj25wu72A8A6nTgGSgM");

            var response = client.Execute(request);
            Console.WriteLine(response.Content);

            // Повернення URL-адреси файлу або іншої інформації
            return Ok(response.Content);
        }*/


       /* [HttpPost("old-old-upload-resume-parser")]
        public async Task<IActionResult> Upload([FromQuery] string fileUrl)
        {
            Console.WriteLine("Start");
            string blobUrl = fileUrl; // Замініть на реальний URL-адрес вашого файлу у Blob Storage

            // Читання тексту з PDF (якщо це потрібно)
            string text = ExtractTextFromPdf(blobUrl);

            Console.WriteLine(text);

            *//*      var chatGPTService = new ChatGPTService();
                  var structuredData = await chatGPTService.GetStructuredDataFromChatGPT(text);*//*

            var url = "https://pdfobject.com/pdf/sample.pdf";
            Console.WriteLine(fileUrl);
            var client = new RestClient("https://api.apilayer.com/resume_parser/url?url=" + fileUrl);

            var request = new RestRequest("GET");
            request.Timeout = TimeSpan.FromSeconds(50); // Встановлення таймауту як об'єкта TimeSpan

            request.AddHeader("apikey", "ZYmkCUSoJy3iZpj25wu72A8A6nTgGSgM");

            var response = client.Execute(request);
            Console.WriteLine(response.Content);


            // Повернення URL-адреси файлу або іншої інформації
            return Ok(text);
        }
*/

        private string ExtractTextFromPdf(string filePath)
        {
            using (PdfReader reader = new PdfReader(filePath))
            using (PdfDocument pdfDoc = new PdfDocument(reader))
            {
                var strategy = new SimpleTextExtractionStrategy();
                string text = PdfTextExtractor.GetTextFromPage(pdfDoc.GetFirstPage(), strategy);
                return text;
            }
        }

        private async Task<string> SendTextToChatGPT(string text)
        {
            return await Task.FromResult("structured data from ChatGPT");
        }

    }
}
