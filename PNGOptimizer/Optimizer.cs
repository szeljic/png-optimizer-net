using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PNGOptimizer
{
    class Optimizer
    {
        private ListView listOfFiles;

        public Optimizer(ListView listOfFiles)
        {
            this.listOfFiles = listOfFiles;
        }

        public string startOptimization(string destination)
        {
            Cursor.Current = Cursors.WaitCursor;
            string path = "";
            ProcessOrganizer process = new ProcessOrganizer();
            foreach(ListViewItem item in listOfFiles.Items)
            {
                path = item.SubItems[3].Text;
                process.executeProcess(path, destination);
            }
            Cursor.Current = Cursors.Default;
            return path;
        }
    }
}
