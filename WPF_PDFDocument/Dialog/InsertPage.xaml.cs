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
    /// <summary>
    /// Interaction logic for InsertPage.xaml
    /// </summary>
    public partial class InsertPage : Window
    {
        public InsertPage()
        {
            InitializeComponent();
        }

        private void InsertCurrentPage_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem listBoxItem = new ListBoxItem();
            //var item = PreviewPDF.PagesContainer.Items.CurrentPosition;
            listBoxItem.Content = "Page ";
            this.ListBox_Data.Items.Add(listBoxItem);
        }

        private void InsertPage_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
