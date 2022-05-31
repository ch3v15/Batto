using System;
using System.Text;
using System.Reflection;
using System.IO;
using System.Resources;
using System.Diagnostics;

namespace Program
{
    public class Program
    {
        public static void Main()
        {
            //Get out batch code
            ResourceManager Manager = new ResourceManager("Batto", Assembly.GetExecutingAssembly());
            string ResourceCode = Manager.GetString("batch");

            //Our process batch
            File.WriteAllText(@"C:\Windows\Temp\batto.bat", ResourceCode);
            Process.Start("cmd", @"/c C:\Windows\Temp\batto.bat").WaitForExit();
            File.Delete(@"C:\Windows\Temp\batto.bat");
        }
    }
}