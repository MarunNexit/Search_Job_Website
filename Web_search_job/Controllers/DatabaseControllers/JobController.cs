using LinqKit;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Web_search_job.Data;
using Web_search_job.DatabaseClasses;
using Web_search_job.DatabaseClasses.EmployerFolder;
using Web_search_job.DatabaseClasses.FiltersFolder;
using Web_search_job.DatabaseClasses.JobFolder;
using Web_search_job.DTO.Employer;
using Web_search_job.DTO.Job;
using Web_search_job.DTO.Resume;
using Web_search_job.DTO.User;

namespace Web_search_job.Controllers.DatabaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("JobOrigins")]
    public class JobController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly UserController _userController;

        public JobController(DataContext context, UserController userController)
        {
            _context = context;
            _userController = userController;
        }

        [HttpGet]
        public async Task<ActionResult<List<Job>>> GetJobAllData(int currentuser)
        {
            return Ok(await _context.Jobs.ToListAsync());
        }

        //POST


        // Add new SubscribeToEmployer
        [HttpPost("subscribe-to-employer")]
        public async Task<ActionResult<SubscribeToEmployer>> AddSubscribeToEmployer([FromBody] SubscribeToEmployer subscribeToEmployer)
        {
            if (subscribeToEmployer == null)
            {
                return BadRequest("Invalid data.");
            }

            _context.SubscribeToEmployer.Add(subscribeToEmployer);
            await _context.SaveChangesAsync();

            return Ok(subscribeToEmployer);
        }



        //GET
        /*[HttpGet("count_jobs")]
        public async Task<ActionResult<List<object>>> GetCountJobs()
        {
            var result = await _context.Jobs.CountAsync();

            return Ok(result);
        }

        [HttpGet("count_rec_jobs")]
        public async Task<ActionResult<List<object>>> GetCountRecJobs()
        {
            var result = await _context.Employers.CountAsync();

            return Ok(result);
        }*/

        private async Task IncrementJobViewCountAsync(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var job = await _context.Jobs.FindAsync(id);
                    if (job != null)
                    {
                        job.number_view++;
                        _context.Jobs.Update(job);
                        await _context.SaveChangesAsync();
                    }

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }


        private async Task IncrementJobRequestCountAsync(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var job = await _context.Jobs.FindAsync(id);
                    if (job != null)
                    {
                        job.number_candidates++;
                        _context.Jobs.Update(job);
                        await _context.SaveChangesAsync();
                    }

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }


        //GET
        [HttpGet("{id}")]
        public async Task<ActionResult<JobDTO>> GetJobById(int id, string? userId)
        {

            Location userLocation = new Location();
            int? intId = 0;

            if (userId != null && userId != "" && userId != "NULL" && userId != " " && userId != "null")
            {
                Console.WriteLine(userId);

                var user = await _userController.GetUserById(userId);

                if (user != null)
                {
                    intId = user.UserInfo.Id;

                    Console.WriteLine(intId);

                    userLocation = user.UserInfo.Location;
                }
            }

            List<SavedJob> savedJobs = new List<SavedJob>();

            savedJobs = await _context.SavedJobs
                    .Where(sj => sj.user_id == intId)
                    .ToListAsync();

            var savedJobIds = savedJobs.Select(sj => sj.job_id).ToList();


            /* CHANGE AND ADD

             var user_ = await _context.UsersInfo
              .Include(ui => ui.ApplicationUser) // Включаємо дані користувача з таблиці ApplicationUser
              .Include(ui => ui.Location) // Включаємо дані про місцезнаходження
              .FirstOrDefaultAsync(ui => ui.UsersOfEmployer.employer_id == id);

             if (user_ != null)
             {
                 var userDTO = new UserDTO
                 {
                     Id = user_.Id,
                     UserId = user_.user_Id,
                     UserName = user_.ApplicationUser != null ? user_.ApplicationUser.UserName : null,
                     FirstName = user_.FirstName,
                     LastName = user_.LastName,
                     PhoneNumber = user_.PhoneNumber,
                     UserCreatedAt = user_.ActionCreatedAt,
                     UserImg = user_.UserImg,
                     Location = user_.Location != null ? new Location
                     {
                         id = user_.Location.id,
                         location_city = user_.Location.location_city,
                         location_region = user_.Location.location_region,
                         location_country = user_.Location.location_country,
                     } : null,
                 };

                 // Тепер ви можете використовувати змінну userDTO з даними користувача
             }
             else
             {
                 // Обробка випадку, коли користувач не знайдений
             }*/

            await IncrementJobViewCountAsync(id);


            var job = await _context.Jobs
                /*.Include(e => e.Industry)
                .Include(e => e.Location)
                .Include(e => e.UserInfo)
                .Include(e => e.Employer)
                    .ThenInclude(tl => tl.Jobs)
                .Include(e => e.Employer)
                    .ThenInclude(tl => tl.CommentToEmployer)
                .Include(e => e.JobRequirements)
                .Include(e => e.JobRequestFields)
                .Include(e => e.SavedJobs)
                .Include(e => e.JobTagsMarks)
                .Include(e => e.JobTagsPros)
                    .ThenInclude(tl => tl.JobTagsProsList)
                .Include(e => e.UserInfo)
                    .ThenInclude(au => au.ApplicationUser)
                .ThenInclude(c => c.UserInfo)
                    .ThenInclude(ui => ui.Location)*/
                .Where(e => e.id == id)
                .Select(e => new JobDTO
                {
                    Id = e.id,
                    JobTitle = e.job_title,
                    JobImg = e.job_img,
                    JobBackgroundImg = e.job_background_img,
                    JobSalaryMin = e.job_salary_min,
                    JobSalaryMax = e.job_salary_max,
                    JobSalaryCurrency = e.job_salary_currency,
                    JobDescription = e.job_description,
                    NumberCandidates = e.number_candidates,
                    NumberView = e.number_view,
                    DateEnding = e.date_ending,
                    DateLastEditing = e.date_last_editing,
                    DateApproving = e.date_approving,
                    Status = e.status,
                    CreatedAt = e.created_at,
                    isSavedJob = savedJobIds.Contains((int)e.id),
                    isExistJobRequest = intId.HasValue && _context.JobRequests.Any(jr => jr.UserId == intId && jr.JobId == e.id && jr.Status != null),  // Додано нове поле
                    Location = e.Location,
                    Industry = e.Industry,
                    JobRequirement = e.JobRequirements != null ? new JobRequirementDTO
                    {
                        id = e.JobRequirements.id,
                        experience = e.JobRequirements.experience,
                        language_name = e.JobRequirements.language_name,
                        language_level = e.JobRequirements.language_level,

                    } : null,
                    JobRequestFields = e.JobRequestFields != null ? new JobRequestFieldsDTO
                    {
                        Id = e.JobRequestFields.Id,
                        NeedAdditionalResume = e.JobRequestFields.NeedAdditionalResume,
                        NeedResume = e.JobRequestFields.NeedResume,
                        PositiveField = e.JobRequestFields.PositiveField,
                        ProjectField = e.JobRequestFields.ProjectField,

                    } : null,
                    JobTagsMarks = e.JobTagsMarks != null ? new JobTagsMarksDTO
                    {
                        Id = e.JobTagsMarks.id,
                        TagHot = e.JobTagsMarks.tag_hot,
                        TagNew = e.JobTagsMarks.tag_new,
                        TagRecommend = _context.JobRecommendationList.Any(r => r.JobId == e.id && r.UserId == intId),
                    } : null,
                    JobTagsPros = e.JobTagsPros != null ? e.JobTagsPros.Select(t => new JobTagsProsDTO
                    {
                        Id = t.id,
                        JobTagsProsName = t.TagsList.tags_name,
                    }).ToList() : null,
                    Employer = e.Employer != null ? new EmployerDTO
                    {
                        Id = e.Employer.id,
                        CompanyName = e.Employer.company_name,
                        CompanyIndustryDescription = e.Employer.company_industry_description,
                        CompanyShortDescription = e.Employer.company_short_description,
                        CompanyChecked = e.Employer.company_checked,
                        CompanyURL = e.Employer.company_url,
                        CompanyYear = e.Employer.company_year_experience,
                        CompanyRating = e.Employer.CommentToEmployer.Any() ? e.Employer.CommentToEmployer.Average(c => c.comment_stars) : 0,
                        CompanyImg = e.Employer.company_img,
                        CompanyBackgroundImg = e.Employer.company_background_img,
                        EmployerCreatedAt = e.Employer.employer_created_at,
                        JobsCount = e.Employer.Jobs.Any() ? e.Employer.Jobs.Count() : 0,
                        CommentCount = e.Employer.CommentToEmployer.Count(),
                        NumberWorkers = e.Employer.number_workers,
                        CompanyDescription = e.Employer.company_description,
                        CompanyStatus = e.Employer.company_status,
                    } : null,
                    /* CHANGE
                    User = e.UserInfo != null ? new UserDTO
                    {
                        Id = e.UserInfo.Id,
                        UserId = e.UserInfo.user_Id,
                        UserName = e.UserInfo.ApplicationUser != null ? e.UserInfo.ApplicationUser.UserName : null,
                        FirstName = e.UserInfo.FirstName,
                        LastName = e.UserInfo.LastName,
                        PhoneNumber = e.UserInfo.PhoneNumber,
                        UserCreatedAt = e.UserInfo.ActionCreatedAt,
                        UserImg = e.UserInfo.UserImg,
                        Location = e.UserInfo.Location != null ? e.UserInfo.Location : null,
                    } : null,*/
                })
                .FirstOrDefaultAsync();

            if (job == null)
            {
                return NotFound();
            }

            return Ok(job);
        }


        [HttpGet("recommend_jobs")]
        public async Task<ActionResult<List<JobShortDTO>>> GetRecommendDataJob(
            string userId,
            string showLess,
            int page = 1,
            int pageSize = 18
            )
        {
            Console.WriteLine(userId);
            Console.WriteLine(page);
            Console.WriteLine(pageSize);
            Console.WriteLine(showLess);


            Location userLocation = new Location();
            int? intId = 0;

            if (userId != null && userId != "" && userId != "NULL")
            {
                var user = await _userController.GetUserById(userId);

                if (user == null)
                {
                    return NotFound($"Помилка користувача {userId}");
                }

                if (user.UserInfo != null && user.UserInfo.Id != null )
                {
                    intId = user.UserInfo.Id;

                    if (user.UserInfo.Location != null) {
                        userLocation = user.UserInfo.Location;
                    }
                }

            }

            List<SavedJob> savedJobs = new List<SavedJob>();

            savedJobs = await _context.SavedJobs
                    .Where(sj => sj.user_id == intId)
                    .ToListAsync();

            var savedJobIds = savedJobs.Select(sj => sj.job_id).ToList();

            var startIndex = (page - 1) * pageSize;


            var query = _context.Jobs
                .Include(e => e.Industry)
                .Include(e => e.Location)
                .Include(e => e.UserInfo)
                .Include(e => e.Employer)
                    .ThenInclude(tl => tl.Jobs)
                .Include(e => e.Employer)
                    .ThenInclude(tl => tl.CommentToEmployer)
                .Include(e => e.JobRequirements)
                .Include(e => e.JobRequestFields)
                .Include(e => e.JobTagsMarks)
                .Include(e => e.JobTagsPros)
                    .ThenInclude(tl => tl.TagsList)
                .Include(e => e.UserInfo)
                    .ThenInclude(au => au.ApplicationUser)
                    .ThenInclude(c => c.UserInfo)
                        .ThenInclude(ui => ui.Location)
                .AsQueryable();


            query = query.Where(e => e.JobRecommendationList != null && e.JobRecommendationList.Any(r => r.JobId == e.id && r.UserId == intId));

            var jobs = await query
                .Select(e => new JobShortDTO
                {
                    Id = e.id,
                    JobTitle = e.job_title,
                    JobImg = e.job_img,
                    JobBackgroundImg = e.job_background_img,
                    JobSalaryMin = e.job_salary_min,
                    JobSalaryMax = e.job_salary_max,
                    JobSalaryCurrency = e.job_salary_currency,
                    JobDescription = e.job_description,

                    NumberCandidates = e.number_candidates,
                    NumberView = e.number_view,

                    DateEnding = e.date_ending,
                    DateLastEditing = e.date_last_editing,
                    DateApproving = e.date_approving,
                    Status = e.status,
                    CreatedAt = e.created_at,
                    isExistJobRequest = intId.HasValue && _context.JobRequests.Any(jr => jr.UserId == intId && jr.JobId == e.id && jr.Status != null),  // Додано нове поле
                    isSavedJob = savedJobIds.Contains((int)e.id),
                    Location = e.Location,
                    Industry = e.Industry,
                    JobTagsMarks = e.JobTagsMarks != null ? new JobTagsMarksDTO
                    {
                        Id = e.JobTagsMarks.id,
                        TagHot = e.JobTagsMarks.tag_hot,
                        TagNew = e.JobTagsMarks.tag_new,
                        TagRecommend = true,
                    } : null,
                    JobTagsPros = e.JobTagsPros.Select(t => new JobTagsProsDTO
                    {
                        Id = t.id,
                        JobTagsProsName = t.TagsList.tags_name,
                    }).ToList(),
                    Employer = e.Employer != null ? new EmployerShortDTO
                    {
                        Id = e.id,
                        CompanyName = e.Employer.company_name,
                        CompanyIndustryDescription = e.Employer.company_industry_description,
                        CompanyShortDescription = e.Employer.company_short_description,
                        CompanyChecked = e.Employer.company_checked,
                        CompanyURL = e.Employer.company_url,
                        CompanyYear = e.Employer.company_year_experience,
                        CompanyRating = e.Employer.CommentToEmployer.Any() ? e.Employer.CommentToEmployer.Average(c => c.comment_stars) : 0,
                        CompanyImg = e.Employer.company_img,
                        EmployerCreatedAt = e.Employer.employer_created_at,
                        CommentCount = e.Employer.CommentToEmployer.Count(),
                    } : null,
                })
                .ToListAsync();


            // Apply showLess
            if (!string.IsNullOrEmpty(showLess) && showLess != "NULL" && showLess != "")
            {
                var showLessItems = showLess.Split(",").Select(item => item.Split(":").Last());
                jobs = jobs.OrderByDescending(e => showLessItems.Contains(e.Employer.CompanyName + ":" + e.JobTitle)).ToList();
            }


            jobs = jobs
                .OrderByDescending(e => e.DateApproving ?? DateTime.MinValue)
                .ThenByDescending(e => e.JobTagsMarks != null && e.JobTagsMarks.TagHot)
                .ThenByDescending(e => e.JobTagsMarks != null && e.JobTagsMarks.TagRecommend)
                .ThenBy(e => e.Location.location_country == userLocation.location_country &&
                                e.Location.location_region == userLocation.location_region &&
                                e.Location.location_city == userLocation.location_city ? 0 : 1)
                .Skip(startIndex)
                .Take(pageSize)
                .ToList();


            return Ok(jobs);
        }



        public class UpdateSavedJobRequest
        {
            public string UserId { get; set; }
            public int JobId { get; set; }
            public bool IsSavedJob { get; set; }
        }


        [HttpPost("update_saved_job")]
        public async Task<IActionResult> UpdateSavedJob([FromBody] UpdateSavedJobRequest request)
        {
            if (string.IsNullOrEmpty(request.UserId) || request.UserId == "NULL" || request.UserId == "null")
            {
                return BadRequest("Invalid userId");
            }

            if (request.JobId == 0)
            {
                return BadRequest("Invalid request");
            }

            var user = await _userController.GetUserById(request.UserId);
            if (user == null)
            {
                return NotFound($"User with id {request.UserId} not found");
            }

            var userInfoId = user.UserInfo.Id;

            // Перевіряємо, чи робота вже збережена
            var savedJob = await _context.SavedJobs
                .FirstOrDefaultAsync(sj => sj.user_id == userInfoId && sj.job_id == request.JobId);

            if (request.IsSavedJob)
            {
                // Якщо потрібно зберегти роботу, але вона ще не збережена
                if (savedJob == null)
                {
                    _context.SavedJobs.Add(new SavedJob { user_id = userInfoId, job_id = request.JobId });
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                // Якщо потрібно видалити збережену роботу
                if (savedJob != null)
                {
                    _context.SavedJobs.Remove(savedJob);
                    await _context.SaveChangesAsync();
                }
            }

            return Ok();
        }

        [HttpGet("saved_jobs")]
        public async Task<ActionResult<List<JobShortDTO>>> GetSavedDataJob(
            string userId,
            int page = 1,
            int pageSize = 18
            )
        {
            Console.WriteLine(userId);
            Console.WriteLine(page);
            Console.WriteLine(pageSize);


            Location userLocation = new Location();
            int? intId = 0;

            if (userId != null && userId != "" && userId != "NULL")
            {
                var user = await _userController.GetUserById(userId);

                if (user == null)
                {
                    return NotFound($"Помилка користувача {userId}");
                }

                intId = user.UserInfo.Id;

                userLocation = user.UserInfo.Location;
            }

            /*List<SavedJob> savedJobs = new List<SavedJob>();

            savedJobs = await _context.SavedJobs
                    .Where(sj => sj.user_id == intId)
                    .ToListAsync();

            var savedJobIds = savedJobs.Select(sj => sj.job_id).ToList();*/

            var startIndex = (page - 1) * pageSize;


            var query = _context.Jobs
                .Include(e => e.Industry)
                .Include(e => e.Location)
                .Include(e => e.UserInfo)
                .Include(e => e.Employer)
                    .ThenInclude(tl => tl.Jobs)
                .Include(e => e.Employer)
                    .ThenInclude(tl => tl.CommentToEmployer)
                .Include(e => e.JobRequirements)
                .Include(e => e.JobRequestFields)
                .Include(e => e.JobTagsMarks)
                .Include(e => e.JobRecommendationList)
                .Include(e => e.JobTagsPros)
                    .ThenInclude(tl => tl.TagsList)
                .Include(e => e.UserInfo)
                    .ThenInclude(au => au.ApplicationUser)
                    .ThenInclude(c => c.UserInfo)
                        .ThenInclude(ui => ui.Location)
                .AsQueryable();


            query = query.Where(e => e.SavedJobs != null && e.SavedJobs.Any(r => r.job_id == e.id && r.user_id == intId));


            var jobs = await query
                .Select(e => new JobShortDTO
                {
                    Id = e.id,
                    JobTitle = e.job_title,
                    JobImg = e.job_img,
                    JobBackgroundImg = e.job_background_img,
                    JobSalaryMin = e.job_salary_min,
                    JobSalaryMax = e.job_salary_max,
                    JobSalaryCurrency = e.job_salary_currency,
                    JobDescription = e.job_description,

                    NumberCandidates = e.number_candidates,
                    NumberView = e.number_view,

                    DateEnding = e.date_ending,
                    DateLastEditing = e.date_last_editing,
                    DateApproving = e.date_approving,
                    Status = e.status,
                    CreatedAt = e.created_at,
                    isExistJobRequest = intId.HasValue && _context.JobRequests.Any(jr => jr.UserId == intId && jr.JobId == e.id && jr.Status != null),  // Додано нове поле
                    isSavedJob = true,
                    Location = e.Location,
                    Industry = e.Industry,
                    JobTagsMarks = e.JobTagsMarks != null ? new JobTagsMarksDTO
                    {
                        Id = e.JobTagsMarks.id,
                        TagHot = e.JobTagsMarks.tag_hot,
                        TagNew = e.JobTagsMarks.tag_new,
                        TagRecommend = e.JobRecommendationList != null && e.JobRecommendationList.Any(r => r.JobId == e.id && r.UserId == intId),
                    } : null,
                    JobTagsPros = e.JobTagsPros.Select(t => new JobTagsProsDTO
                    {
                        Id = t.id,
                        JobTagsProsName = t.TagsList.tags_name,
                    }).ToList(),
                    Employer = e.Employer != null ? new EmployerShortDTO
                    {
                        Id = e.id,
                        CompanyName = e.Employer.company_name,
                        CompanyIndustryDescription = e.Employer.company_industry_description,
                        CompanyShortDescription = e.Employer.company_short_description,
                        CompanyChecked = e.Employer.company_checked,
                        CompanyURL = e.Employer.company_url,
                        CompanyYear = e.Employer.company_year_experience,
                        CompanyRating = e.Employer.CommentToEmployer.Any() ? e.Employer.CommentToEmployer.Average(c => c.comment_stars) : 0,
                        CompanyImg = e.Employer.company_img,
                        EmployerCreatedAt = e.Employer.employer_created_at,
                        CommentCount = e.Employer.CommentToEmployer.Count(),
                    } : null,
                })
                    .ToListAsync();


            jobs = jobs
                .OrderByDescending(e => e.DateApproving ?? DateTime.MinValue)
                .ThenByDescending(e => e.JobTagsMarks != null && e.JobTagsMarks.TagHot)
                .ThenByDescending(e => e.JobTagsMarks != null && e.JobTagsMarks.TagRecommend)
                .ThenBy(e => e.Location.location_country == userLocation.location_country &&
                                e.Location.location_region == userLocation.location_region &&
                                e.Location.location_city == userLocation.location_city ? 0 : 1)
                .Skip(startIndex)
                .Take(pageSize)
                .ToList();


            return Ok(jobs);
        }


        static string ExtractNumber(string input)
        {
            // Regular expression to match digits and thousands separators
            var match = Regex.Match(input, @"\d[\d\s]*\d");
            return match.Value.Replace(" ", string.Empty);
        }


        [HttpGet("search_cards")]
        public async Task<ActionResult<JobSearchResultDTO>> GetShortDataJob(
            string userId,
            string showLess,
            string searchBarParam,
            string sortingParam,
            string filtersParam,
            int page = 1,
            int pageSize = 18
            )
        {
            Location userLocation = new Location();
            int? intId = 0;

            if (userId != null && userId != "" && userId != "NULL")
            {
                var user = await _userController.GetUserById(userId);

                if (user == null)
                {
                    return NotFound($"Помилка користувача {userId}");
                }

                intId = user.UserInfo.Id;

                userLocation = user.UserInfo.Location;
            }

            List<SavedJob> savedJobs = new List<SavedJob>();

            savedJobs = await _context.SavedJobs
                    .Where(sj => sj.user_id == intId)
                    .ToListAsync();

            var savedJobIds = savedJobs.Select(sj => sj.job_id).ToList();


            var startIndex = (page - 1) * pageSize;


            var query = _context.Jobs
                .Include(e => e.Industry)
                .Include(e => e.Location)
                .Include(e => e.UserInfo)
                .Include(e => e.Employer)
                    .ThenInclude(tl => tl.Jobs)
                .Include(e => e.Employer)
                    .ThenInclude(tl => tl.CommentToEmployer)
                .Include(e => e.JobRequirements)
                .Include(e => e.JobRequestFields)
                .Include(e => e.SavedJobs)
                .Include(e => e.JobTagsMarks)
                .Include(e => e.JobTagsPros)
                    .ThenInclude(tl => tl.TagsList)
                .Include(e => e.UserInfo)
                    .ThenInclude(au => au.ApplicationUser)
                    .ThenInclude(c => c.UserInfo)
                        .ThenInclude(ui => ui.Location)
                .AsQueryable();


            // Exclude jobs with date_ending passed
            query = query.Where(e => e.date_ending == null || e.date_ending > DateTime.Now);

            // Apply search bar filter
            if (!string.IsNullOrEmpty(searchBarParam) && searchBarParam != "NULL" && searchBarParam != "")
            {
                query = query.Where(e => e.job_title.Contains(searchBarParam) ||
                                         e.job_description.Contains(searchBarParam) ||
                                         e.Industry.industry_name.Contains(searchBarParam) ||
                                         e.Employer.company_name.Contains(searchBarParam) ||
                                         e.Location.location_city.Contains(searchBarParam) ||
                                         e.Location.location_region.Contains(searchBarParam) ||
                                         e.Location.location_country.Contains(searchBarParam));
            }

            var activeFilters = new List<Expression<Func<Job, bool>>>();


            var locationFilters = new List<Expression<Func<Job, bool>>>();
            var industryFilters = new List<Expression<Func<Job, bool>>>();
            var characterFilters = new List<Expression<Func<Job, bool>>>();
            var salaryFilters = new List<Expression<Func<Job, bool>>>();
            var experienceFilters = new List<Expression<Func<Job, bool>>>();
            var typeWorkFilters = new List<Expression<Func<Job, bool>>>();
            var searchRequestFilters = new List<Expression<Func<Job, bool>>>();


            // Apply filters
            if (filtersParam != null && filtersParam != "" && filtersParam != "NULL" && filtersParam.Any())
            {
                foreach (var filter in filtersParam.Split(","))
                {
                    var parts = filter.Split(':');
                    if (parts.Length == 2)
                    {
                        var filterType = parts[0];
                        var filterValue = parts[1];

                        switch (filterType)
                        {
                            case "location":
                                /*query = query.Where*/
                                locationFilters.Add(e => e.Location != null &&
                                                       (e.Location.location_city.Contains(filterValue) ||
                                                          e.Location.location_region.Contains(filterValue) ||
                                                          e.Location.location_country.Contains(filterValue)));
                                break;
                            case "industry":
                                industryFilters.Add(e => e.Industry != null && e.Industry.industry_name.Contains(filterValue));
                                break;
                            // Add more filter types as needed
                            case "character":
                                characterFilters.Add(e => e.JobTagsPros != null && e.JobTagsPros.Any(t => t.TagsList.tags_name.Contains(filterValue)));
                                break;
                            // Add more filter types as needed
                            case "salary":
                                string input = filterValue;
                                string numberString = ExtractNumber(input);

                                if (decimal.TryParse(numberString, NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out decimal number))
                                {
                                    decimal usdToUahRate = 37;

                                    salaryFilters.Add(e =>
                                        (e.job_salary_currency == "USD" ? (e.job_salary_min.HasValue && e.job_salary_min >= number * usdToUahRate) ||
                                                                         (e.job_salary_max.HasValue && e.job_salary_max >= number * usdToUahRate)
                                                                       : (e.job_salary_min.HasValue && e.job_salary_min >= number) ||
                                                                         (e.job_salary_max.HasValue && e.job_salary_max >= number)
                                        )
                                    );
                                }
                                else
                                {
                                    Console.WriteLine("Could not parse the number.");
                                }
                                break;
                            // Add more filter types as needed
                            case "experience":
                                experienceFilters.Add(e => e.JobTagsPros != null && e.JobTagsPros.Any(t => t.TagsList.tags_name.Contains(filterValue)));
                                break;
                            // Add more filter types as needed
                            case "type-work":
                                typeWorkFilters.Add(e => e.JobTagsPros != null && e.JobTagsPros.Any(t => t.TagsList.tags_name.Contains(filterValue)));
                                break;
                            // Add more filter types as needed
                            case "search-request":
                                switch (filterValue)
                                {
                                    case "Рекомендовані вакансії":
                                        searchRequestFilters.Add(e => e.JobRecommendationList != null && e.JobRecommendationList.Any(r => r.JobId == e.id && r.UserId == intId));
                                        //query = query.Where(e => e.JobTagsMarks != null && e.JobTagsMarks.tag_recommend);
                                        break;
                                    case "Лише вакансії в ЗСУ":
                                        searchRequestFilters.Add(e => e.JobTagsPros != null && e.JobTagsPros.Any(t => t.TagsList.tags_name.Contains("ЗСУ")));
                                        break;
                                    case "Приховати Гарячі вакансії":
                                        searchRequestFilters.Add(e => e.JobTagsMarks == null || !e.JobTagsMarks.tag_hot);
                                        break;
                                        // Add more filter options as needed
                                }
                                break;
                        }
                    }
                }
            }

            var combinedFilter = PredicateBuilder.New<Job>(true); // Початковий фільтр, який завжди true

            var filterGroups = new List<IEnumerable<Expression<Func<Job, bool>>>>
            {
                locationFilters,
                industryFilters,
                characterFilters,
                salaryFilters,
                experienceFilters,
                typeWorkFilters,
                searchRequestFilters
            };

            foreach (var filterGroup in filterGroups)
            {
                if (filterGroup.Any())
                {
                    var groupFilter = PredicateBuilder.New<Job>(false); // Початковий фільтр групи, який завжди false

                    foreach (var filter in filterGroup)
                    {
                        groupFilter = groupFilter.Or(filter); // Додавання кожного фільтру до групового виразу через OR
                    }

                    combinedFilter = combinedFilter.And(groupFilter); // Додавання групового виразу до комбінованого через AND
                }
            }

            // Застосування комбінованого фільтра до запиту
            query = query.Where(combinedFilter);
            // Застосування комбінованого фільтра до запиту
            query = query.Where(combinedFilter);

            var jobs = await query
                .Select(e => new JobShortDTO
                {
                    Id = e.id,
                    JobTitle = e.job_title,
                    JobImg = e.job_img,
                    JobBackgroundImg = e.job_background_img,
                    JobSalaryMin = e.job_salary_min,
                    JobSalaryMax = e.job_salary_max,
                    JobSalaryCurrency = e.job_salary_currency,
                    JobDescription = e.job_description,

                    NumberCandidates = e.number_candidates,
                    NumberView = e.number_view,

                    DateEnding = e.date_ending,
                    DateLastEditing = e.date_last_editing,
                    DateApproving = e.date_approving,
                    Status = e.status,
                    CreatedAt = e.created_at,
                    isExistJobRequest = intId.HasValue && _context.JobRequests.Any(jr => jr.UserId == intId && jr.JobId == e.id && jr.Status != null),  // Додано нове поле
                    isSavedJob = savedJobIds.Contains((int)e.id),
                    Location = e.Location,
                    Industry = e.Industry,
                    JobTagsMarks = e.JobTagsMarks != null ? new JobTagsMarksDTO
                    {
                        Id = e.JobTagsMarks.id,
                        TagHot = e.JobTagsMarks.tag_hot,
                        TagNew = e.JobTagsMarks.tag_new,
                        TagRecommend = e.JobRecommendationList != null && e.JobRecommendationList.Any(r => r.JobId == e.id && r.UserId == intId),
                    } : null,
                    JobTagsPros = e.JobTagsPros.Select(t => new JobTagsProsDTO
                    {
                        Id = t.id,
                        JobTagsProsName = t.TagsList.tags_name,
                    }).ToList(),
                    Employer = e.Employer != null ? new EmployerShortDTO
                    {
                        Id = e.id,
                        CompanyName = e.Employer.company_name,
                        CompanyIndustryDescription = e.Employer.company_industry_description,
                        CompanyShortDescription = e.Employer.company_short_description,
                        CompanyChecked = e.Employer.company_checked,
                        CompanyURL = e.Employer.company_url,
                        CompanyYear = e.Employer.company_year_experience,
                        CompanyRating = e.Employer.CommentToEmployer.Any() ? e.Employer.CommentToEmployer.Average(c => c.comment_stars) : 0,
                        CompanyImg = e.Employer.company_img,
                        EmployerCreatedAt = e.Employer.employer_created_at,
                        CommentCount = e.Employer.CommentToEmployer.Count(),
                    } : null,
                })
                .ToListAsync();


            // Apply showLess
            if (!string.IsNullOrEmpty(showLess) && showLess != "NULL" && showLess != "")
            {
                var showLessItems = showLess.Split(",").Select(item => item.Split(":").Last());
                jobs = jobs.OrderByDescending(e => showLessItems.Contains(e.Employer.CompanyName + ":" + e.JobTitle)).ToList();
            }

            var totalElements = await query.CountAsync();

            if (sortingParam != null && sortingParam != "NULL" && sortingParam != "" && sortingParam != "new")
            {
                if (sortingParam == "recommend")
                {
                    jobs = jobs
                        .OrderByDescending(e => e.JobTagsMarks != null ? e.JobTagsMarks.TagRecommend : false)
                        .ThenBy(e => e.Location.location_region != null && e.Location.location_country != null && e.Location.location_city != null && userLocation != null
                        && e.Location.location_country == userLocation.location_country && e.Location.location_region == userLocation.location_region && e.Location.location_city == userLocation.location_city ? 0 : 1)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList();
                }
                else if (sortingParam == "popular")
                {
                    jobs = jobs
                        .OrderByDescending(e => e.NumberView != null && e.NumberCandidates != null ? e.NumberView + e.NumberCandidates : 0)
                        .ThenByDescending(e => e.JobTagsMarks != null ? e.JobTagsMarks.TagHot : false)
                        .ThenByDescending(e => e.JobTagsMarks != null ? e.JobTagsMarks.TagRecommend : false)
                        .ThenBy(e => e.Location.location_region != null && e.Location.location_country != null && e.Location.location_city != null && userLocation != null
                        && e.Location.location_country == userLocation.location_country && e.Location.location_region == userLocation.location_region && e.Location.location_city == userLocation.location_city ? 0 : 1)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList();
                }
            }
            else
            {
                if (userLocation != null && userLocation.location_country != null && userLocation.location_region != null && userLocation.location_city != null)
                {
                    jobs = jobs
                     .OrderByDescending(e => e.JobTagsMarks != null && e.JobTagsMarks.TagHot)
                     .ThenByDescending(e => e.DateApproving ?? DateTime.MinValue)
                     .ThenByDescending(e => e.JobTagsMarks != null && e.JobTagsMarks.TagRecommend)
                     .ThenBy(e => e.Location.location_country == userLocation.location_country &&
                                  e.Location.location_region == userLocation.location_region &&
                                  e.Location.location_city == userLocation.location_city ? 0 : 1)
                     .Skip(startIndex)
                     .Take(pageSize)
                     .ToList();
                }
                else
                {
                    jobs = jobs
                       .OrderByDescending(e => e.JobTagsMarks != null && e.JobTagsMarks.TagHot)
                       .ThenByDescending(e => e.DateApproving ?? DateTime.MinValue)
                       .ThenByDescending(e => e.JobTagsMarks != null && e.JobTagsMarks.TagRecommend)
                       .Skip(startIndex)
                       .Take(pageSize)
                       .ToList();

                }

            }

            var result = new JobSearchResultDTO
            {
                Jobs = jobs,
                TotalElements = totalElements
            };



            if (intId != null && intId != 0)
            {
                await CreateAuditRecord((int)intId, "GET_Search");
            }


            return Ok(result);
        }


        private async Task<IActionResult> CreateAuditRecord(int userId, string action)
        {
            if (userId == 0 || userId == null || string.IsNullOrEmpty(action))
            {
                return BadRequest("Invalid audit data.");
            }
            else
            {
                var audit = new Audit
                {
                    user_id = userId,
                    action = action,
                    action_created_at = DateTime.UtcNow
                };

                _context.Audit.Add(audit);
                await _context.SaveChangesAsync();

                return Ok(audit);
            }
        }



        [HttpGet("similar_jobs")]
        public async Task<ActionResult<List<JobShortDTO>>> GetSimilarDataJob(
            string userId,
            string showLess,
            string searchBarParam,
            string sortingParam,
            string filtersParam,
            int id
            )
        {
            Console.WriteLine(userId);
            Console.WriteLine(showLess);
            Console.WriteLine(searchBarParam);
            Console.WriteLine(sortingParam);
            Console.WriteLine(filtersParam);
            Console.WriteLine(id);


            Location userLocation = new Location();
            int? intId = 0;

            if (userId != null && userId != "" && userId != "NULL")
            {
                var user = await _userController.GetUserById(userId);

                if (user == null)
                {
                    return NotFound($"Помилка користувача {userId}");
                }

                intId = user.UserInfo.Id;

                userLocation = user.UserInfo.Location;
            }

            List<SavedJob> savedJobs = new List<SavedJob>();

            savedJobs = await _context.SavedJobs
                    .Where(sj => sj.user_id == intId)
                    .ToListAsync();

            var savedJobIds = savedJobs.Select(sj => sj.job_id).ToList();


            var query = _context.Jobs
                .Include(e => e.Industry)
                .Include(e => e.Location)
                .Include(e => e.UserInfo)
                .Include(e => e.Employer)
                    .ThenInclude(tl => tl.Jobs)
                .Include(e => e.Employer)
                    .ThenInclude(tl => tl.CommentToEmployer)
                .Include(e => e.JobRequirements)
                .Include(e => e.JobRequestFields)
                .Include(e => e.SavedJobs)
                .Include(e => e.JobTagsMarks)
                .Include(e => e.JobTagsPros)
                    .ThenInclude(tl => tl.TagsList)
                .Include(e => e.UserInfo)
                    .ThenInclude(au => au.ApplicationUser)
                    .ThenInclude(c => c.UserInfo)
                        .ThenInclude(ui => ui.Location)
                .Where(e => e.id != id) // Виключити роботу з переданим id
                .AsQueryable();


            // Apply search bar filter
            if (!string.IsNullOrEmpty(searchBarParam) && searchBarParam != "NULL" && searchBarParam != "")
            {
                query = query.Where(e => e.job_title.Contains(searchBarParam) ||
                                         e.job_description.Contains(searchBarParam) ||
                                         e.Industry.industry_name.Contains(searchBarParam) ||
                                         e.Employer.company_name.Contains(searchBarParam) ||
                                         e.Location.location_city.Contains(searchBarParam) ||
                                         e.Location.location_region.Contains(searchBarParam) ||
                                         e.Location.location_country.Contains(searchBarParam));
            }


            // Apply filters
            if (filtersParam != null && filtersParam != "" && filtersParam != "NULL" && filtersParam.Any())
            {
                foreach (var filter in filtersParam.Split(","))
                {
                    var parts = filter.Split(':');
                    if (parts.Length == 2)
                    {
                        var filterType = parts[0];
                        var filterValue = parts[1];

                        switch (filterType)
                        {
                            case "location":
                                query = query.Where(e => e.Location != null &&
                                                       (e.Location.location_city.Contains(filterValue) ||
                                                          e.Location.location_region.Contains(filterValue) ||
                                                          e.Location.location_country.Contains(filterValue)));
                                break;
                            case "industry":
                                query = query.Where(e => e.Industry != null && e.Industry.industry_name.Contains(filterValue));
                                break;
                            // Add more filter types as needed
                            case "character":
                                query = query.Where(e => e.JobTagsPros != null && e.JobTagsPros.Any(t => t.TagsList.tags_name.Contains(filterValue)));
                                break;
                            // Add more filter types as needed
                            case "salary":
                                string input = filterValue;
                                string numberString = ExtractNumber(input);

                                if (decimal.TryParse(numberString, NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out decimal number))
                                {
                                    query = query.Where(e => e.job_salary_min.HasValue && e.job_salary_min >= number ||
                                                              e.job_salary_max.HasValue && e.job_salary_max >= number);
                                }
                                else
                                {
                                    Console.WriteLine("Could not parse the number.");
                                }
                                break;
                            // Add more filter types as needed
                            case "experience":
                                query = query.Where(e => e.JobTagsPros != null && e.JobTagsPros.Any(t => t.TagsList.tags_name.Contains(filterValue)));
                                break;
                            // Add more filter types as needed
                            case "type-work":
                                query = query.Where(e => e.JobTagsPros != null && e.JobTagsPros.Any(t => t.TagsList.tags_name.Contains(filterValue)));
                                break;
                            // Add more filter types as needed
                            case "search-request":
                                switch (filterValue)
                                {
                                    case "Рекомендовані вакансії":
                                        query = query.Where(e => e.JobRecommendationList != null && e.JobRecommendationList.Any(r => r.JobId == e.id && r.UserId == intId));
                                        //query = query.Where(e => e.JobTagsMarks != null && e.JobTagsMarks.tag_recommend);
                                        break;
                                    case "Лише вакансії в ЗСУ":
                                        query = query.Where(e => e.JobTagsPros != null && e.JobTagsPros.Any(t => t.TagsList.tags_name.Contains("ЗСУ")));
                                        break;
                                    case "Приховати Гарячі вакансії":
                                        query = query.Where(e => e.JobTagsMarks == null || !e.JobTagsMarks.tag_hot);
                                        break;
                                        // Add more filter options as needed
                                }
                                break;
                        }
                    }
                }
            }


            var jobs = await query
                .Select(e => new JobShortDTO
                {
                    Id = e.id,
                    JobTitle = e.job_title,
                    JobImg = e.job_img,
                    JobBackgroundImg = e.job_background_img,
                    JobSalaryMin = e.job_salary_min,
                    JobSalaryMax = e.job_salary_max,
                    JobSalaryCurrency = e.job_salary_currency,
                    JobDescription = e.job_description,

                    NumberCandidates = e.number_candidates,
                    NumberView = e.number_view,

                    DateEnding = e.date_ending,
                    DateLastEditing = e.date_last_editing,
                    DateApproving = e.date_approving,
                    Status = e.status,
                    CreatedAt = e.created_at,
                    isExistJobRequest = intId.HasValue && _context.JobRequests.Any(jr => jr.UserId == intId && jr.JobId == e.id && jr.Status != null),  // Додано нове поле
                    isSavedJob = savedJobIds.Contains((int)e.id),
                    Location = e.Location,
                    Industry = e.Industry,
                    JobTagsMarks = e.JobTagsMarks != null ? new JobTagsMarksDTO
                    {
                        Id = e.JobTagsMarks.id,
                        TagHot = e.JobTagsMarks.tag_hot,
                        TagNew = e.JobTagsMarks.tag_new,
                        TagRecommend = e.JobRecommendationList != null && e.JobRecommendationList.Any(r => r.JobId == e.id && r.UserId == intId),
                    } : null,
                    JobTagsPros = e.JobTagsPros.Select(t => new JobTagsProsDTO
                    {
                        Id = t.id,
                        JobTagsProsName = t.TagsList.tags_name,
                    }).ToList(),
                    Employer = e.Employer != null ? new EmployerShortDTO
                    {
                        Id = e.id,
                        CompanyName = e.Employer.company_name,
                        CompanyIndustryDescription = e.Employer.company_industry_description,
                        CompanyShortDescription = e.Employer.company_short_description,
                        CompanyChecked = e.Employer.company_checked,
                        CompanyURL = e.Employer.company_url,
                        CompanyYear = e.Employer.company_year_experience,
                        CompanyRating = e.Employer.CommentToEmployer.Any() ? e.Employer.CommentToEmployer.Average(c => c.comment_stars) : 0,
                        CompanyImg = e.Employer.company_img,
                        EmployerCreatedAt = e.Employer.employer_created_at,
                        CommentCount = e.Employer.CommentToEmployer.Count(),
                    } : null,
                })
                    .ToListAsync();


            // Apply showLess
            if (!string.IsNullOrEmpty(showLess) && showLess != "NULL" && showLess != "")
            {
                var showLessItems = showLess.Split(",").Select(item => item.Split(":").Last());
                jobs = jobs.OrderByDescending(e => showLessItems.Contains(e.Employer.CompanyName + ":" + e.JobTitle)).ToList();
            }



            if (sortingParam != null && sortingParam != "NULL" && sortingParam != "" && sortingParam != "new")
            {
                if (sortingParam == "recommend")
                {
                    jobs = jobs
                        .OrderByDescending(e => e.JobTagsMarks != null ? e.JobTagsMarks.TagRecommend : false)
                        .ThenBy(e => e.Location.location_region != null && e.Location.location_country != null && e.Location.location_city != null && userLocation != null
                        && e.Location.location_country == userLocation.location_country && e.Location.location_region == userLocation.location_region && e.Location.location_city == userLocation.location_city ? 0 : 1)
                        .Take(4)
                        .ToList();
                }
                else if (sortingParam == "popular")
                {
                    jobs = jobs
                        .OrderByDescending(e => e.NumberView != null && e.NumberCandidates != null ? e.NumberView + e.NumberCandidates : 0)
                        .ThenByDescending(e => e.JobTagsMarks != null ? e.JobTagsMarks.TagHot : false)
                        .ThenByDescending(e => e.JobTagsMarks != null ? e.JobTagsMarks.TagRecommend : false)
                        .ThenBy(e => e.Location.location_region != null && e.Location.location_country != null && e.Location.location_city != null && userLocation != null
                        && e.Location.location_country == userLocation.location_country && e.Location.location_region == userLocation.location_region && e.Location.location_city == userLocation.location_city ? 0 : 1)
                        .Take(4)
                        .ToList();
                }
            }
            else
            {
                jobs = jobs
                    .OrderByDescending(e => e.DateApproving ?? DateTime.MinValue)
                    .ThenByDescending(e => e.JobTagsMarks != null && e.JobTagsMarks.TagHot)
                    .ThenByDescending(e => e.JobTagsMarks != null && e.JobTagsMarks.TagRecommend)
                    .ThenBy(e => e.Location.location_country == userLocation.location_country &&
                                 e.Location.location_region == userLocation.location_region &&
                                 e.Location.location_city == userLocation.location_city ? 0 : 1)
                    .Take(4)
                    .ToList();
            }

            return Ok(jobs);
        }


        private async Task<JobRequest> CreateJobRequestAsync(int jobId, int userId, int? resumeId, string? resume, string? coverLetter, string? positives, string? projects, string? status)
        {
            var existingJobRequest = await _context.JobRequests.FirstOrDefaultAsync(jr => jr.JobId == jobId && jr.UserId == userId && jr.Status != null);

            if (existingJobRequest != null)
            {
                return existingJobRequest;
            }

            var jobRequest = new JobRequest
            {
                JobId = jobId,
                UserId = userId,
                ResumeId = resumeId != null ? resumeId : null,
                ResumeURL = resume != null ? resume : null,
                CoverLetter = coverLetter != null ? coverLetter : null,
                Positives = positives != null ? positives : null,
                Projects = projects != null ? projects : null,
                Status = status != null ? status : "active",
                CreatedAt = DateTime.UtcNow
            };

            _context.JobRequests.Add(jobRequest);
            await _context.SaveChangesAsync();

            return jobRequest;
        }


        [HttpPost("create-job-request")]
        public async Task<IActionResult> CreateJobRequest([FromBody] CreateJobRequestDTO createJobRequestDto)
        {
            var jobRequest = await CreateJobRequestAsync(
                createJobRequestDto.JobId,
                createJobRequestDto.UserId,
                createJobRequestDto.ResumeId,
                createJobRequestDto.ResumeURL,
                createJobRequestDto.CoverLetter,
                createJobRequestDto.Positives,
                createJobRequestDto.Projects,
                createJobRequestDto.Status
            );

            return CreatedAtAction(nameof(GetJobRequest), new { id = jobRequest.Id }, jobRequest);
        }


        [HttpGet("get-job-request/{id}")]
        public async Task<IActionResult> GetJobRequest(int id)
        {
            var jobRequest = await _context.JobRequests.FindAsync(id);
            if (jobRequest == null)
            {
                return NotFound();
            }
            return Ok(jobRequest);
        }


/*        [HttpGet("get-job-request-data/{jobId}/{userId}")]
        public async Task<IActionResult> GetJobRequestData(int jobId, int userId)
        {
            var jobRequest = await _context.JobRequests.FirstOrDefaultAsync(jr => jr.JobId == jobId && jr.UserId == userId);

            if (jobRequest == null)
            {
                return NotFound();
            }
            return Ok(jobRequest);
        }
*/

     /*   [HttpGet("jobs-with-requests")]
        public async Task<ActionResult<List<JobWithRequestDTO>>> GetJobsWithRequests(string userId, int page = 1, int pageSize = 18)
        {
            Console.WriteLine(userId);
            Console.WriteLine(page);
            Console.WriteLine(pageSize);

            Location userLocation = new Location();
            int? intId = 0;

            if (!string.IsNullOrEmpty(userId) && userId != "NULL")
            {
                var user = await _userController.GetUserById(userId);

                if (user == null)
                {
                    return NotFound($"User not found: {userId}");
                }

                intId = user.UserInfo.Id;
                userLocation = user.UserInfo.Location;
            }

            var jobRequests = await _context.JobRequests
                .Where(jr => jr.UserId == intId)
                .Include(jr => jr.Resume) // З'єднуємо з резюме
                .ToListAsync();

            if (!jobRequests.Any())
            {
                return Ok(new List<JobWithRequestDTO>());
            }

            var jobIds = jobRequests.Select(jr => jr.JobId).ToList();
            var startIndex = (page - 1) * pageSize;

            var jobsQuery = _context.Jobs
                .Include(e => e.Industry)
                .Include(e => e.Location)
                .Include(e => e.Employer)
                    .ThenInclude(tl => tl.Jobs)
                .Include(e => e.Employer)
                    .ThenInclude(tl => tl.CommentToEmployer)
                .Include(e => e.JobRequirements)
                .Include(e => e.JobRequestFields)
                .Include(e => e.JobTagsMarks)
                .Include(e => e.JobRecommendationList)
                .Include(e => e.JobTagsPros)
                    .ThenInclude(tl => tl.TagsList)
                .Where(e => jobIds.Contains(e.id ?? 0));

            var jobs = await jobsQuery.ToListAsync();

            var jobWithRequestDTOs = jobs
                .Select(e => new JobWithRequestDTO
                {
                    JobShortDTO = new JobShortDTO
                    {
                        Id = e.id,
                        JobTitle = e.job_title,
                        JobImg = e.job_img,
                        JobBackgroundImg = e.job_background_img,
                        JobSalaryMin = e.job_salary_min,
                        JobSalaryMax = e.job_salary_max,
                        JobSalaryCurrency = e.job_salary_currency,
                        JobDescription = e.job_description,
                        NumberCandidates = e.number_candidates,
                        NumberView = e.number_view,
                        DateEnding = e.date_ending,
                        DateLastEditing = e.date_last_editing,
                        DateApproving = e.date_approving,
                        Status = e.status,
                        CreatedAt = e.created_at,
                        isExistJobRequest = intId.HasValue && jobRequests.Any(jr => jr.UserId == intId && jr.JobId == e.id && jr.Status != null),
                        isSavedJob = true,
                        Location = e.Location,
                        Industry = e.Industry,
                        JobTagsMarks = e.JobTagsMarks != null ? new JobTagsMarksDTO
                        {
                            Id = e.JobTagsMarks.id,
                            TagHot = e.JobTagsMarks.tag_hot,
                            TagNew = e.JobTagsMarks.tag_new,
                            TagRecommend = e.JobRecommendationList != null && e.JobRecommendationList.Any(r => r.JobId == e.id && r.UserId == intId),
                        } : null,
                        JobTagsPros = e.JobTagsPros.Select(t => new JobTagsProsDTO
                        {
                            Id = t.id,
                            JobTagsProsName = t.TagsList.tags_name,
                        }).ToList(),
                        Employer = e.Employer != null ? new EmployerShortDTO
                        {
                            Id = e.id,
                            CompanyName = e.Employer.company_name,
                            CompanyIndustryDescription = e.Employer.company_industry_description,
                            CompanyShortDescription = e.Employer.company_short_description,
                            CompanyChecked = e.Employer.company_checked,
                            CompanyURL = e.Employer.company_url,
                            CompanyYear = e.Employer.company_year_experience,
                            CompanyRating = e.Employer.CommentToEmployer.Any() ? e.Employer.CommentToEmployer.Average(c => c.comment_stars) : 0,
                            CompanyImg = e.Employer.company_img,
                            EmployerCreatedAt = e.Employer.employer_created_at,
                            CommentCount = e.Employer.CommentToEmployer.Count(),
                        } : null,
                    },
                    JobRequestDTO = jobRequests.Where(jr => jr.JobId == e.id).Select(jr => new JobRequestDTO
                    {
                        Id = jr.Id,
                        JobId = jr.JobId,
                        UserId = jr.UserId,
                        Resume = jr.Resume != null ? new ResumeShortDTO
                        {
                            Id = jr.Resume.Id,
                            ResumeName = jr.Resume.ResumeName,
                            ResumeDescription = jr.Resume.ResumeDescription,
                            ResumeActive = jr.Resume.ResumeActive
                        } : null,
                        ResumeURL = jr.ResumeURL,
                        CoverLetter = jr.CoverLetter,
                        Positives = jr.Positives,
                        Projects = jr.Projects,
                        Status = jr.Status,
                        CreatedAt = jr.CreatedAt
                    }).FirstOrDefault() 
                })
                .OrderBy(e => e.JobRequestDTO.CreatedAt)
                .Skip(startIndex)
                .Take(pageSize)
                .ToList();

            return Ok(jobWithRequestDTOs);
        }*/


        [HttpGet("jobs-with-requests-with-param")]
        public async Task<ActionResult<List<JobWithRequestDTO>>> GetJobsWithRequests(string userId, int page = 1, int pageSize = 18, string type = "active")
        {
            Console.WriteLine(userId);
            Console.WriteLine(page);
            Console.WriteLine(pageSize);

            Location userLocation = new Location();
            int? intId = 0;

            if (!string.IsNullOrEmpty(userId) && userId != "NULL")
            {
                var user = await _userController.GetUserById(userId);

                if (user == null)
                {
                    return NotFound($"User not found: {userId}");
                }

                intId = user.UserInfo.Id;
                userLocation = user.UserInfo.Location;
            }

            var jobRequests = await _context.JobRequests
                .Where(jr => jr.UserId == intId)
                .Include(jr => jr.Resume) // З'єднуємо з резюме
                .ToListAsync();

            if (!jobRequests.Any())
            {
                return Ok(new List<JobWithRequestDTO>());
            }

            List<int> jobIds;
            DateTime now = DateTime.Now;

            if (type == "active")
            {
                jobIds = jobRequests.Where(jr => jr.Status == "active").Select(jr => jr.JobId).ToList();
            }
            else
            {
                /*                jobIds = jobRequests.Where(jr => jr.Status == null || jr.Status == "denied").Select(jr => jr.JobId).ToList();
                */
                jobIds = jobRequests.Select(jr => jr.JobId).ToList();
            }


            var startIndex = (page - 1) * pageSize;

            var jobsQuery = _context.Jobs
                .Include(e => e.Industry)
                .Include(e => e.Location)
                .Include(e => e.Employer)
                    .ThenInclude(tl => tl.Jobs)
                .Include(e => e.Employer)
                    .ThenInclude(tl => tl.CommentToEmployer)
                .Include(e => e.JobRequirements)
                .Include(e => e.JobRequestFields)
                .Include(e => e.JobTagsMarks)
                .Include(e => e.JobRecommendationList)
                .Include(e => e.JobTagsPros)
                    .ThenInclude(tl => tl.TagsList)
                .Where(e => jobIds.Contains(e.id ?? 0));


            if (type == "active")
            {
                Console.WriteLine(type);
                jobsQuery = jobsQuery.Where(e => e.date_ending == null || e.date_ending > now);
            }
            else
            {
                Console.WriteLine(type);
                jobsQuery = jobsQuery.Where(e => e.date_ending != null && e.date_ending <= now);
            }

            var jobs = await jobsQuery.ToListAsync();

            var jobWithRequestDTOs = jobs
                .Select(e => new JobWithRequestDTO
                {
                    JobShortDTO = new JobShortDTO
                    {
                        Id = e.id,
                        JobTitle = e.job_title,
                        JobImg = e.job_img,
                        JobBackgroundImg = e.job_background_img,
                        JobSalaryMin = e.job_salary_min,
                        JobSalaryMax = e.job_salary_max,
                        JobSalaryCurrency = e.job_salary_currency,
                        JobDescription = e.job_description,
                        NumberCandidates = e.number_candidates,
                        NumberView = e.number_view,
                        DateEnding = e.date_ending,
                        DateLastEditing = e.date_last_editing,
                        DateApproving = e.date_approving,
                        Status = e.status,
                        CreatedAt = e.created_at,
                        isExistJobRequest = intId.HasValue && jobRequests.Any(jr => jr.UserId == intId && jr.JobId == e.id && jr.Status != null),
                        isSavedJob = true,
                        Location = e.Location,
                        Industry = e.Industry,
                        JobTagsMarks = e.JobTagsMarks != null ? new JobTagsMarksDTO
                        {
                            Id = e.JobTagsMarks.id,
                            TagHot = e.JobTagsMarks.tag_hot,
                            TagNew = e.JobTagsMarks.tag_new,
                            TagRecommend = e.JobRecommendationList != null && e.JobRecommendationList.Any(r => r.JobId == e.id && r.UserId == intId),
                        } : null,
                        JobTagsPros = e.JobTagsPros.Select(t => new JobTagsProsDTO
                        {
                            Id = t.id,
                            JobTagsProsName = t.TagsList.tags_name,
                        }).ToList(),
                        Employer = e.Employer != null ? new EmployerShortDTO
                        {
                            Id = e.id,
                            CompanyName = e.Employer.company_name,
                            CompanyIndustryDescription = e.Employer.company_industry_description,
                            CompanyShortDescription = e.Employer.company_short_description,
                            CompanyChecked = e.Employer.company_checked,
                            CompanyURL = e.Employer.company_url,
                            CompanyYear = e.Employer.company_year_experience,
                            CompanyRating = e.Employer.CommentToEmployer.Any() ? e.Employer.CommentToEmployer.Average(c => c.comment_stars) : 0,
                            CompanyImg = e.Employer.company_img,
                            EmployerCreatedAt = e.Employer.employer_created_at,
                            CommentCount = e.Employer.CommentToEmployer.Count(),
                        } : null,
                    },
                    JobRequestDTO = jobRequests.Where(jr => jr.JobId == e.id).Select(jr => new JobRequestDTO
                    {
                        Id = jr.Id,
                        JobId = jr.JobId,
                        UserId = jr.UserId,
                        Resume = jr.Resume != null ? new ResumeShortDTO
                        {
                            Id = jr.Resume.Id,
                            ResumeName = jr.Resume.ResumeName,
                            ResumeDescription = jr.Resume.ResumeDescription,
                            ResumeActive = jr.Resume.ResumeActive
                        } : null,
                        ResumeURL = jr.ResumeURL,
                        CoverLetter = jr.CoverLetter,
                        Positives = jr.Positives,
                        Projects = jr.Projects,
                        Status = jr.Status,
                        CreatedAt = jr.CreatedAt
                    }).FirstOrDefault()
                })
                .OrderByDescending(e => e.JobRequestDTO.CreatedAt)
                .Skip(startIndex)
                .Take(pageSize)
                .ToList();

            return Ok(jobWithRequestDTOs);
        }

        [HttpDelete("delete-job-request/{jobId}/{userId}")]
        public async Task<IActionResult> DeleteJobRequest(int jobId, int userId)
        {
            var result = await DeleteJobRequestAsync(jobId, userId);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }


        private async Task<bool> DeleteJobRequestAsync(int jobId, int userId)
        {
            var jobRequest = await _context.JobRequests.FirstOrDefaultAsync(jr => jr.JobId == jobId && jr.UserId == userId);

            if (jobRequest == null)
            {
                return false;
            }

            _context.JobRequests.Remove(jobRequest);
            await _context.SaveChangesAsync();

            return true;
        }


        [HttpGet("job_request_fields/{jobId}")]
        public async Task<ActionResult<JobRequestFieldsDTO>> GetJobRequestFields(int jobId)
        {
            var jobRequestField = await _context.JobRequestFields
                .FirstOrDefaultAsync(jrf => jrf.JobId == jobId);

            if (jobRequestField == null)
            {
                return NotFound($"JobRequestFields for JobId {jobId} not found.");
            }

            var jobRequestFieldsDTO = new JobRequestFieldsDTO
            {
                Id = jobRequestField.Id,
                NeedAdditionalResume = jobRequestField.NeedAdditionalResume,
                NeedResume = jobRequestField.NeedResume,
                PositiveField = jobRequestField.PositiveField,
                ProjectField = jobRequestField.ProjectField
            };

            return Ok(jobRequestFieldsDTO);
        }


    }
  
}




















