﻿using Business.Constants;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Configuration
{
    public static class DbConfiguration
    {
        public static string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../../../WebAPI"));
                configurationManager.AddJsonFile("appsettings.json");

                string connectionString = configurationManager.GetConnectionString(DbConfigurationMessages.MySQLConnectionString);
                return connectionString == null ? throw new Exception(DbConfigurationMessages.ConnectionStringNotFound) : connectionString;
            }
        }
    }
}
