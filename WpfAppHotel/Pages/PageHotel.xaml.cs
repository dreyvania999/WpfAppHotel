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
    /// Логика взаимодействия для PageHotel.xaml
    /// </summary>
    public partial class PageHotel : Page
    {
        private readonly Pagination Pagin = new Pagination();
        private List<Hotel> HotelList = new List<Hotel>();
        public PageHotel()
        {
            InitializeComponent();
            DataContext = Pagin;
            HotelList = DataBase.DB.Hotel.ToList();
            ListHotel.ItemsSource = HotelList;

            tboxPageCount.Text = "10";
        }

        private void EditingCurrentPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;

            switch (tb.Uid)
            {
                case "first":
                    Pagin.CurrentPage = 1;
                    break;
                case "last":
                    Pagin.CurrentPage = HotelList.Count;
                    break;
                case "back":
                    Pagin.CurrentPage--;
                    break;
                case "next":
                    Pagin.CurrentPage++;
                    break;
                default:
                    Pagin.CurrentPage = Convert.ToInt32(tb.Text);
                    break;
            }

            ListHotel.ItemsSource = HotelList.Skip((Pagin.CurrentPage * Pagin.CountPage) - Pagin.CountPage).Take(Pagin.CountPage).ToList();
        }


        private void tboxPageCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetPagination();
        }

        private void SetPagination()
        {
            try
            {
                if (Convert.ToInt32(tboxPageCount.Text) > 0)
                {
                    Pagin.CountPage = Convert.ToInt32(tboxPageCount.Text);
                }
            }
            catch
            {
                Pagin.CountPage = HotelList.Count;
            }

            Pagin.CountList = HotelList.Count;
            ListHotel.ItemsSource = HotelList.Skip(0).Take(Pagin.CountPage).ToList();



        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            _ = MainFrame.MFrame.Navigate(new PageMain());
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            WindowAddHotel addHotel = new WindowAddHotel();
            _ = addHotel.ShowDialog();
            HotelList = DataBase.DB.Hotel.ToList();
            SetPagination();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int id = Convert.ToInt32(button.Uid);
            Hotel hotel = DataBase.DB.Hotel.FirstOrDefault(x => x.Id == id);
            foreach (Tour item1 in DataBase.DB.Tour)
            {
                if (hotel.Tour == item1 && item1.IsActual == true)
                {
                    _ = MessageBox.Show("Вы пытаетесь удалить отель для актуального тура");
                    return;
                }
            }
            string nameHotel = hotel.Name;
            foreach (HotelImage item in DataBase.DB.HotelImage.Where(x => x.HotelId == id))
            {
                _ = DataBase.DB.HotelImage.Remove(item);
            }
            _ = DataBase.DB.Hotel.Remove(hotel);
            _ = DataBase.DB.SaveChanges();
            _ = MessageBox.Show("Был удален отель: " + nameHotel);
            HotelList = DataBase.DB.Hotel.ToList();
            ListHotel.ItemsSource = HotelList;
        }
    }
}
