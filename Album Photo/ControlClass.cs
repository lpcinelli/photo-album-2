using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Controls;
using System.IO;
using System.Xml.Serialization;

namespace Album_Photo
{
    class ControlClass : INotifyPropertyChanged
    {
    

        public event PropertyChangedEventHandler PropertyChanged;

        private AlbumPhoto myAlbum = new AlbumPhoto();

        private Pages.GenericPage currentPage { get; set; }

        private ICommand AddPageCommandAtt { get; set; }

        private ICommand NewPageModel1CommandAtt { get; set; }

        private ICommand NewPageModel2CommandAtt { get; set; }

        private ICommand NewPageModel3CommandAtt { get; set; }

        private ICommand NewPageModel4CommandAtt { get; set; }

        private ICommand DeleteCommandAtt { get; set; }

        private ICommand GoBackCommandAtt { get; set; }

        private ICommand GoForwardCommandAtt { get; set; }

        private ICommand NewAlbumCommandAtt { get; set; }

        private ICommand OpenAlbumCommandAtt { get; set; }

        private ICommand SaveAlbumCommandAtt { get; set; }

        public ICommand AddPageCommand 
        {
            get
            {
                return AddPageCommandAtt;
            }

            set
            {
                AddPageCommandAtt = value;
            }
        }

        public ICommand NewPageModel1Command
        {
            get
            {
                return NewPageModel1CommandAtt;
            }

            set
            {
                NewPageModel1CommandAtt = value;
            }
        }

        public ICommand NewPageModel2Command
        {
            get
            {
                return NewPageModel2CommandAtt;
            }

            set
            {
                NewPageModel2CommandAtt = value;
            }
        }

        public ICommand NewPageModel3Command
        {
            get
            {
                return NewPageModel3CommandAtt;
            }

            set
            {
                NewPageModel3CommandAtt = value;
            }
        }

        public ICommand NewPageModel4Command
        {
            get
            {
                return NewPageModel4CommandAtt;
            }

            set
            {
                NewPageModel4CommandAtt = value;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                return DeleteCommandAtt;
            }

            set
            {
                DeleteCommandAtt = value;
            }
        }

        public ICommand GoBackCommand
        {
            get
            {
                return GoBackCommandAtt;
            }
            set
            {
                GoBackCommandAtt = value;
            }
        }

        public ICommand GoForwardCommand
        {
            get
            {
                return GoForwardCommandAtt;
            }

            set
            {
                GoForwardCommandAtt = value;
            }
        }

        public ICommand NewAlbumCommand
        {
            get
            {
                return NewAlbumCommandAtt;
            }

            set
            {
                NewAlbumCommandAtt = value;
            }
        }

        public ICommand OpenAlbumCommand
        {
            get
            {
                return OpenAlbumCommandAtt;
            }

            set
            {
                OpenAlbumCommandAtt = value;
            }
        }

        public ICommand SaveAlbumCommand
        {
            get
            {
                return SaveAlbumCommandAtt;
                ;
            }

            set
            {
                SaveAlbumCommandAtt = value;
            }
        }

          public ControlClass()
        {
            currentPage = null;
            NewAlbumCommand = new RelayCommand(NewAlbum, param => true);
            OpenAlbumCommand = new RelayCommand(OpenAlbum, param => true);
            SaveAlbumCommand = new RelayCommand(SaveAlbum, param => myAlbum != null);
            AddPageCommand = new RelayCommand(AddPage, param => currentPage != null);
            NewPageModel1Command = new RelayCommand(NewPageModel1, param => true);
            NewPageModel2Command = new RelayCommand(NewPageModel2, param => true);
            NewPageModel3Command = new RelayCommand(NewPageModel3, param => true);
            NewPageModel4Command = new RelayCommand(NewPageModel4, param => true);
            DeleteCommand = new RelayCommand(Delete, param => (myAlbum.current_pos != null));
            GoBackCommand = new RelayCommand(GoBack, param => (myAlbum.current_pos != null && myAlbum.current_pos.Previous != null));
            GoForwardCommand = new RelayCommand(GoForward, param => (myAlbum.current_pos != null && myAlbum.current_pos.Next != null));
        }


        private void AddPage(object obj)
        {
            //change model
            myAlbum.CreerPage(currentPage);
            currentPage = null;
        }

        private void NewPageModel1(object obj)
        {
            currentPage = new Pages.Models.PageModel1();

            //change UI
           // ApplicationHelper.NavigationService.Navigate(currentPage);
            
        }

        private void NewPageModel2(object obj)
        {
            currentPage = new Pages.Models.PageModel2();
            
            //change UI
           // ApplicationHelper.NavigationService.Navigate(currentPage);
        }

        private void NewPageModel3(object obj)
        {
            currentPage = new Pages.Models.PageModel3();

            //change UI
            //ApplicationHelper.NavigationService.Navigate(currentPage);

        }

        private void NewPageModel4(object obj)
        {
            currentPage = new Pages.Models.PageModel4();

            //change UI
           // ApplicationHelper.NavigationService.Navigate(currentPage);

        }

        private void Delete(object obj)
        {
            //change model
            myAlbum.DeletePage();
            currentPage = null;

            //change UI
            if (PropertyChanged != null) // Point 2
            {
                //PropertyChanged(this, new PropertyChangedEventArgs("Tax"));
            }
        }

        private void GoForward(object obj)
        {
            
            //change model
            myAlbum.current_pos = myAlbum.current_pos.Next;

            //change UI
            if (PropertyChanged != null) // Point 2
            {
                //  PropertyChanged(this, new PropertyChangedEventArgs("Tax"));
            }
        }

        public void GoBack(object obj)
        {      
            //change model
            myAlbum.current_pos = myAlbum.current_pos.Previous;

            //change UI
            if (PropertyChanged != null) // Point 2
            {
                Console.WriteLine("funfou");
                //  PropertyChanged(this, new PropertyChangedEventArgs("Tax"));
            }

        }

        private void OpenAlbum(object obj)
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
                myAlbum = DeserializeFromXML(@dlg.FileName);
                
            }
        }
        
        private void SaveAlbum(object obj)
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
                SerializeToXML(myAlbum, @dlg.FileName);
            }
        }

        private void NewAlbum(object obj)
        {
            //_mainContent.Navigate(new Pages.Models.PageModel1());
           // ((AlbumPhoto)DataContext).N++;
            //PageButton.IsEnabled = true;
        }

        static AlbumPhoto DeserializeFromXML(string file)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(AlbumPhoto));
            TextReader textReader = new StreamReader(@file);

            AlbumPhoto myAlbum = (AlbumPhoto)deserializer.Deserialize(textReader);
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


    }

   
    
}
