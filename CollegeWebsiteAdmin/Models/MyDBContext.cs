using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;

namespace CollegeWebsiteAdmin.Models
{

    public class MyDBContext : DbContext
    {
        private readonly IConfiguration _myAppSettingsConfig;
        public MyDBContext(IConfiguration configFromAppSettings)
        {
            _myAppSettingsConfig = configFromAppSettings;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Use the connection string from appsettings.json
                optionsBuilder.UseSqlServer(_myAppSettingsConfig.GetConnectionString("ABCDatabase"));
            }
        }
        public DbSet<PagesInfo> PagesInfo { get; set; }
        public DbSet<CoursesInfo> CoursesInfo { get; set; }
        public DbSet<NewsInfo> NewsInfo { get; set; }
        public DbSet<ContactFormInfo> ContactFormInfo { get; set; }
    }
}
