using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_search_job.Data;
using Web_search_job.DatabaseClasses;
using Web_search_job.DatabaseClasses.FiltersFolder;
using Web_search_job.DTO.Employer;
using Web_search_job.DTO.Job;
using Web_search_job.DTO.Other;
using Web_search_job.DTO.User;

namespace Web_search_job.Controllers.DatabaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtherInfoController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly UserController _userController;

        public OtherInfoController(DataContext context, UserController userController)
        {
            _context = context;
            _userController = userController;
        }

        [HttpGet("locations")]
        public async Task<ActionResult<LocationDataDTO>> GetLocation(string? userId, string anonymLocation = "", string employerLocationCountry = "", string employerLocationRegion = "", string employerLocationCity = "")
        {
            Location? userLocation = new Location();

            if (userId != null && userId != "" && userId != "null")
            {
                var user = await _userController.GetUserById(userId);

                if (user == null)
                {
                    return NotFound($"Помилка користувача {userId}");
                }

                if (user.UserInfo == null)
                {
                    userLocation = null;
                }
                else 
                {
                    userLocation = user.UserInfo.Location;
                }

            }

            var location = await _context.Locations
                .Select(e => new LocationDataDTO
                {
                    id = e.id,
                    location_city = e.location_city,
                    location_region = e.location_region,
                    location_country = e.location_country,
                })
                .ToListAsync();

            if (location == null)
            {
                return NotFound();
            }

            if (userLocation != null && userLocation.location_region != "" && employerLocationCountry != "" && employerLocationRegion != "" && employerLocationCity != "" 
                && employerLocationCountry != null && employerLocationRegion != null && employerLocationCity != null )
            {
                var sortedList = location
                .OrderByDescending(j => 
                    j.location_region == userLocation.location_region
                    && j.location_city == userLocation.location_city
                    && j.location_country == userLocation.location_country)
                .ThenByDescending(j =>
                    j.location_region == employerLocationRegion
                    && j.location_city == employerLocationCity
                    && j.location_country == employerLocationCountry)
                .ThenByDescending(j => j.location_region)
                .ToList();

                return Ok(sortedList);
            }
            else if (anonymLocation != "" && anonymLocation != null && anonymLocation != "NULL")
            {
                var sortedList = location
                .OrderByDescending(j =>
                    j.location_region == anonymLocation)
                .ThenByDescending(j => j.location_region)
                .ToList();

                return Ok(sortedList);
            }
            else {
                var sortedList = location
                .OrderByDescending(j => j.location_region)
                .ToList();

                return Ok(sortedList);
            }
        }


        [HttpGet("industries")]
        public async Task<ActionResult<IndustryDataDTO>> GetIndustry(string anonymindustry = "",  string employerIndustry = "")
        {
            var industry = await _context.Industry
                .Select(e => new IndustryDataDTO
                {
                    id = e.id,
                    industry_name = e.industry_name,
                })
                .ToListAsync();

            if (industry == null)
            {
                return NotFound();
            }

            if (anonymindustry != "" && employerIndustry != "" && employerIndustry != null && employerIndustry != null)
            {
                var sortedList = industry
                .OrderByDescending(j =>
                    j.industry_name == anonymindustry)
                .ThenByDescending(j =>
                    j.industry_name == employerIndustry)
                .ThenByDescending(j => j.industry_name)
                .ToList();

                return Ok(sortedList);
            }
            else if (anonymindustry != "")
            {
                var sortedList = industry
                .OrderByDescending(j =>
                    j.industry_name == anonymindustry)
                .ThenByDescending(j => j.industry_name)
                .ToList();

                return Ok(sortedList);
            }
            else if (employerIndustry != "")
            {
                var sortedList = industry
                .OrderByDescending(j =>
                    j.industry_name == employerIndustry)
                .ThenByDescending(j => j.industry_name)
                .ToList();

                return Ok(sortedList);
            }
            else
            {
                var sortedList = industry
                .OrderByDescending(j => j.industry_name)
                .ToList();

                return Ok(sortedList);
            }
        }


    }
}
