using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
//using System.Drawing.Drawing2D;
//using System.Drawing.Imaging;


namespace Album_Photo.Pages
{

   public class GenericPage : UserControl
    {   
         public GenericPage()
         {
         }

         //protected void ConfirmPage_Click(object sender, RoutedEventArgs e)
         //{
         //   Album.CreerPage(this);
         //   //Console.WriteLine(myAlbum.N);
         //}   

         private static BitmapImage resizeImage(BitmapImage imgToResize, int Width, int Height)
         {
             double sourceWidth = imgToResize.Width;
             double sourceHeight = imgToResize.Height;

             double nPercent = 0;
             double nPercentW = 0;
             double nPercentH = 0;

             nPercentW = (Width / sourceWidth);
             nPercentH = (Height / sourceHeight);

             if (nPercentH < nPercentW)
                 nPercent = nPercentH;
             else
                 nPercent = nPercentW;

             int destWidth = (int)(sourceWidth * nPercent);
             int destHeight = (int)(sourceHeight * nPercent);

             BitmapImage bi = new BitmapImage();
             bi.BeginInit();
             bi.DecodePixelHeight = destHeight;
             bi.DecodePixelWidth = destWidth;
             bi.UriSource = imgToResize.UriSource;
             bi.EndInit();
             return bi;
         }

         public void InsertNewImage(object sender, MouseButtonEventArgs e)
         {
             Image ClickedImage = (Image)sender;

             Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

             //dlg.FileName = "Document"; // Default file name 
             dlg.DefaultExt = ".txt"; // Default file extension 
             dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png"; // Filter files by extension 

             // Show open file dialog box 
             Nullable<bool> result = dlg.ShowDialog();

             System.Console.WriteLine(dlg);
             System.Console.WriteLine(result);
             if (result == true) 
             {
                 BitmapImage originalImage = new BitmapImage(new Uri(@dlg.FileName));
                 if (originalImage.Width > 369 || originalImage.Height > 190)
                 {
                     BitmapImage imageToDisplay = new BitmapImage();
                     imageToDisplay = resizeImage(originalImage, 369, 190);
                     Console.WriteLine("New Height:" + imageToDisplay.Height + "  New Width: " + imageToDisplay.Width);
                     ClickedImage.Source = imageToDisplay;
                 }
                 else ClickedImage.Source = originalImage;
            }
         }

    }
}
