using iText.Kernel.Pdf;
using iText.Kernel.Utils; //Merger


namespace WPF_PDFDocument
{
    class PDFAction
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PdfSourcePath">Đường dẫn của file Pdf chứa trang cần insert vào</param>
        /// <param name="PdfDesPath">Đường dẫn của file pdf sẽ chèn trang vào</param>
        /// <param name="offset">Chèn vào sau trang của vị trí này</param>
        /// <param name="begin">trang đầu tiên chèn</param>
        /// <param name="end">trang cuối cùng chèn</param>
        public static int number = 0;
        public static void InsertPageFromPdf(string PdfSourcePath, string PdfDesPath, System.Collections.Generic.List<int> ListPage, int offset)
        {
            //PDF Merger
            string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "Merged" + number++ + ".pdf");
            PdfDocument pdfMergered = new PdfDocument(new PdfWriter(path));
            PdfMerger pdfMerger = new PdfMerger(pdfMergered);

            //Source and Des
            PdfDocument source = new PdfDocument(new PdfReader(PdfSourcePath));
            PdfDocument des = new PdfDocument(new PdfReader(PdfDesPath));

            //Check offset
            if(offset==0)
            {
                for (int i = 0; i < ListPage.Count; i++)
                {
                    pdfMerger.Merge(source, ListPage[i] + 1, ListPage[i] + 1);
                }

                if (offset + 1 <= des.GetNumberOfPages())
                {
                    pdfMerger.Merge(des, offset + 2, des.GetNumberOfPages());
                }

                source.Close();
                des.Close();
                pdfMergered.Close();
                return;
            }

            pdfMerger.Merge(des, 1, offset);

            for (int i = 0; i < ListPage.Count; i++)
            {
                pdfMerger.Merge(source, ListPage[i] + 1, ListPage[i] + 1);
            }

            if (offset + 1 <= des.GetNumberOfPages())
            {
                pdfMerger.Merge(des, offset + 2, des.GetNumberOfPages());
            }

            source.Close();
            des.Close();
            pdfMergered.Close();
        }
    }
}
