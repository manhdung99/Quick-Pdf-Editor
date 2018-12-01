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

namespace WPF_PDFDocument.Controls
{
    public partial class PdfViewer : UserControl
    {
        //Fields
        private double width, k;
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
            slider.ValueChanged += Slider_ValueChanged;
            zoomvalue = 1;

            //int nopage = PagesContainer.Items.Count;
            //for (int i = 0; i < nopage; i++)
            //{
            //    ComboBoxItem item = new ComboBoxItem();

            //    Pages.Items.Add(new ListBoxItem());
            //}
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.zoomvalue = e.NewValue;
            PagesContainer.LayoutTransform = new ScaleTransform(zoomvalue, zoomvalue);
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
        public static readonly DependencyProperty PdfPathProperty =
        DependencyProperty.Register("PdfPath", typeof(string), typeof(PdfViewer), new PropertyMetadata(null, propertyChangedCallback: OnPdfPathChanged));

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

            //working
            Image image = e.Source as Image;
            //MessageBox.Show(image.DesiredSize.Height +"--" + image.DesiredSize.Width);
            //working
            
            MessageBox.Show(image.ActualHeight * k + "|" + image.ActualWidth * k);
        }


        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            if (Keyboard.IsKeyDown(Key.RightCtrl) || Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                zoomvalue += 1.0 * e.Delta / 800;
                PagesContainer.LayoutTransform = new ScaleTransform(zoomvalue, zoomvalue);
            }
        }

        private void PagesContainer_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.RightCtrl) || Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                zoomvalue += 1.0 * e.Delta / 800;
                PagesContainer.LayoutTransform = new ScaleTransform(zoomvalue, zoomvalue);
                width = PagesContainer.DesiredSize.Width;
            }
        }

    }
}
