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

namespace Album_Photo.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : System.Windows.Controls.Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        //private void NewAlbumAction(object sender, RoutedEventArgs e)
        //{
        //    NavigationService nav = NavigationService.GetNavigationService(this);
        //    nav.Navigate(new Uri("Pages/Models/PageModel1.xaml", UriKind.RelativeOrAbsolute));
        //}
        //private void ManageAlbumAction(object sender, RoutedEventArgs e)
        //{
        //    NavigationService nav = NavigationService.GetNavigationService(this);
        //    nav.Navigate(new Uri("Pages/ManageAlbumPage.xaml", UriKind.RelativeOrAbsolute));
        //}
    }
}
