using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PNGOptimizer
{
    class ProcessOrganizer
    {

        public ProcessOrganizer()
        {

        }

        public void executeProcess(string path, string destination)
        {
            try
            {
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = @"Optimizer\\optipng.exe";
                info.Arguments = @"-o7 " + path;
                info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

                using (Process process = Process.Start(info))
                {
                    process.WaitForExit();
                }
                FileOrganizer.moveFileOnDestination(path, destination);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

    }
}
