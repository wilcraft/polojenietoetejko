using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace polojenietoetejko.Core
{
    internal static class Validation
    {
        public static bool isAddressValid(string address)
        {
            string regex = @"^((25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)(\.)){3}(25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)";
            if (Regex.Match(address, regex).Success)
            {
                return true;
            }

            return false;
        }
        public static bool IsPortValid(string port)
        {
            if(int.TryParse(port, out int portNumber) && portNumber >= 1000 && portNumber <= 65565)
            {
                return true;
            }
            return false;
        }
    }
}
