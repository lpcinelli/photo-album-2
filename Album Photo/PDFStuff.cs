#define DRAWING


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
    
using System.Windows.Documents.Serialization;
using System.Windows.Markup;

using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;



//using DocumentFormat.OpenXml.Wordprocessing;
//using DocumentFormat.OpenXml.Packaging;
//using DocumentFormat.OpenXml.Wordprocessing;

namespace Album_Photo
{
    class PDFStuff
    {
        //static public DocumentViewer DocumentViewer1;

        static public void CreatePDF(AlbumPhoto thisAlbum)
        {
            // create an iTextSharp document
            Document doc = new Document(PageSize.LETTER, 0f, 0f, 0f, 0f);
            iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new FileStream("myFile.pdf", FileMode.Create));
            doc.Open();

           // FixedDocument myFixedDocument = CreateFixedDocument(thisAlbum);

            // cycle through each page of the WPF FixedDocument
            //DocumentPaginator paginator = myFixedDocument.DocumentPaginator;
            //for (int i = 0; i < paginator.PageCount; i++)
            for (int i = 0; i < thisAlbum.albumSize; i++)
            {
                // render the fixed document to a WPF Visual object
                //Visual visual = paginator.GetPage(i).Visual;
          
                Visual visual = (Visual) thisAlbum.GetPageAt(i);


                // create a temporary file for the bitmap image
                string targetFile = Path.GetTempFileName();

                // convert XPS file to an image
                using (FileStream outStream = new FileStream(targetFile, FileMode.Create))
                {
                    PngBitmapEncoder enc = new PngBitmapEncoder();
                    enc.Frames.Add(BitmapFrame.Create(CreateBitmapFromVisual(visual, 300, 300, thisAlbum.GetPageAt(i).ActualHeight, thisAlbum.GetPageAt(i).ActualWidth)));
                    enc.Save(outStream);    
                }

                //("~/images/HKVictoriaHarbour.png"
                iTextSharp.text.Image background = iTextSharp.text.Image.GetInstance("../../Images/brown-antique-paper.jpg");
                background.Alignment = iTextSharp.text.Image.UNDERLYING;

                // add the image to the iTextSharp PDF document
                using (FileStream fs = new FileStream(targetFile, FileMode.Open))
                {
                    iTextSharp.text.Image png = GetInstance(System.Drawing.Image.FromStream(fs), System.Drawing.Imaging.ImageFormat.Png);

                    png.ScalePercent(24f);
                    png.Alignment = iTextSharp.text.Image.ALIGN_CENTER;

                    doc.NewPage();
                    doc.Add(background);
                    doc.Add(png);
                }
            }
            doc.Close();
        }

