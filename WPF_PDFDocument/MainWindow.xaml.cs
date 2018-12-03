using System.Windows;
using System.Windows.Input;

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
    }
}
