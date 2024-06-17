using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using Web_search_job.Data;
using Web_search_job.DatabaseClasses;
using Web_search_job.DatabaseClasses.EmployerFolder;
using Web_search_job.DatabaseClasses.FiltersFolder;
using Web_search_job.DatabaseClasses.JobFolder;
using Web_search_job.DatabaseClasses.ProfileFolder;
using Web_search_job.DTO;
using Web_search_job.DTO.Employer;
using Web_search_job.DTO.Job;
using Web_search_job.DTO.User;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Web_search_job.Controllers.DatabaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployerController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly UserController _userController;

        public EmployerController(DataContext context, UserController userController)
        {
            _context = context;
            _userController = userController;
        }

        [HttpGet("short")]
        public async Task<ActionResult<List<EmployerShortDTO>>> GetShortDataEmployer(int page = 1, int pageSize = 18)
        {
            var startIndex = (page - 1) * pageSize;

            var employersWithTopRatingTag = await _context.Employers
                .Include(e => e.CommentToEmployer)
                .Include(e => e.CompanyTags)
                    .ThenInclude(ct => ct.TagsList)
                .Where(e => e.CompanyTags.Any(t => t.type_tag == "rating"))
                .Select(e => new EmployerShortDTO
                {
                    Id = e.id,
                    CompanyName = e.company_name,
                    CompanyIndustryDescription = e.company_industry_description,
                    CompanyShortDescription = e.company_short_description,
                    CompanyChecked = e.company_checked,
                    CompanyURL = e.company_url,
                    CompanyYear = e.company_year_experience,
                    CompanyRating = e.CommentToEmployer.Any() ? e.CommentToEmployer.Average(c => c.comment_stars) : 0,
                    CompanyImg = e.company_img,
                    EmployerCreatedAt = e.employer_created_at,
                    CommentCount = e.CommentToEmployer.Count(),
                    Comments = e.CommentToEmployer.Select(c => new CommentStarsDTO
                    {
                        Id = c.id,
                        CommentStars = c.comment_stars,
                    }).ToList(),
                    Tags = e.CompanyTags.Select(t => new EmployerTagDTO
                    {
                        Id = t.id,
                        TagType = t.type_tag,
                        TagName = t.TagsList.tags_name,
                    }).ToList()
                })
                .OrderByDescending(e => e.CommentCount)
                .ToListAsync();


            var otherEmployers = await _context.Employers
                .Include(c => c.CommentToEmployer)
                .Include(c => c.CompanyTags)
                        .ThenInclude(ct => ct.TagsList) // Завантажити пов'язані CompanyTagsList
                .Where(e => !e.CompanyTags.Any(t => t.type_tag == "rating"))
                .Where(e => e.company_status == "OK")
                .Select(e => new EmployerShortDTO
                {
                    Id = e.id,
                    CompanyName =  e.company_name,
                    CompanyIndustryDescription = e.company_industry_description,
                    CompanyShortDescription = e.company_short_description,
                    CompanyChecked = e.company_checked,
                    CompanyURL = e.company_url,
                    CompanyYear = e.company_year_experience,
                    CompanyRating = e.CommentToEmployer.Any() ? e.CommentToEmployer.Average(c => c.comment_stars) : 0,
                    CompanyImg = e.company_img,
                    EmployerCreatedAt = e.employer_created_at,
                    CommentCount = e.CommentToEmployer.Any() ? e.CommentToEmployer.Count() : 0,
                    Comments = e.CommentToEmployer.Select(c => new CommentStarsDTO
                    {
                        Id = c.id,
                        CommentStars = c.comment_stars,
                    }).ToList(),
                    Tags = e.CompanyTags
                    .Select(t => new EmployerTagDTO
                    {
                        Id = t.id,
                        TagType = t.type_tag,
                        TagName = t.TagsList.tags_name,
                    }).ToList()
                })
                .OrderByDescending(e => e.CommentCount)
                .ToListAsync();

            var sortedEmployers = employersWithTopRatingTag
            .Concat(otherEmployers)
            .Skip(startIndex)
            .Take(pageSize)
            .ToList();

            return Ok(sortedEmployers);
        }

        [HttpGet("count")]
        public async Task<ActionResult<List<object>>> GetCountEmployers()
        {
            var result = await _context.Employers.CountAsync();

            return Ok(result);
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<EmployerDTO>> GetEmployerById(int id, string? userId)
        {
            string userLocation = null;
            int? userIntId = null;
     
            if (userId != null && userId != "" && userId != "NULL" && userId != "null")
            {
                var user = await _userController.GetUserById(userId);

                if (user != null)
                {
                    userIntId = user.UserInfo.Id;
                    userLocation = user.UserInfo.Location.location_region;
                }

            }


            List<SavedJob> savedJobs = new List<SavedJob>();

            savedJobs = await _context.SavedJobs
                    .Where(sj => sj.user_id == userIntId)
                    .ToListAsync();

            var savedJobIds = savedJobs.Select(sj => sj.job_id).ToList();

            var user_ = await _context.UsersInfo
             .Include(ui => ui.ApplicationUser) // Включаємо дані користувача з таблиці ApplicationUser
             .Include(ui => ui.Location) // Включаємо дані про місцезнаходження
             .Where(ui => ui.UsersOfEmployer.employer_id == id)
             .Select(ui => new UserDTO
             {
                 Id = ui.Id,
                 UserId = ui.user_Id,
                 Email = ui.ApplicationUser != null ? ui.ApplicationUser.UserName : null,
                 FirstName = ui.FirstName,
                 LastName = ui.LastName,
                 UserAge = CalculateAge(ui.DateOfBirth),
                 PhoneNumber = ui.PhoneNumber,
                 UserCreatedAt = ui.ActionCreatedAt,
                 UserImg = ui.UserImg,
                 Gender = ui.Gender,
                 Location = ui.Location != null ? new Location
                 {
                     id = ui.Location.id,
                     location_city = ui.Location.location_city,
                     location_region = ui.Location.location_region,
                     location_country = ui.Location.location_country,
                 } : null,
             })
             .ToListAsync();

            var employer = await _context.Employers
                .Include(e => e.UsersOfEmployer)
                         .ThenInclude(ei => ei.UserInfo)
                /*.Include(e => e.Industry)
                .Include(e => e.CommentToEmployer)
                    .ThenInclude(c => c.UserInfo)
                         .ThenInclude(us => us.ApplicationUser)
                .Include(e => e.UsersOfEmployer)
                         .ThenInclude(ei => ei.UserInfo)
                                .ThenInclude(ea => ea.ApplicationUser)
                .Include(e => e.UsersOfEmployer)
                         .ThenInclude(ei => ei.UserInfo)
                                .ThenInclude(ea => ea.Location)
                .Include(e => e.CompanyTags)
                    .ThenInclude(ct => ct.CompanyTagsList)
                .Include(e => e.Jobs)
                    .ThenInclude(j => j.JobTagsMarks)*/
                .Where(e => e.id == id)
                .Select(e => new EmployerDTO
                {
                    Id = e.id,
                    CompanyName = e.company_name,
                    CompanyIndustryDescription = e.company_industry_description,
                    CompanyShortDescription = e.company_short_description,
                    CompanyChecked = e.company_checked,
                    CompanyURL = e.company_url,
                    CompanyYear = e.company_year_experience,
                    CompanyRating = e.CommentToEmployer.Any() ? e.CommentToEmployer.Average(c => c.comment_stars) : 0,
                    CompanyImg = e.company_img,
                    CompanyBackgroundImg = e.company_background_img,
                    EmployerCreatedAt = e.employer_created_at,
                    JobsCount = e.Jobs.Any() ? e.Jobs.Count() : 0,
                    CommentCount = e.CommentToEmployer.Count(),
                    NumberWorkers = e.number_workers,
                    CompanyDescription = e.company_description,
                    CompanyStatus = e.company_status,
                    Comments = e.CommentToEmployer.Select(c => new CommentDTO
                    {
                        Id = c.id,
                        User = c.UserInfo != null ? new CommentUserDTO
                        {
                            Id = c.UserInfo.Id,
                            UserId = c.UserInfo.user_Id,
                            UserName = c.UserInfo.ApplicationUser != null ? c.UserInfo.ApplicationUser.UserName : null,
                            WorkNow = "Temporary",
                        } : null,
                        CommentStars = c.comment_stars,
                        CommentTitle = c.comment_title,
                        CommentText = c.comment_text,
                        CommentsToEmployerCreatedAt = c.comments_to_employer_created_at
                    }).ToList(),
                    Tags = e.CompanyTags != null ? e.CompanyTags.Select(t => new EmployerTagDTO
                    {
                        Id = t.id,
                        TagType = t.type_tag,
                        TagName = t.TagsList.tags_name,
                    }).ToList() : null,
                    Location = e.Location != null ? new Location
                    {
                        id = e.Location.id,
                        location_city = e.Location.location_city,
                        location_region = e.Location.location_region,
                        location_country = e.Location.location_country,
                    } : null,
                    //Location = e.Location,

                    Users = user_ != null ? user_ : null,

                    /*Users = e.UsersOfEmployer != null ? e.UsersOfEmployer
                    .Select(ei => ei.UserInfo)
                    .Select(ei => new UserDTO
                    {
                        Id = ei.Id,
                        UserId = ei.user_Id,
                        UserName = ei.ApplicationUser != null ? ei.ApplicationUser.UserName : null,
                        PhoneNumber = ei.PhoneNumber,
                        UserCreatedAt = ei.ActionCreatedAt,
                        UserImg = ei.UserImg,
                        Location = ei.Location != null ? new Location
                        {
                            id = ei.Location.id,
                            location_city = ei.Location.location_city,
                            location_region = ei.Location.location_region,
                            location_country = ei.Location.location_country,
                        } : null,
                    }).ToList() : null,*/
                    Jobs = e.Jobs != null ? e.Jobs.Select(j => new JobEmployerShortDTO
                    {
                        Id = j.id,
                        JobTitle = j.job_title,
                        JobSalaryMin = j.job_salary_min,
                        JobSalaryMax = j.job_salary_max,
                        JobSalaryCurrency = j.job_salary_currency,
                        DateEnding = j.date_ending,
                        DateLastEditing = j.date_last_editing,
                        DateApproving = j.date_approving,
                        Status = j.status,
                        CreatedAt = j.created_at,
                        isSavedJob = savedJobIds.Contains((int)j.id),
                        JobTagsMarksDTO = j.JobTagsMarks != null ? new JobTagsMarksDTO
                        {
                            Id = j.JobTagsMarks.id,
                            TagHot = j.JobTagsMarks.tag_hot,
                            TagNew = j.JobTagsMarks.tag_new,
                            TagRecommend = _context.JobRecommendationList.Any(r => r.JobId == j.id && r.UserId == userIntId),
                        } : null,
                        Industry = j.Industry != null ? new Industry
                        {
                            id = j.Industry.id,
                            industry_name = j.Industry.industry_name,
                        } : null,
                        Location = j.Location != null ? new Location
                        {
                            id = j.Location.id,
                            location_city = j.Location.location_city,
                            location_region = j.Location.location_region,
                            location_country = j.Location.location_country,
                        } : null,
                        /*                      Industry = j.Industry,
                                                Location = j.Location,*/
                    }).ToList() : null,
                    Industry = e.Industry != null ? new Industry
                    {
                        id = e.Industry.id,
                        industry_name = e.Industry.industry_name,
                    } : null,
                    //Industry = e.Industry,
                })
                .FirstOrDefaultAsync();

            Console.WriteLine(user_);

            if (employer == null)
            {
                return NotFound();
            }


            if (!string.IsNullOrEmpty(userLocation))
            {
                employer.Jobs = employer.Jobs != null ? employer.Jobs
                   .OrderByDescending(j => j.JobTagsMarksDTO?.TagHot ?? false)
                   .ThenByDescending(j => j.JobTagsMarksDTO?.TagRecommend ?? false)
                   .ThenByDescending(j => j.JobTagsMarksDTO?.TagNew ?? false)
                   .ThenByDescending(j => j.Location != null && j.Location.location_region == userLocation)
                   .ThenByDescending(j => j.CreatedAt)
                   .ToList() : null;
            }
            else
            {
                employer.Jobs = employer.Jobs != null ? employer.Jobs
                    .OrderByDescending(j => j.JobTagsMarksDTO?.TagHot ?? false)
                    .ThenByDescending(j => j.JobTagsMarksDTO?.TagRecommend ?? false)
                    .ThenByDescending(j => j.JobTagsMarksDTO?.TagNew ?? false)
                    .ThenByDescending(j => j.CreatedAt)
                    .ToList() : null;
            }
            return Ok(employer);
        }



        [HttpGet("short_6_limit")]
        public async Task<ActionResult<IEnumerable<EmployerShortDTO>>> GetShortData6LimitEmployer()
        {
            var employersWithTopRatingTag = await _context.Employers
                .Include(e => e.CommentToEmployer)
                .Include(e => e.CompanyTags)
                    .ThenInclude(ct => ct.TagsList)
                .Where(e => e.CompanyTags.Any(t => t.type_tag == "rating"))
                .Where(e => e.company_status == "OK")
                .Select(e => new EmployerShortDTO
                {
                    Id = e.id,
                    CompanyName = e.company_name,
                    CompanyIndustryDescription = e.company_industry_description,
                    CompanyShortDescription = e.company_short_description,
                    CompanyChecked = e.company_checked,
                    CompanyURL = e.company_url,
                    CompanyYear = e.company_year_experience,
                    CompanyRating = e.CommentToEmployer.Any() ? e.CommentToEmployer.Average(c => c.comment_stars) : 0,
                    CompanyImg = e.company_img,
                    EmployerCreatedAt = e.employer_created_at,
                    CommentCount = e.CommentToEmployer.Count(),
                    Comments = e.CommentToEmployer.Select(c => new CommentStarsDTO
                    {
                        Id = c.id,
                        CommentStars = c.comment_stars,
                    }).ToList(),
                    Tags = e.CompanyTags.Select(t => new EmployerTagDTO
                    {
                        Id = t.id,
                        TagType = t.type_tag,
                        TagName = t.TagsList.tags_name,
                    }).ToList()
                })
                .OrderByDescending(e => e.CommentCount)
                .ToListAsync();


            var otherEmployers = await _context.Employers
                .Include(c => c.CommentToEmployer)
                .Include(c => c.CompanyTags)
                        .ThenInclude(ct => ct.TagsList) // Завантажити пов'язані CompanyTagsList
                .Where(e => !e.CompanyTags.Any(t => t.type_tag == "rating"))
                .Where(e => e.company_status == "OK")
                .Select(e => new EmployerShortDTO
                {
                    Id = e.id,
                    CompanyName = e.company_name,
                    CompanyIndustryDescription = e.company_industry_description,
                    CompanyShortDescription = e.company_short_description,
                    CompanyChecked = e.company_checked,
                    CompanyURL = e.company_url,
                    CompanyYear = e.company_year_experience,
                    CompanyRating = e.CommentToEmployer.Any() ? e.CommentToEmployer.Average(c => c.comment_stars) : 0,
                    CompanyImg = e.company_img,
                    EmployerCreatedAt = e.employer_created_at,
                    CommentCount = e.CommentToEmployer.Any() ? e.CommentToEmployer.Count() : 0,
                    Comments = e.CommentToEmployer.Select(c => new CommentStarsDTO
                    {
                        Id = c.id,
                        CommentStars = c.comment_stars,
                    }).ToList(),
                    Tags = e.CompanyTags
                    .Select(t => new EmployerTagDTO
                    {
                        Id = t.id,
                        TagType = t.type_tag,
                        TagName = t.TagsList.tags_name,
                    }).ToList()
                })
                .OrderByDescending(e => e.CommentCount)
                .ToListAsync();


            var sortedEmployers = employersWithTopRatingTag
            .Concat(otherEmployers)
            .Take(6)
            .ToList();

            return Ok(sortedEmployers);
        }


        private static int? CalculateAge(DateTime? dateOfBirth)
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
    }
}
