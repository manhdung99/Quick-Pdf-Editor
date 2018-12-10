using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WPF_PDFDocument.Dialog
{
    
    public partial class InsertPage : Window
    {
        TabItem tabitem;
        private List<int> ListPageInsert;
        private int _offset;
        private int offset
        {
            get
            {
                return _offset;
            }
            set
            {
                _offset = value;
                tboffset.Text = _offset.ToString();
            }
              
            
        }
        private string DesPath;
        public InsertPage()
        {
            InitializeComponent();
            ListPageInsert = new List<int>();
            offset = PreviewPDF.PagesContainer.Items.Count;
        }
        
        //Woring
        public InsertPage(TabItem tab)
        {
            InitializeComponent();
            ListPageInsert = new List<int>();
            var pdfviewer = tab.Content as Controls.PdfViewer;
            DesPath = pdfviewer.PdfPath;
            this.tabitem = tab;
        }

        private void InsertCurrentPage_Click(object sender, RoutedEventArgs e)
        {
            ListPageInsert.Add(Convert.ToInt32(PreviewPDF.textbox.Text));
            UpdateListPage();
        }

        public void UpdateListPage()
        {
            ListBox_Data.Items.Clear();
            for (int i = 0; i < ListPageInsert.Count; i++)
            {
                ListBoxItem listBoxItem = new ListBoxItem();
                listBoxItem.Content = "Page " + ListPageInsert[i];
                this.ListBox_Data.Items.Add(listBoxItem);
            }

        }

        private void InsertPage_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            PDFAction.InsertPageFromPdf(this.PreviewPDF.PdfPath, this.DesPath, ListPageInsert,this.offset);
            MessageBox.Show("Insert pages successfully!","Notification",MessageBoxButton.OK,MessageBoxImage.Information);

            //Create Temporary
            var pdfviever = tabitem.Content as Controls.PdfViewer;
            pdfviever.PdfPath= System.IO.Path.Combine(System.IO.Path.GetTempPath(), "Merged" + --PDFAction.number + ".pdf");
            this.Close();
        }

        private void Insertatob_Click(object sender, RoutedEventArgs e)
        {
            ChoosePageInsert Pagea2b = new ChoosePageInsert(this.ListPageInsert,this);
            Pagea2b.Show();
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            int removeindex = ListBox_Data.SelectedIndex;
            this.ListPageInsert.RemoveAt(removeindex);
            UpdateListPage();
        }

        private void PageOffset_Changed(object sender, TextChangedEventArgs e)
        {
            this.offset = Convert.ToInt32(this.tboffset.Text);
        }
    }
}
