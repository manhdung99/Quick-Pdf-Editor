using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.IO;
using Microsoft.Win32;

namespace WPF_PDFDocument.Dialog
{
    public partial class PdfMerger : Window
    {
        System.Collections.Generic.List<string> Paths;
        TabControl Tabcontroller;
        public PdfMerger()
        {
            InitializeComponent();
            Paths = new List<string>();
        }

        public PdfMerger(TabControl tabControl)
        {
            InitializeComponent();
            Paths = new List<string>();
            this.Tabcontroller = tabControl;
        }
        private void MergeClick(object sender, MouseButtonEventArgs e)
        {
            if(Paths.Count==0)
            {
                MessageBox.Show("There is nothing to merge!", "Quick Pdf Editor", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Controls.PdfViewer pdfViewer = new Controls.PdfViewer();
            pdfViewer.OriginalPdfPath = "";
            pdfViewer.PdfPath = PDFAction.MergePdf(Paths);
            
            TabItem tabitem = new TabItem();
            tabitem.Header = "Merged pdf";
            tabitem.Content = pdfViewer;
            Tabcontroller.Items.Add(tabitem);
            Tabcontroller.SelectedItem = tabitem;
            this.Close();
        }

        private void InsertClick(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.ShowDialog();
            foreach(string path in openFile.FileNames)
            {
                Paths.Add(path);

                ListViewItem item = new ListViewItem();
                item.Content = path;
                ListView.Items.Add(item);
            }
        }
    }
}
