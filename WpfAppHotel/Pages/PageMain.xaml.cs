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

namespace WpfAppHotel
{
    /// <summary>
    /// Логика взаимодействия для PageMain.xaml
    /// </summary>
    public partial class PageMain : Page
    {
        public PageMain()
        {
            InitializeComponent();
        }

        private void btnTour_Click(object sender, RoutedEventArgs e)
        {
            _ = MainFrame.MFrame.Navigate(new TourListView());
        }

        private void btnHotel_Click(object sender, RoutedEventArgs e)
        {
            _ = MainFrame.MFrame.Navigate(new PageHotel());
        }
    }
}
