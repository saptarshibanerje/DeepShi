using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepShiShared.Models
{
    public static class SiteKeys
    {
        private static IConfigurationSection _configuration;
        public static void Configure(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }

        public static string WebSiteDomain => _configuration["Issuer"];
        public static string Token => _configuration["Key"];

        //public static string EncryptionKey => _configuration["ProtectionString"];

    }
}
