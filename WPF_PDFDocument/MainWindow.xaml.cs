using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;


namespace WPF_PDFDocument
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Collections.Generic.List<Boolean> IsSavedTab;
        private System.Collections.Generic.List<string> OpenedFiles;
        private int NewDocCount;
        public MainWindow()
        {
            InitializeComponent();
            IsSavedTab = new System.Collections.Generic.List<bool>();
            OpenedFiles = new System.Collections.Generic.List<string>();
            NewDocCount = 0;
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

            FileInfo info;
            if (openFile.ShowDialog() == true)
            {
                info = new FileInfo(openFile.FileName);
                if (info.Extension != ".pdf")
                {
                    MessageBox.Show("Can't open file");
                    return;
                }
                //pdfviewer.PdfPath = openFile.FileName;

                //Nếu File chưa Open
                System.Windows.Controls.TabItem tabItem = new System.Windows.Controls.TabItem();
                tabItem.Header = info.Name;

                //Thêm Tên file vào list
                OpenedFiles.Add(info.Name);

                Controls.PdfViewer pdfViewer = new Controls.PdfViewer();
                pdfViewer.PdfPath = openFile.FileName;
                tabItem.Content = pdfViewer;
                this.TabController.Items.Add(tabItem);
                Dispatcher.BeginInvoke((Action)(() => this.TabController.SelectedIndex = this.TabController.Items.Count - 1));
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            if (IsSavedTab.Count == 0)
                this.Close();

            for (int i = 0; i < IsSavedTab.Count; i++)
            {
                if (IsSavedTab[i] == false)
                {
                    MessageBoxResult result = MessageBox.Show("Do you want to save " + OpenedFiles[i] + "?", "Save?", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                    if(result== MessageBoxResult.Yes)
                    {
                        //Save
                    }
                    
                    if(result==MessageBoxResult.No)
                    {
                        //Discard

                    }
                }
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Dialog.About about = new Dialog.About();
            about.Show();
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

            if (openFile.FileName == "")
                return;

            Dialog.InsertPage insertPage = new Dialog.InsertPage();
            insertPage.PreviewPDF.PdfPath = openFile.FileName;
            insertPage.Show();
        }

        private void NewTab_Click(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Controls.TabItem tabItem = new System.Windows.Controls.TabItem();
            tabItem.Header = "Blank";
            this.TabController.Items.Add(tabItem);
            tabItem.BringIntoView();
        }

        private void NewBlankPdf_Click(object sender, RoutedEventArgs e)
        {
            ///Chưa hoàn thiện
            //Nếu File chưa Open
            System.Windows.Controls.TabItem tabItem = new System.Windows.Controls.TabItem();
            tabItem.Header = "Document" + (++NewDocCount);
            Controls.PdfViewer pdfViewer = new Controls.PdfViewer();
            tabItem.Content = pdfViewer;
            this.TabController.Items.Add(tabItem);
            Dispatcher.BeginInvoke((Action)(() => this.TabController.SelectedIndex = this.TabController.Items.Count - 1));
        }
    }
}
