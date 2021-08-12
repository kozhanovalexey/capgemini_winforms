using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        string path = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void OpenToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrEmpty(fbd.SelectedPath))
                {
                    path = fbd.SelectedPath;
                    //MessageBox.Show(path);
                    StartLoading();
                }
            }
        }

        private void StartLoading()
        {
            treeView1.Nodes.Clear();
            if (path != "" && Directory.Exists(path))
                LoadDirectory(path);
            else
                MessageBox.Show("Select Directory!!");
        }

        public void LoadDirectory(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            TreeNode tds = treeView1.Nodes.Add(di.Name);
            tds.Tag = di.FullName;
            tds.StateImageIndex = 0;
            LoadFiles(path, tds);
            tds.Expand();
        }

        private void LoadFiles(string dir, TreeNode td)
        {
            var files = Directory.EnumerateFiles(dir, "*.*", SearchOption.TopDirectoryOnly).Where(
                s => s.EndsWith(".jpg") 
                || s.EndsWith(".gif")
                || s.EndsWith(".png")
                || s.EndsWith(".bmp")
                || s.EndsWith(".jpe")
                || s.EndsWith(".jpeg")
                || s.EndsWith(".wmf")
                || s.EndsWith(".emf")
                || s.EndsWith(".xbm")
                || s.EndsWith(".ico")
                || s.EndsWith(".tif")
                || s.EndsWith(".tiff"));
            if (files.Count() == 0) MessageBox.Show("Нет изображений в выбранной директории!");

            foreach (string file in files)
            {
                FileInfo fi = new FileInfo(file);
                TreeNode tds = td.Nodes.Add(fi.Name);
                tds.Tag = fi.FullName;
                tds.StateImageIndex = 1;
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode.Level == 1)
            {
                string path_image = path + "\\" + treeView1.SelectedNode.Text;
                pictureBox1.Image = Image.FromFile(@path_image);
            }
        }
    }
}
