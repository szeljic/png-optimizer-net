using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Concurrent;

namespace PNGOptimizer
{
    class Optimizer
    {
        ConcurrentStack<String> list;
        private string destination;

        public Optimizer(ListView listOfFiles, string destination)
        {
            list = new ConcurrentStack<string>();
            string path;
            foreach (ListViewItem item in listOfFiles.Items)
            {
                path = item.SubItems[3].Text;
                list.Push(path);
            }
            this.destination = destination;
        }

        public void startOptimization()
        {

            Cursor.Current = Cursors.WaitCursor;
            string path = "";
            ProcessOrganizer process = new ProcessOrganizer();
            while(!list.IsEmpty)
            {
                list.TryPop(out path);
                process.executeProcess(path, this.destination);
            }
            Cursor.Current = Cursors.Default;
        }
    }
}
