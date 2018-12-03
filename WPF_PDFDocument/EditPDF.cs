using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

//Include Itext Namespace
using iText.Kernel.Pdf;
using iText.Layout.Layout;
using iText.Layout.Element;


namespace WPF_PDFDocument
{
    class EditPDF
    {
        private void TestPDF()
        {
            var writer = new PdfWriter("test.pdf");
            var pdf = new PdfDocument(writer);
            var document = new iText.Layout.Document(pdf);
            document.SetFontSize(20);
            document.Add(new Paragraph("Vì giờ anh biết chuyện tình mình chẳng còn gì."));
            document.Add(new Paragraph("Khi gió xuân sang người đừng ngọt lời thầm thì."));
            document.Add(new Paragraph("Anh bước sang ngang đợi chờ điều gì diệu kì"));
            document.Add(new Paragraph("Như lúc ban đầu"));
            document.Close();

        }
    }
}
