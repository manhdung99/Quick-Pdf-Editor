using iText.Kernel.Pdf;
using iText.Kernel.Utils; //Merger


namespace WPF_PDFDocument
{
    class PDFAction
    {
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
            if (offset == 0)
            {
                for (int i = 0; i < ListPage.Count; i++)
                {
                    pdfMerger.Merge(source, ListPage[i] + 1, ListPage[i] + 1);
                }

                pdfMerger.Merge(des, 1, des.GetNumberOfPages());
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

        public static string InsertPageFromPdf(string PdfSourcePath, System.Collections.Generic.List<int> ListPage)
        {
            //PDF Merger
            string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "Merged" + number++ + ".pdf");
            PdfDocument pdfMergered = new PdfDocument(new PdfWriter(path));
            PdfMerger pdfMerger = new PdfMerger(pdfMergered);

            //Source and Des
            PdfDocument source = new PdfDocument(new PdfReader(PdfSourcePath));

            //Check offset
            for (int i = 0; i < ListPage.Count; i++)
            {
                pdfMerger.Merge(source, ListPage[i] + 1, ListPage[i] + 1);
            }
            source.Close();
            pdfMergered.Close();
            return path;
        }




        public static string DeletePage(string Path, int from, int to)
        {
            //PDF Merger
            if (!System.IO.Directory.Exists(System.IO.Path.Combine(System.IO.Path.GetTempPath(), "DeletePage")))
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.Combine(System.IO.Path.GetTempPath(), "DeletePage"));
            }

            string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "DeletePage", "Merged" + number++ + ".pdf");


            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            PdfDocument pdfMergered = new PdfDocument(new PdfWriter(path));
            PdfMerger pdfMerger = new PdfMerger(pdfMergered);

            //Source and Des
            PdfDocument source = new PdfDocument(new PdfReader(Path));
            for (int i = 1; i < from; i++)
            {
                pdfMerger.Merge(source, i, i);
            }

            for (int i = to + 1; i <= source.GetNumberOfPages(); i++)
            {
                pdfMerger.Merge(source, i, i);
            }

            source.Close();
            pdfMerger.Close();
            pdfMergered.Close();
            return path;
        }

        public static string MergePdf(System.Collections.Generic.List<string> Paths)
        {
            if (!System.IO.Directory.Exists(System.IO.Path.Combine(System.IO.Path.GetTempPath(), "MergePdf")))
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.Combine(System.IO.Path.GetTempPath(), "MergePdf"));
            }

            string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "MergePdf", "Merged" + number++ + ".pdf");


            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            PdfDocument pdfMergered = new PdfDocument(new PdfWriter(path));
            PdfMerger pdfMerger = new PdfMerger(pdfMergered);

            for (int i = 0; i < Paths.Count; i++)
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(Paths[i]);
                if (fileInfo.Extension != ".pdf")
                    continue;

                PdfDocument source = new PdfDocument(new PdfReader(Paths[i]));
                pdfMerger.Merge(source, 1, source.GetNumberOfPages());
                source.Close();
            }

            pdfMerger.Close();
            return path;
        }
    }
}
