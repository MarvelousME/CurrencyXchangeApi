using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyApi.Manager
{
    static class ConfigurationManager
    {
        public static IConfiguration ApplicationSettings
        {
            get;
        }
        static ConfigurationManager()
        {
            ApplicationSettings = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        }
    }
}
