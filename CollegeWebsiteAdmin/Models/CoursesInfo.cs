namespace CollegeWebsiteAdmin.Models
{
    public class CoursesInfo : _BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DurationYears { get; set; }
    }
}
