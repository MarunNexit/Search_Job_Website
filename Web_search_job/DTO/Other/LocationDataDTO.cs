namespace Web_search_job.DTO.Other
{
    public class LocationDataDTO
    {
        public int? id { get; set; }

        public string location_city { get; set; } = "";
        public string location_region { get; set; } = "";
        public string location_country { get; set; } = "";
    }
}
