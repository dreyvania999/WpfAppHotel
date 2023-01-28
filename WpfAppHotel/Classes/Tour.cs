using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfAppHotel
{
    public partial class Tour
    {
        public string TicetsCount => "Количество билетов: " + TicketCount;

        public string Costes => Price.ToString("N0", new CultureInfo("en-us")) + " руб.";
        public SolidColorBrush ColorTour
        {
            get
            {
                new SolidColorBrush(Color.FromRgb(111, 111, 111));
                SolidColorBrush propertyColor;
                if (IsActual == true)
                {
                    propertyColor = new SolidColorBrush(Color.FromRgb(186, 227, 232));
                    return propertyColor;
                }
                else
                {
                    propertyColor = new SolidColorBrush(Color.FromRgb(227, 30, 36));
                    return propertyColor;
                }
            }
        }
    }
}
