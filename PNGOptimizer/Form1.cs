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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon("color_settings_icon.ico");
            this.loadTB.AutoSize = false;
            this.loadTB.Size = new System.Drawing.Size(600, 30);
            this.destinationTB.AutoSize = false;
            this.destinationTB.Size = new System.Drawing.Size(600, 30);
            this.btnFile.Checked = true;
            this.rbNormal.Checked = true;
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
                folderBrowserDialog.ShowDialog();

                string path = folderBrowserDialog.SelectedPath;
                this.loadTB.Text = path;

                string[] paths = System.IO.Directory.GetFiles(path);

                int i = listOfFiles.Items.Count + 1;
                int k = 0;

                foreach (String item in paths)
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
                    k++;
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
            int quality;
            if (rbLow.Checked)
            {
                quality = 1;
            }
            else if(rbNormal.Checked)
            {
                quality = 2;
            }
            else
            {
                quality = 3;
            }


            Optimizer optimizer = new Optimizer(listOfFiles, destinationTB.Text, quality);
            Thread thread = new Thread(new ThreadStart(optimizer.startOptimization));
            thread.Start();
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
