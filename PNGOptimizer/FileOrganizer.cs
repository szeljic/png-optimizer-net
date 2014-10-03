using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PNGOptimizer
{
    class FileOrganizer
    {

        public static void moveFileOnDestination(string path, string destination)
        {
            string directoryName = System.IO.Path.GetDirectoryName(path);
            string fileName = System.IO.Path.GetFileNameWithoutExtension(path);

            try
            {
                if (File.Exists(destination))
                {
                    File.Delete(destination);
                }
                if (path.EndsWith(".png"))
                {
                    File.Copy(directoryName + "\\" + fileName + ".png", destination + "\\" + fileName + ".png");
                }
                else
                {
                    File.Move(directoryName + "\\" + fileName + ".png", destination + "\\" + fileName + ".png");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }

        }

    }
}
