using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;

namespace Album_Photo
{
    class ControlClass : INotifyPropertyChanged
    {
        private AlbumPhoto myAlbum = new AlbumPhoto();

        private ICommand NewPageModel1CommandAtt { get; set; }

        private ICommand NewPageModel2CommandAtt { get; set; }

        private ICommand NewPageModel3CommandAtt { get; set; }

        private ICommand NewPageModel4CommandAtt { get; set; }

        private ICommand DeleteCommandAtt { get; set; }

        private ICommand GoBackCommandAtt { get; set; }

        private ICommand GoForwardCommandAtt { get; set; }

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

       
          public ControlClass()
        {
            NewPageModel1Command = new RelayCommand(NewPageModel1, param => true);
            NewPageModel2Command = new RelayCommand(NewPageModel2, param => true);
            NewPageModel3Command = new RelayCommand(NewPageModel3, param => true);
            NewPageModel4Command = new RelayCommand(NewPageModel4, param => true);
            DeleteCommand = new RelayCommand(Delete, param => (myAlbum.current_pos != null));
            GoBackCommand = new RelayCommand(GoBack, param => (myAlbum.current_pos != null && myAlbum.current_pos.Previous != null));
            GoForwardCommand = new RelayCommand(GoForward, param => (myAlbum.current_pos != null && myAlbum.current_pos.Next != null));
        }

         
        private void AddPage(Pages.GenericPage newPage)
        {
            //change model
            myAlbum.CreerPage(newPage); 

            //change UI
            if (PropertyChanged != null) 
            {
                //PropertyChanged(this, new PropertyChangedEventArgs("Tax"));
            }
        }

        private void NewPageModel1(object obj)
        {
            //change model
            myAlbum.CreerPage(new Pages.Models.PageModel1());
        }

        private void NewPageModel2(object obj)
        {
            //change model
            myAlbum.CreerPage(new Pages.Models.PageModel2());
        }

        private void NewPageModel3(object obj)
        {
            //change model
            myAlbum.CreerPage(new Pages.Models.PageModel3());
        }

        private void NewPageModel4(object obj)
        {
            //change model
            myAlbum.CreerPage(new Pages.Models.PageModel4());
        }

        private void Delete(object obj)
        {
            //change model
            myAlbum.DeletePage();

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

        public event PropertyChangedEventHandler PropertyChanged;

    }

   
    
}
