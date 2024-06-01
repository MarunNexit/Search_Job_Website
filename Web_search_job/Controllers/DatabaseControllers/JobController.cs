using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Web_search_job.Data;
using Web_search_job.DatabaseClasses;
using Web_search_job.DTO.Employer;
using Web_search_job.DTO.Job;
using Web_search_job.DTO.User;

namespace Web_search_job.Controllers.DatabaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
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
                        TagRecommend = e.JobTagsMarks.tag_recommend,
                    } : null,
                    JobTagsPros = e.JobTagsPros != null ? e.JobTagsPros.Select(t => new JobTagsProsDTO
                    {
                        Id = t.id,
                        JobTagsProsName = t.JobTagsProsList.job_tags_pros_name,
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
                .Include(e => e.JobTagsMarks)
                .Include(e => e.JobTagsPros)
                    .ThenInclude(tl => tl.JobTagsProsList)
                .Include(e => e.UserInfo)
                    .ThenInclude(au => au.ApplicationUser)
                    .ThenInclude(c => c.UserInfo)
                        .ThenInclude(ui => ui.Location)
                .AsQueryable();



            query = query.Where(e => e.JobTagsMarks != null && e.JobTagsMarks.tag_recommend);


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
                    isSavedJob = savedJobIds.Contains((int)e.id),
                    Location = e.Location,
                    Industry = e.Industry,
                    JobTagsMarks = e.JobTagsMarks != null ? new JobTagsMarksDTO
                    {
                        Id = e.JobTagsMarks.id,
                        TagHot = e.JobTagsMarks.tag_hot,
                        TagNew = e.JobTagsMarks.tag_new,
                        TagRecommend = e.JobTagsMarks.tag_recommend,
                    } : null,
                    JobTagsPros = e.JobTagsPros.Select(t => new JobTagsProsDTO
                    {
                        Id = t.id,
                        JobTagsProsName = t.JobTagsProsList.job_tags_pros_name,
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
                    .ThenInclude(tl => tl.JobTagsProsList)
                .Include(e => e.UserInfo)
                    .ThenInclude(au => au.ApplicationUser)
                    .ThenInclude(c => c.UserInfo)
                        .ThenInclude(ui => ui.Location)
                .AsQueryable();


            query = query.Where(e => e.JobTagsMarks != null && e.JobTagsMarks.tag_recommend);


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
                    isSavedJob = savedJobIds.Contains((int)e.id),
                    Location = e.Location,
                    Industry = e.Industry,
                    JobTagsMarks = e.JobTagsMarks != null ? new JobTagsMarksDTO
                    {
                        Id = e.JobTagsMarks.id,
                        TagHot = e.JobTagsMarks.tag_hot,
                        TagNew = e.JobTagsMarks.tag_new,
                        TagRecommend = e.JobTagsMarks.tag_recommend,
                    } : null,
                    JobTagsPros = e.JobTagsPros.Select(t => new JobTagsProsDTO
                    {
                        Id = t.id,
                        JobTagsProsName = t.JobTagsProsList.job_tags_pros_name,
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
        public async Task<ActionResult<List<JobShortDTO>>> GetShortDataJob(
            string userId,
            string showLess,
            string searchBarParam,
            string sortingParam,
            string filtersParam,
            int page = 1,
            int pageSize = 18
            )
        {
            Console.WriteLine(userId);
            Console.WriteLine(page);
            Console.WriteLine(pageSize);
            Console.WriteLine(showLess);
            Console.WriteLine(searchBarParam);
            Console.WriteLine(sortingParam);
            Console.WriteLine(filtersParam);


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
                    .ThenInclude(tl => tl.JobTagsProsList)
                .Include(e => e.UserInfo)
                    .ThenInclude(au => au.ApplicationUser)
                    .ThenInclude(c => c.UserInfo)
                        .ThenInclude(ui => ui.Location)
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
                                query = query.Where(e => e.JobTagsPros != null && e.JobTagsPros.Any(t => t.JobTagsProsList.job_tags_pros_name.Contains(filterValue)));
                                break;
                            // Add more filter types as needed
                            case "salary":
                                string input = filterValue;
                                string numberString = ExtractNumber(input);

                                if (decimal.TryParse(numberString, NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out decimal number))
                                {
                                    query = query.Where(e => e.job_salary_min.HasValue && e.job_salary_min > number ||
                                                              e.job_salary_max.HasValue && e.job_salary_max > number);
                                }
                                else
                                {
                                    Console.WriteLine("Could not parse the number.");
                                }
                                break;
                            // Add more filter types as needed
                            case "experience":
                                query = query.Where(e => e.JobTagsPros != null && e.JobTagsPros.Any(t => t.JobTagsProsList.job_tags_pros_name.Contains(filterValue)));
                                break;
                            // Add more filter types as needed
                            case "type-work":
                                query = query.Where(e => e.JobTagsPros != null && e.JobTagsPros.Any(t => t.JobTagsProsList.job_tags_pros_name.Contains(filterValue)));
                                break;
                            // Add more filter types as needed
                            case "search-request":
                                switch (filterValue)
                                {
                                    case "Рекомендовані вакансії":
                                        query = query.Where(e => e.JobTagsMarks != null && e.JobTagsMarks.tag_recommend);
                                        break;
                                    case "Лише вакансії в ЗСУ":
                                        query = query.Where(e => e.JobTagsPros != null && e.JobTagsPros.Any(t => t.JobTagsProsList.job_tags_pros_name.Contains("ЗСУ")));
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
                    isSavedJob = savedJobIds.Contains((int)e.id),
                    Location = e.Location,
                    Industry = e.Industry,
                    JobTagsMarks = e.JobTagsMarks != null ? new JobTagsMarksDTO
                    {
                        Id = e.JobTagsMarks.id,
                        TagHot = e.JobTagsMarks.tag_hot,
                        TagNew = e.JobTagsMarks.tag_new,
                        TagRecommend = e.JobTagsMarks.tag_recommend,
                    } : null,
                    JobTagsPros = e.JobTagsPros.Select(t => new JobTagsProsDTO
                    {
                        Id = t.id,
                        JobTagsProsName = t.JobTagsProsList.job_tags_pros_name,
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
            }


           


            return Ok(jobs);
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
                    .ThenInclude(tl => tl.JobTagsProsList)
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
                                query = query.Where(e => e.JobTagsPros != null && e.JobTagsPros.Any(t => t.JobTagsProsList.job_tags_pros_name.Contains(filterValue)));
                                break;
                            // Add more filter types as needed
                            case "salary":
                                string input = filterValue;
                                string numberString = ExtractNumber(input);

                                if (decimal.TryParse(numberString, NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out decimal number))
                                {
                                    query = query.Where(e => e.job_salary_min.HasValue && e.job_salary_min > number ||
                                                              e.job_salary_max.HasValue && e.job_salary_max > number);
                                }
                                else
                                {
                                    Console.WriteLine("Could not parse the number.");
                                }
                                break;
                            // Add more filter types as needed
                            case "experience":
                                query = query.Where(e => e.JobTagsPros != null && e.JobTagsPros.Any(t => t.JobTagsProsList.job_tags_pros_name.Contains(filterValue)));
                                break;
                            // Add more filter types as needed
                            case "type-work":
                                query = query.Where(e => e.JobTagsPros != null && e.JobTagsPros.Any(t => t.JobTagsProsList.job_tags_pros_name.Contains(filterValue)));
                                break;
                            // Add more filter types as needed
                            case "search-request":
                                switch (filterValue)
                                {
                                    case "Рекомендовані вакансії":
                                        query = query.Where(e => e.JobTagsMarks != null && e.JobTagsMarks.tag_recommend);
                                        break;
                                    case "Лише вакансії в ЗСУ":
                                        query = query.Where(e => e.JobTagsPros != null && e.JobTagsPros.Any(t => t.JobTagsProsList.job_tags_pros_name.Contains("ЗСУ")));
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
                    isSavedJob = savedJobIds.Contains((int)e.id),
                    Location = e.Location,
                    Industry = e.Industry,
                    JobTagsMarks = e.JobTagsMarks != null ? new JobTagsMarksDTO
                    {
                        Id = e.JobTagsMarks.id,
                        TagHot = e.JobTagsMarks.tag_hot,
                        TagNew = e.JobTagsMarks.tag_new,
                        TagRecommend = e.JobTagsMarks.tag_recommend,
                    } : null,
                    JobTagsPros = e.JobTagsPros.Select(t => new JobTagsProsDTO
                    {
                        Id = t.id,
                        JobTagsProsName = t.JobTagsProsList.job_tags_pros_name,
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

    }
}




















