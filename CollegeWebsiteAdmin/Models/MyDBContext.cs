﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CollegeWebsiteAdmin.Models
{

    public class MyDBContext: DbContext
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


    }
}
