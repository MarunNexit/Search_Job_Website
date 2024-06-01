using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web_search_job.DTO.Job
{
    public class JobTagsMarksDTO
    {
        public int? Id { get; set; }

        public bool TagHot { get; set; } = false;
        public bool TagNew { get; set; } = false;
        public bool TagRecommend { get; set; } = false;

    }
}
