using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Windows.Data.Pdf;
using Windows.Storage;
using Windows.Storage.Streams;

//using System.Drawing;
//using System.Drawing.Drawing2D;
//using System.Drawing.Imaging;
namespace WPF_PDFDocument.Controls
{
    public partial class PdfViewer : UserControl
    {
        //void testmethod()
        //{
        //    Graphics graphics = Graphics.FromImage(this.PagesContainer.Items.CurrentItem as Bitmap);
        //    graphics.DrawLine(new System.Drawing.Pen(System.Drawing.Brushes.AliceBlue), 0, 0, 50, 50);

        //}
        //Fields
        private double rzoomvalue;
        public double zoomvalue
        {
            get
            {
                return rzoomvalue;
            }
            set
            {
                double a = value;
                if (a > 0.01 && a < 2)
                    rzoomvalue = a;
                else
                    return;

            }
        }
        public PdfViewer()
        {
            InitializeComponent();
            
            zoomvalue = 1;

            //int nopage = PagesContainer.Items.Count;
            //for (int i = 0; i < nopage; i++)
            //{
            //    ComboBoxItem item = new ComboBoxItem();

            //    Pages.Items.Add(new ListBoxItem());
            //}
        }


        

        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PdfViewer));
        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        protected void onClick()
        {
            RoutedEventArgs args = new RoutedEventArgs(ClickEvent, this);
            RaiseEvent(args);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            onClick();
        }


        #region Bindable Properties

        public string PdfPath
        {
            get { return (string)GetValue(PdfPathProperty); }
            set { SetValue(PdfPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PdfPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PdfPathProperty = DependencyProperty.Register("PdfPath", typeof(string), typeof(PdfViewer), new PropertyMetadata(null, propertyChangedCallback: OnPdfPathChanged));

        private static void OnPdfPathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pdfDrawer = (PdfViewer)d;

            if (!string.IsNullOrEmpty(pdfDrawer.PdfPath))
            {
                //making sure it's an absolute path
                var path = System.IO.Path.GetFullPath(pdfDrawer.PdfPath);


                StorageFile.GetFileFromPathAsync(path).AsTask()
                //load pdf document on background thread
                .ContinueWith(t => PdfDocument.LoadFromFileAsync(t.Result).AsTask()).Unwrap()
                //display on UI Thread
                .ContinueWith(t2 => PdfToImages(pdfDrawer, t2.Result), TaskScheduler.FromCurrentSynchronizationContext());
            }
        }
        #endregion


        private async static Task PdfToImages(PdfViewer pdfViewer, PdfDocument pdfDoc)
        {
            var items = pdfViewer.PagesContainer.Items;

            //Small update
            var comboboxitem = pdfViewer.Pages.Items;

            items.Clear();

            if (pdfDoc == null) return;

            for (uint i = 0; i < pdfDoc.PageCount; i++)
            {
                using (var page = pdfDoc.GetPage(i))
                {
                    var bitmap = await PageToBitmapAsync(page);
                    var image = new Image
                    {
                        Source = bitmap,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Margin = new Thickness(0, 4, 0, 4),
                        MaxWidth = 800
                    };
                    items.Add(image);
                    //Small update
                    comboboxitem.Add(i + 1 + "/" + pdfDoc.PageCount);
                }
            }
        }

        private static async Task<BitmapImage> PageToBitmapAsync(PdfPage page)
        {
            BitmapImage image = new BitmapImage();

            using (var stream = new InMemoryRandomAccessStream())
            {
                await page.RenderToStreamAsync(stream);
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream.AsStream();
                image.EndInit();
            }
            return image;
        }

        private void PagesContainer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UIElement element = e.Source as UIElement;
            MessageBox.Show(e.GetPosition(element).ToString());

            MessageBox.Show(element.RenderSize.ToString());

        }

        private void Btntest_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Paused");
        }

        private void jumpToPage(int n)
        {
            try
            {
                Image image = PagesContainer.Items.GetItemAt(n - 1) as Image;
                image.BringIntoView();
            }
            catch (System.ArgumentOutOfRangeException)
            {
                if (n == 0)
                    return;
                MessageBox.Show("Please wait...");
            }
        }

        private void JumptoPage(object sender, SelectionChangedEventArgs e)
        {
            jumpToPage(Pages.SelectedIndex);
        }

        protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
        {
            //Xây dựng ma trận transform dựa trên vị trí của chuột
            if (Keyboard.IsKeyDown(Key.RightCtrl) || Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                var element = PagesContainer as UIElement;
                var position = e.GetPosition(element);

                var transform = element.RenderTransform as MatrixTransform;
                var matrix = transform.Matrix;
                var scale = e.Delta >= 0 ? 1.1 : (1.0 / 1.1); // 
                zoomvalue *= scale;
                slider.Value = Math.Log10(zoomvalue);

                matrix.ScaleAtPrepend(scale, scale, position.X, position.Y);

                element.RenderTransform = new MatrixTransform(matrix);
                e.Handled = true;
            }
            else
            {
                base.OnPreviewMouseWheel(e);
            }
        }

        private void SliderZoom_ValueChange(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var element = PagesContainer as UIElement;
            var trasnform = element.RenderTransform as MatrixTransform;
            var matrix = trasnform.Matrix;
            var scale = Math.Pow(10, e.NewValue) / Math.Pow(10, e.OldValue);

            matrix.Scale(scale, scale);
            element.RenderTransform = new MatrixTransform(matrix);

            e.Handled = true;
        }
        
        void nothing()
        {
            
        }
    }
}
