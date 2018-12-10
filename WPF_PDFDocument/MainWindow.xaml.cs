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

                //Nếu file đã open
                for (int i = 0; i < OpenedFiles.Count; i++)
                {
                    if(OpenedFiles[i]==info.Name)
                    {
                        this.TabController.SelectedIndex = i;
                        MessageBox.Show("File is already opened!", "Quick PDF Editor", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }

                //Nếu File chưa Open
                System.Windows.Controls.TabItem tabItem = new System.Windows.Controls.TabItem();
                tabItem.Header = info.Name;

                //Thêm Tên file vào list
                OpenedFiles.Add(info.Name);
                //Opened File
                IsSavedTab.Add(true);


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
                    if (result == MessageBoxResult.Yes)
                    {
                        //Save
                    }

                    if (result == MessageBoxResult.No)
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

            //get path
            var tabItem = TabController.SelectedItem as System.Windows.Controls.TabItem;
            var pdfview = tabItem.Content as Controls.PdfViewer;
            string path = pdfview.PdfPath;

            Dialog.InsertPage insertPage = new Dialog.InsertPage(tabItem);
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

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var tab = this.TabController.SelectedItem as System.Windows.Controls.TabItem;
            var pdfviewer = tab.Content as Controls.PdfViewer;

            if(pdfviewer.PdfPath==pdfviewer.OriginalPdfPath)
            {
                return;
            }

            System.IO.File.Delete(pdfviewer.OriginalPdfPath);

            FileInfo fileInfo = new FileInfo(pdfviewer.PdfPath);
            fileInfo.MoveTo(pdfviewer.OriginalPdfPath);
            MessageBox.Show("File Saved!", "Quick Pdf Editor", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SaveAll_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            if(this.OpenedFiles.Count==0)
            {
                MessageBox.Show("There is nothing to close!", "Quick PDF Editor", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            //Compare original path with pdfpath
            var tab = this.TabController.SelectedItem as System.Windows.Controls.TabItem;
            var pdfviewer = tab.Content as Controls.PdfViewer;
            if(pdfviewer.OriginalPdfPath!=pdfviewer.PdfPath)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to save this document?", "Quick Pdf Editor", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Cancel)
                    return;
                if(result==MessageBoxResult.Yes)
                {
                    MessageBox.Show("Save");
                }
            }
            int index = TabController.SelectedIndex;
            this.TabController.Items.RemoveAt(index);
            this.OpenedFiles.RemoveAt(index);
        }

        private void CloseAll_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
