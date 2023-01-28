using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfAppHotel
{
    /// <summary>
    /// Логика взаимодействия для WindowAddHotel.xaml
    /// </summary>
    public partial class WindowAddHotel : Window
    {
        private Hotel HT;
        private readonly bool FlagCreate;
        public WindowAddHotel()
        {
            InitializeComponent();

            cbCountry.ItemsSource = DataBase.DB.Country.ToList();
            cbCountry.SelectedValuePath = "Code";
            cbCountry.DisplayMemberPath = "Name";

            FlagCreate = true;
        }
        public WindowAddHotel(Hotel hotel)
        {
            InitializeComponent();

            btnAdd.Content = "Изменить";
            tbHeader.Text = "ИЗМЕНЕНИЕ ОТЕЛЯ";

            cbCountry.ItemsSource = DataBase.DB.Country.ToList();
            cbCountry.SelectedValuePath = "Code";
            cbCountry.DisplayMemberPath = "Name";

            HT = hotel;
            FlagCreate = false;

            tboxName.Text = hotel.Name;
            tboxStars.Text = hotel.CountOfStars.ToString();
            cbCountry.SelectedValue = hotel.CountryCode;
            tboxDescription.Text = hotel.Description;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (CheckData())
            {
                if (FlagCreate)
                {
                    HT = new Hotel();
                }

                HT.Name = tboxName.Text;
                HT.CountOfStars = Convert.ToInt32(tboxStars.Text);
                HT.CountryCode = cbCountry.SelectedValue.ToString();
                HT.Description = tboxDescription.Text;

                if (FlagCreate)
                {
                    _ = DataBase.DB.Hotel.Add(HT);
                }
                _ = DataBase.DB.SaveChanges();

                _ = FlagCreate ? MessageBox.Show("Отель успешно добавлен") : MessageBox.Show("Отель успешно изменён");

                Close();
            }
        }

        private bool CheckData()
        {
            if (tboxName.Text.Length == 0)
            {
                _ = MessageBox.Show("Поле \"Название\" не должно быть пустым");
                return false;
            }
            else if (!Regex.IsMatch(tboxStars.Text, @"^[0-5]$"))
            {
                _ = MessageBox.Show("Количество звёзд может быть от 0 до 5!");
                return false;
            }
            else if (cbCountry.SelectedIndex == -1)
            {
                _ = MessageBox.Show("Выберите страну");
                return false;
            }
            else if (tboxDescription.Text.Length == 0)
            {
                _ = MessageBox.Show("Поле \"Описание\" не должно быть пустым");
                return false;
            }

            return true;
        }
    }
}
