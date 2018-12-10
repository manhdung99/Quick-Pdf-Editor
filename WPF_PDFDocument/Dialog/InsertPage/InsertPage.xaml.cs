using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WPF_PDFDocument.Dialog
{
    /// <summary>
    /// Interaction logic for InsertPage.xaml
    /// </summary>
    public partial class InsertPage : Window
    {
        private List<int> ListPageInsert;

        public InsertPage()
        {
            InitializeComponent();
            ListPageInsert = new List<int>();
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
            //Create new Pdf from this PDF
            //Perform Insert to pdf
            //Change pdfpath to new pdffile

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
    }
}
