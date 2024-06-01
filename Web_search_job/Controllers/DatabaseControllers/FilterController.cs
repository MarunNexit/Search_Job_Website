using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_search_job.Data;
using Web_search_job.DatabaseClasses;
using Web_search_job.DTO.Other;

namespace Web_search_job.Controllers.DatabaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly OtherInfoController _otherInfoController;

        public FilterController(DataContext context, OtherInfoController otherInfoController)
        {
            _context = context;
            _otherInfoController = otherInfoController;
        }

        [HttpGet]
        public async Task<ActionResult<List<object>>> GetAllFilters(
            string? userId,
            string anonymLocation = "",
            string anonymindustry = ""
            )
        {

            var filtersGrouped = await _context.Filters
                .Include(f => f.FilterType)
                .ToListAsync();

            var groupedFilters = filtersGrouped.GroupBy(f => f.FilterType.filter_type_name)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(f => new
                    {
                        id = f.id,
                        filter_name = f.filter_name,
                        filter_type_id = f.filter_type_id,
                        filterType = f.FilterType
                    }).ToList()
                );

            //var industries = await _context.Industry.ToListAsync();

            var industriesResult = await _otherInfoController.GetIndustry(anonymindustry);

            if (industriesResult.Result is OkObjectResult okResult && okResult.Value is List<IndustryDataDTO> industry)
            {
                var filteredIndustry = industry;

                var locationsResult = await _otherInfoController.GetLocation(userId, anonymLocation);

                if (locationsResult.Result is OkObjectResult okResultLocation && okResultLocation.Value is List<LocationDataDTO> locations)
                {
                    // Групування та сортування локацій
                    var filteredLocations = locations
                        .GroupBy(l => l.location_region)
                        .OrderBy(group => group.Key)
                        .Select(group => group.First())
                        .ToList();

                    var result = new
                    {
                        Filters = groupedFilters,
                        Industry = filteredIndustry,
                        Locations = filteredLocations,
                    };

                    return Ok(result);
                }
                else
                {
                    return BadRequest("Не вдалося отримати локації.");
                }
            }
            else
            {
                return BadRequest("Не вдалося отримати індустрії.");
            }
        }
    }
}
