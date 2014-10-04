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

                switch (quality)
                {
                    case 1:
                        info.Arguments = @"-o3 " + path;
                        break;
                    case 2:
                        info.Arguments = @"-o5 " + path;
                        break;
                    case 3:
                        info.Arguments = @"-o7 " + path;
                        break;
                    default:
                        info.Arguments = path;
                        break;
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
