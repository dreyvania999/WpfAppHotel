using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WpfAppHotel
{
    public partial class Hotel
    {
        public string AllInform
        {
            get
            {
                try
                {
                    return "Название: " + Name + "\nКоличество звезд: " + CountOfStars + "\nСтрана: " + Country.Name + "\nОписание: " + Description;

                }
                catch (System.Exception)
                {
                    return "Не удалось загрузить информацию";
                    throw;
                }
            }
        }
    }
}
