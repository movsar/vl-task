using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data {
    internal static class Constants {
        private const string localServerName = "DESKTOP-BODQELM";
        public const string localConnectionString = $"Data Source={localServerName};Database=TestDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public const string remoteConnectionString = "";
    }
}
