using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data {
    internal static class Constants {
        private const string localDbName = "TestDb";
        private const string localServerName = "DESKTOP-BODQELM";
        private const string localUserName = "TestDb";
        private const string localPassword = "";

        public const string connectionString = $"Data Source={localServerName};Database={localDbName};User={localUserName};Pwd={localPassword};Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
    }
}
