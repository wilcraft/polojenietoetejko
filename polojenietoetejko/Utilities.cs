using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace polojenietoetejko
{
    internal class Utilities
    {
        private static bool isOpenFlag = false;
        public int IPSeperation(ConnectionDialogBox cbd)
        {
            string[] temp = cbd.ConnectionAddress.Split('.');
            return 0;
        }
        internal static string ConsoleLogger()
        {
            [DllImport("kernel32.dll")]
            static extern bool AllocConsole();
            [DllImport("kernel32.dll")]
            static extern bool FreeConsole();
            string buttonText = string.Empty;
            if (isOpenFlag)
            {
                FreeConsole();
                isOpenFlag = !isOpenFlag;
                buttonText = "Open";
            }
            else
            {
                AllocConsole();
                isOpenFlag = !isOpenFlag;
                buttonText = "Close";
            }
            return buttonText;
        }
    }
}
