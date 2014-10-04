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

        public void executeProcess(string path, string destination, int quality)
        {
            try
            {
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = @"Optimizer\\optipng.exe";

                if(quality == 1)
                {
                    info.Arguments = @"-o3 " + path;
                }
                else if(quality == 2)
                {
                    info.Arguments = @"-o5 " + path;
                }
                else
                {
                    info.Arguments = @"-o7 " + path;
                }

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
