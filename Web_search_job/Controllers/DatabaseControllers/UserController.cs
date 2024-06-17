using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_search_job.Data;
using Web_search_job.DatabaseClasses;
using Web_search_job.DatabaseClasses.FiltersFolder;
using Web_search_job.DatabaseClasses.JobFolder;
using Web_search_job.DTO.User;

namespace Web_search_job.Controllers.DatabaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("get_user_data_by_id")]
        public async Task<ApplicationUser> GetUserById(string userId)
        {
            var user = await _context.Users
                .Include(u => u.UserInfo)
                    .ThenInclude(l => l.Location)
                .FirstOrDefaultAsync(u => u.Id == userId);

            Console.WriteLine(user);
            return user;
        }



        [HttpGet("user_data_by_id")]
        public async Task<IActionResult> GetUserDataById(string userId)
        {
            var user = await _context.Users
                .Include(u => u.UserInfo)
                    .ThenInclude(l => l.Location)
                .Where(u => u.Id == userId)
                .Select(u => new UserDTO
                {
                    Id = u.UserInfo.Id,
                    UserId = u.Id,
                    Email = u.UserName,
                    FirstName = u.UserInfo.FirstName,
                    LastName = u.UserInfo.LastName,
                    UserAge = CalculateAge(u.UserInfo.DateOfBirth),
                    PhoneNumber = u.UserInfo.PhoneNumber,
                    UserImg = u.UserInfo.UserImg,
                    UserCreatedAt = u.UserInfo.ActionCreatedAt,
                    Gender = u.UserInfo.Gender,
                    Location = new Location
                    {
                        id = u.UserInfo.Location.id,
                        location_city = u.UserInfo.Location.location_city,
                        location_region = u.UserInfo.Location.location_region,
                        location_country = u.UserInfo.Location.location_country,
                    }
                })
                .FirstOrDefaultAsync();


            if (user == null)
            {
                return NotFound("Помилка користувача.");
            }

            return Ok(user);
        }


        private async Task<List<SavedJob>> GetSavedJobsByUser(string userId)
        {
            // Отримати список збережених робіт для даного користувача

            var user = await _context.Users
                .Include(u => u.UserInfo)
                    .ThenInclude(l => l.SavedJobs)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null || user.UserInfo == null)
            {
                return new List<SavedJob>(); // Повернути порожній список, якщо користувача не знайдено
            }

            return user.UserInfo.SavedJobs.ToList();

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
        /*

                [HttpPost]
                public async Task<IActionResult> CreateAudit([FromBody] Audit audit)
                {
                    if (audit == null)
                    {
                        return BadRequest("Invalid audit data.");
                    }

                    audit.action_created_at = DateTime.UtcNow;
                    _context.Audit.Add(audit);
                    await _context.SaveChangesAsync();

                    return Ok(audit);
                }
        */

        [HttpGet("get_statistics_searcher")]
        public async Task<IActionResult> GetStatisticsSearcherAsync(string userId)
        {
            try
            {
                int? intId = 0;

                if (!string.IsNullOrEmpty(userId) && userId != "NULL")
                {
                    var user = await GetUserById(userId);

                    if (user == null)
                    {
                        return NotFound($"Користувача з ID {userId} не знайдено");
                    }

                    intId = user.UserInfo.Id;
                }



                DateTime today = DateTime.UtcNow.Date.AddDays(1).AddTicks(-1); // End of today
                DateTime lastWeek = today.AddDays(-6).Date; // Start of last week

                // Кількість відправлених заявок за останній тиждень для конкретного користувача
                int countLastWeek = await _context.JobRequests
                    .Where(jr => jr.CreatedAt >= lastWeek && jr.CreatedAt <= today && jr.UserId == intId)
                    .CountAsync();

                // Кількість відправлених заявок за весь час
                int countTotal = await _context.JobRequests
                    .Where(jr => jr.UserId == intId)
                    .CountAsync();

                string topIndustry = "";

                topIndustry = await _context.JobRequests
                   .Where(jr => intId == 0 || jr.UserId == intId)
                   .GroupBy(jr => jr.Job.Industry.industry_name)
                   .OrderByDescending(g => g.Count())
                   .Select(g => g.Key)
                   .FirstOrDefaultAsync();

                // Найбільш активний час пошуку (години)
                var hoursPeriods = Enumerable.Range(0, 24).Select(h => (h / 2) + 1); // Години в періодах по 2 години
                int mostActiveHourPeriod = await _context.Audit
                    .Where(jr => jr.user_id == intId)
                    .GroupBy(jr => jr.action_created_at.Hour / 2)
                    .OrderByDescending(g => g.Count())
                    .Select(g => g.Key + 1)
                    .FirstOrDefaultAsync();


                var mostPopularDayOfWeek = await _context.Audit
                    .Where(jr => (jr.user_id == intId))
                    .GroupBy(jr => EF.Functions.DateDiffDay(jr.action_created_at, DateTime.UtcNow) % 7) // Group by day of the week as integer
                    .OrderByDescending(g => g.Count())
                    .Select(g => (DayOfWeek)((g.Key + 1) % 7)) // Convert integer back to DayOfWeek
                    .FirstOrDefaultAsync();


                /*      // Найбільш популярний день тижня
                      DayOfWeek mostPopularDayOfWeek = await _context.JobRequests
                          .Where(jr => jr.UserId == intId)
                          .GroupBy(jr => jr.CreatedAt.DayOfWeek)
                          .OrderByDescending(g => g.Count())
                          .Select(g => g.Key)
                          .FirstOrDefaultAsync();*/

                // Найбільш популярні регіони в запитах (максимум 3)
                var popularRegions = await _context.JobRequests
                    .Where(jr => (jr.UserId == intId) && jr.Job.Location != null)
                    .GroupBy(jr => jr.Job.Location.location_city)
                    .OrderByDescending(g => g.Count())
                    .Select(g => g.Key)
                    .Take(3)
                    .ToListAsync();

                string topRegions = string.Join(", ", popularRegions);


                // Найбільш популярна країна в запитах
                string topCountry = await _context.JobRequests
                  .Where(jr => (jr.UserId == intId) && jr.Job.Location != null)
                  .GroupBy(jr => jr.Job.Location.location_country)
                  .OrderByDescending(g => g.Count())
                  .Select(g => g.Key)
                  .FirstOrDefaultAsync();



                DateTime today_ = DateTime.UtcNow.Date;

                // Зміна активності (відсоток зміни за останній місяць)
                DateTime lastMonth = today_.AddMonths(-1);
                int lastMonthAuditsCount = await _context.Audit
                    .Where(a => a.action_created_at >= lastMonth && a.action_created_at < today_ && (a.user_id == intId))
                    .CountAsync();

                int currentMonthAuditsCount = await _context.Audit
                    .Where(a => a.action_created_at >= today.AddMonths(-1) && (a.user_id == intId))
                    .CountAsync();

                double changePercentage = CalculatePercentageChange(lastMonthAuditsCount, currentMonthAuditsCount);

                var dataSummary = new DataSummary
                {
                    CountLastWeek = countLastWeek,
                    CountTotal = countTotal,
                    TopIndustry = topIndustry,
                    MostActiveHourPeriod = mostActiveHourPeriod,
                    MostPopularDayOfWeek = mostPopularDayOfWeek,
                    TopRegions = topRegions,
                    TopCountry = topCountry,
                    ChangePercentage = changePercentage
                };

                return Ok(dataSummary);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Помилка під час отримання статистики: {ex.Message}");
            }
        }

        private double CalculatePercentageChange(int oldValue, int newValue)
        {
            if (oldValue == 0)
                return newValue > 0 ? 100 : 0;

            return ((double)(newValue - oldValue) / oldValue) * 100;
        }
    }


    public class DataSummary
    {
        public int CountLastWeek { get; set; }
        public int CountTotal { get; set; }
        public string TopIndustry { get; set; }
        public int MostActiveHourPeriod { get; set; }
        public DayOfWeek MostPopularDayOfWeek { get; set; }
        public string TopRegions { get; set; }
        public string TopCountry { get; set; }
        public double ChangePercentage { get; set; }
    }

}

