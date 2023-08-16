using System.ComponentModel.DataAnnotations;

namespace CollegeWebsiteAdmin.Models
{
    public class CoursesInfo : _BaseEntity
    {
        [UIHint("NormalDate")]
        public DateTime StartDate { get; set; }
        [UIHint("NormalDate")]
        public DateTime EndDate { get; set; }
        [UIHint("ShowInYears")]
        public int DurationYears { get; set; }
    }
}
