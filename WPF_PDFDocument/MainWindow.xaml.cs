using System.Windows;
using System.Windows.Input;

using System;
using System.IO;
using Microsoft.Win32;


namespace WPF_PDFDocument
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        
        private void Pdfviewer_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Fuck");
        }

        private void TabControl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void TabItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("create new tab");
        }

        private void TabButton_Click(object sender, System.EventArgs e)
        {

        }

        private void TabButton_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void PdfViewer_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void OpenFileDialog_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Portable Document Format(*.pdf)|*.pdf|All files (*.*)|*.*";
            
            if(openFile.ShowDialog()==true)
            {
                FileInfo info = new FileInfo(openFile.FileName);
                if (info.Extension != ".pdf")
                {
                    MessageBox.Show("Can't open file");
                    return;
                }

                pdfviewer.PdfPath = openFile.FileName;
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void TabButton_Loaded_1(object sender, RoutedEventArgs e)
        {
            Window window = new Window();
            window.Show();
        }

        private void InsertPage(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Pdf|*.pdf";
            openFile.ShowDialog();
            if (openFile.FileName == null)
                return;

            Dialog.InsertPage insertPage = new Dialog.InsertPage();
            insertPage.PreviewPDF.PdfPath = openFile.FileName;
            insertPage.Show();
        }
    }
}
