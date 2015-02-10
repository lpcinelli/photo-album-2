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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Xml.Serialization;

namespace Album_Photo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //AlbumPhoto myAlbum = new AlbumPhoto();
        }

        static AlbumPhoto DeserializeFromXML(string file)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(AlbumPhoto));
            TextReader textReader = new StreamReader(@file);
      
            AlbumPhoto myAlbum = (AlbumPhoto) deserializer.Deserialize(textReader);
            textReader.Close();

            return myAlbum;
        }

        static public void SerializeToXML(AlbumPhoto album, string file)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(AlbumPhoto));
            TextWriter textWriter = new StreamWriter(@file);
            serializer.Serialize(textWriter, album);
            textWriter.Close();
        }

        private void NewAlbum_Click(object sender, RoutedEventArgs e)
        {
            _mainContent.Navigate(new Pages.Models.PageModel1());
            ((AlbumPhoto)DataContext).N++;
            PageButton.IsEnabled = true;
        }

        private void OpenAlbum_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".alb"; // Default file extension
            dlg.Filter = "Album documents (.alb)|*.alb"; // Filter files by extension

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {   
                // Open document
                //((AlbumPhoto)DataContext) = DeserializeFromXML(@dlg.FileName);
               //  AlbumPhoto Album = new AlbumPhoto(new Uri(@dlg.FileName));
  
            }
        }

        private void SaveAlbum_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".xml"; // Default file extension
            dlg.Filter = "XML files (.xml)|*.xml"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                SerializeToXML(((AlbumPhoto)DataContext), @dlg.FileName);          
            }
        }

        private void NewPageModel1_Click(object sender, RoutedEventArgs e)
        {
            if (((AlbumPhoto)DataContext).N >= 0)    
                _mainContent.Navigate(new Pages.Models.PageModel1());
        }

        private void NewPageModel2_Click(object sender, RoutedEventArgs e)
        {
            if (((AlbumPhoto)DataContext).N >= 0)
                _mainContent.Navigate(new Pages.Models.PageModel2());
        }

        private void NewPageModel3_Click(object sender, RoutedEventArgs e)
        {
            if (((AlbumPhoto)DataContext).N >= 0)    
                _mainContent.Navigate(new Pages.Models.PageModel3()); 
          
        }

        private void NewPageModel4_Click(object sender, RoutedEventArgs e)
        {
            if (((AlbumPhoto)DataContext).N >= 0)    
                _mainContent.Navigate(new Pages.Models.PageModel4());
        }

        private void DeletePage_Click(object sender, RoutedEventArgs e)
        {
            if (((AlbumPhoto)DataContext).current_pos != null)
            {
                return;
            }

            _mainContent.GoBack();
            //((ICollection)_mainContent.ForwardStack());
            //_mainContent.CurrentSource;
            //_mainContent.Content = null;

           
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            //if (((AlbumPhoto)DataContext).current_pos.Previous != null)
            //{
            //    ((AlbumPhoto)DataContext).current_pos = ((AlbumPhoto)DataContext).current_pos.Previous;
            //  //  _mainContent.GoBack();
            //}

        }

        private void GoForward_Click(object sender, RoutedEventArgs e)
        {
            if (((AlbumPhoto)DataContext).current_pos == null)
                System.Console.WriteLine("current_pos = null");

            if ( ( (AlbumPhoto)  DataContext).current_pos.Next !=null )
            {
                ((AlbumPhoto)DataContext).current_pos = ((AlbumPhoto)DataContext).current_pos.Previous;
                //_mainContent.GoForward();
            }

        }

        private void ConfirmPage_Click(object sender, RoutedEventArgs e)
        {
            ((AlbumPhoto)DataContext).CreerPage((Pages.GenericPage)_mainContent.Content);         
        }  

    
    }
}