        //using iTextSharp.text.Image.GetInstance(System.Drawing.Image image, System.Drawing.Imaging.ImageFormat format)
        //directly was not working - recopied the original function
        static private iTextSharp.text.Image GetInstance(System.Drawing.Image image, System.Drawing.Imaging.ImageFormat format)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, format);
            return iTextSharp.text.Image.GetInstance(ms.ToArray());
        }

        static public FixedDocument CreateFixedDocument(AlbumPhoto thisAlbum)
        {
            // create a FixedDocument and set the size
            FixedDocument fixedDocument = new FixedDocument();
            fixedDocument.DocumentPaginator.PageSize = new System.Windows.Size(96 * 8.5, 96 * 11);

            PageContent pageContent = new PageContent();
            FixedPage fixedPage = new FixedPage();

            //add each album page
            for(int i=0; i< thisAlbum.albumSize; i++)
            {
                Console.WriteLine(i);
                fixedPage.Children.Add(thisAlbum.GetPageAt(i));
                fixedDocument.Pages.Add(pageContent);

                ((IAddChild)pageContent).AddChild(fixedPage);
            }
                    
            return fixedDocument;
        }

        public static BitmapSource CreateBitmapFromVisual(Visual target, Double dpiX, Double dpiY, Double height, Double width)
        {
            if (target == null)
            {
                return null;
            }

           // Rect bounds = VisualTreeHelper.GetContentBounds(target);
            System.Windows.Size bounds = new  System.Windows.Size(width,height);

            ;

            RenderTargetBitmap rtb = new RenderTargetBitmap((Int32)(width * dpiX / 96.0),
                                                                                    (Int32)(height * dpiY / 96.0),
                                                                                    dpiX,
                                                                                    dpiY,
                                                                                    PixelFormats.Pbgra32);

            DrawingVisual dv = new DrawingVisual();
            using (DrawingContext dc = dv.RenderOpen())
            {
                VisualBrush vb = new VisualBrush(target);
                dc.DrawRectangle(vb, null, new Rect(new System.Windows.Point(), bounds));
            }
             
            rtb.Render(dv);

            return rtb;
        }


        //static public void CreateMyWPFControlReport(ControlClass usefulData)
        //{
        //    //Set up the WPF Control to be printed
        //    Pages.GenericPage controlToPrint;
        //    controlToPrint = new Pages.GenericPage();
        //    controlToPrint.DataContext = usefulData;

        //    FixedDocument fixedDoc = new FixedDocument();
        //    PageContent pageContent = new PageContent();
        //    FixedPage fixedPage = new FixedPage();

        //    //Create first page of document

        //    fixedPage.Children.Add(controlToPrint);
        //    ((System.Windows.Markup.IAddChild)pageContent).AddChild(fixedPage);
        //    fixedDoc.Pages.Add(pageContent);
        //    //Create any other required pages here

        //    //View the document
        //    DocumentViewer1.Document = fixedDoc;

        //            MemoryStream lMemoryStream = new MemoryStream();
        //Package package = Package.Open(lMemoryStream, FileMode.Create);
        //XpsDocument doc = new XpsDocument(package);
        //XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);
        //writer.Write(dp);
        //doc.Close();
        //package.Close();

        //var pdfXpsDoc = PdfSharp.Xps.XpsModel.XpsDocument.Open(lMemoryStream);
        //PdfSharp.Xps.XpsConverter.Convert(pdfXpsDoc, d.FileName, 0);
        //}

        //public void SaveCurrentDocument()
        //{
        //    // Configure save file dialog box
        //    Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
        //    dlg.FileName = "MyReport"; // Default file name
        //    dlg.DefaultExt = ".xps"; // Default file extension
        //    dlg.Filter = "XPS Documents (.xps)|*.xps"; // Filter files by extension

        //    // Show save file dialog box
        //    Nullable<bool> result = dlg.ShowDialog();

        //    // Process save file dialog box results
        //    if (result == true)
        //    {
        //        // Save document
        //        string filename = dlg.FileName;

        //        PdfDocument doc = new PdfDocument();
        //        doc.LoadFromFile(xpsFile, FileFormat.XPS);

        //        FixedDocument doc = (FixedDocument)DocumentViewer1.Document;
        //        XpsDocument xpsd = new XpsDocument(filename, FileAccess.ReadWrite);
        //        XpsDocumentWriter xw = XpsDocument.CreateXpsDocumentWriter(xpsd);
        //        xw.Write(doc);
        //        xpsd.Close();
        //    }
        //}

        //MemoryStream lMemoryStream = new MemoryStream();
        //Package package = Package.Open(lMemoryStream, FileMode.Create);
        //XpsDocument doc = new XpsDocument(package);
        //XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);
        //writer.Write(dp);
        //doc.Close();
        //package.Close();

        //var pdfXpsDoc = PdfSharp.Xps.XpsModel.XpsDocument.Open(lMemoryStream);
        //PdfSharp.Xps.XpsConverter.Convert(pdfXpsDoc, d.FileName, 0);


        //MemoryStream lMemoryStream = new MemoryStream();
        //Package package = Package.Open(lMemoryStream, FileMode.Create);
        //XpsDocument doc = new XpsDocument(package);
        //XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);
        //writer.Write(dp);
        //doc.Close();
        //package.Close();

        //var pdfXpsDoc = PdfSharp.Xps.XpsModel.XpsDocument.Open(lMemoryStream);
        //PdfSharp.Xps.XpsConverter.Convert(pdfXpsDoc, d.FileName, 0);

    }
}
