using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace PNGOptimizer
{
    public partial class Form1 : Form
    {
        private Thread thread;
        Optimizer optimizer;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.loadTB.AutoSize = false;
            this.loadTB.Size = new System.Drawing.Size(600, 30);
            this.destinationTB.AutoSize = false;
            this.destinationTB.Size = new System.Drawing.Size(600, 30);
            this.btnFile.Checked = true;
            this.rbNormal.Checked = true;
            this.btnRemove.Enabled = false;
        }
              
        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (btnFile.Checked)
            {
                btnFolder.Checked = true;
                btnFile.Checked = false;
            }
            else
            {
                btnFile.Checked = true;
                btnFolder.Checked = false;
            }
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            if (btnFolder.Checked)
            {
                btnFile.Checked = true;
                btnFolder.Checked = false;
            }
            else
            {
                btnFolder.Checked = true;
                btnFile.Checked = false;
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {

            if (btnFile.Checked == true)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                openFileDialog.Filter = "Image (*.png;*.bmp;*.gif;*.pnm;*.tiff)|*.png;*.bmp;*.gif;*.pnm;*.tiff";
                openFileDialog.FilterIndex = 1;
                openFileDialog.Multiselect = true;
                openFileDialog.ShowDialog();
                String[] paths = openFileDialog.FileNames;

                string directoryPath = ((paths.Length > 0) ? System.IO.Path.GetDirectoryName(paths[0]) : "");

                int i = listOfFiles.Items.Count + 1;
                int k = 0;
                
                foreach(String item in paths)
                {
                    ListViewItem lvi = new ListViewItem(i.ToString());

                    lvi.SubItems.Add(System.IO.Path.GetFileName(item));

                    if (item.EndsWith(".png"))
                    {
                        lvi.SubItems.Add("PNG");
                    }
                    else if(item.EndsWith(".bmp"))
                    {
                        lvi.SubItems.Add("BMP");
                    }
                    else if(item.EndsWith(".tiff"))
                    {
                        lvi.SubItems.Add("TIFF");
                    }
                    else if(item.EndsWith(".gif"))
                    {
                        lvi.SubItems.Add("GIF");
                    }
                    else if (item.EndsWith(".pnm"))
                    {
                        lvi.SubItems.Add("PNM");
                    }
                    
                    lvi.SubItems.Add(item);
                    listOfFiles.Items.Add(lvi);

                    i++;
                    k++;
                }
                this.loadTB.Text = directoryPath;
            }
            else
            {
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.Desktop;
                folderBrowserDialog.Description = "Select the directory.";
                folderBrowserDialog.ShowNewFolderButton = false;
                DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.Cancel)
                {
                    return;
                }

                string path = folderBrowserDialog.SelectedPath;
                this.loadTB.Text = path;
                string[] paths = System.IO.Directory.GetFiles(path);

                int i = listOfFiles.Items.Count + 1;

                foreach (String item in paths)
                {
                    if(item.EndsWith(".png") || item.EndsWith(".tiff") || item.EndsWith(".gif") || item.EndsWith(".bmp") || item.EndsWith(".pnm"))
                    {
                        ListViewItem lvi = new ListViewItem(i.ToString());

                        lvi.SubItems.Add(System.IO.Path.GetFileName(item));

                        if (item.EndsWith(".png"))
                        {
                            lvi.SubItems.Add("PNG");
                        }   
                        else if (item.EndsWith(".bmp"))
                        {
                            lvi.SubItems.Add("BMP");
                        }
                        else if (item.EndsWith(".tiff"))
                        {
                            lvi.SubItems.Add("TIFF");
                        }
                        else if (item.EndsWith(".gif"))
                        {
                            lvi.SubItems.Add("GIF");
                        }
                        else if (item.EndsWith(".pnm"))
                        {
                            lvi.SubItems.Add("PNM");
                        }

                        lvi.SubItems.Add(item);
                        listOfFiles.Items.Add(lvi);

                        i++;
                        }
                }
            }
        }

        private void btnSpecify_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.Desktop;
            folderBrowserDialog.Description = "Select the directory.";
            folderBrowserDialog.ShowDialog();

            string path = folderBrowserDialog.SelectedPath;
            this.destinationTB.Text = path;
        }

        private void btnOptimize_Click(object sender, EventArgs e)
        {
            if (this.listOfFiles.Items.Count == 0)
            {
                this.label2.Text = @"Please load images.";
                return;
            }

            if (this.destinationTB.Text.Equals(""))
            {
                this.label2.Text = @"Please specify destination.";
                return;
            }

            this.label2.Text = @"Optimization in progress...";

            progressBar.Maximum = 100;
            progressBar.Value = 0;

            int quality;
            if (rbLow.Checked)
            {
                quality = 1;
            }
            else if (rbNormal.Checked)
            {
                quality = 2;
            }
            else
            {
                quality = 3;
            }

            optimizer = new Optimizer(listOfFiles, destinationTB.Text, quality);
            thread = new Thread(new ThreadStart(optimizer.startOptimization));
            thread.IsBackground = true;
            thread.Start();
            this.backgroundWorker.RunWorkerAsync();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.backgroundWorker.CancelAsync();
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            listOfFiles.Items.Clear();
            this.btnRemove.Enabled = false;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem item in listOfFiles.SelectedItems)
            {
                listOfFiles.Items.Remove(item);
            }

            int i = 1;
            foreach (ListViewItem item in listOfFiles.Items)
            {
                if (Int32.Parse(item.SubItems[0].Text) != i)
                {
                    item.SubItems[0].Text = i.ToString();
                }
                i++;
            }
            this.btnRemove.Enabled = false;

        }

        private void bwDoWork(object sender, DoWorkEventArgs e)
        {
            while(this.progressBar.Value != this.progressBar.Maximum)
            {
                Thread.Sleep(100);
                this.Invoke(new MethodInvoker(delegate() {
                    (sender as BackgroundWorker).ReportProgress((100 * optimizer.numberOfDone) / optimizer.numberOfAll);
                }));
            }

            this.Invoke(new MethodInvoker(delegate()
            {
                this.label2.Text = @"Done.";
            }));
        }

        private void bwProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar.Value = e.ProgressPercentage;
        }

        private void listOfFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listOfFiles.SelectedItems.Count != 0)
            {
                this.btnRemove.Enabled = true;
            }
            else
            {
                this.btnRemove.Enabled = false;
            }
        }

    }
}
