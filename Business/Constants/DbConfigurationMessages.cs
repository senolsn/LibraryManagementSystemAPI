using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class DbConfigurationMessages
    {
        public static string MySQLConnectionString = "MySQLConnectionString";
        public static string PostgreSQLConnectionString = "PostgreSQLConnectionString";

        public static string ConnectionStringNotFound= "There is no connection string with this key!";
    }
}
