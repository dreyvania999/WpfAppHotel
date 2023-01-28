using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppHotel
{
    internal class Pagination
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private static readonly int Countitems = 5;
        public int[] NPage { get; set; } = new int[Countitems];
        public string[] Visible { get; set; } = new string[Countitems];
        public string[] Gray { get; set; } = new string[Countitems];

        private int Countpages; //Количество страниц
        public int CountPages
        {
            get => Countpages;
            set
            {
                Countpages = value;
                for (int i = 1; i < Countitems; i++)
                {
                    Visible[i] = CountPages <= i ? "Hidden" : "Visible";
                }
            }
        }

        private int Countpage; //Количество записей на странице
        public int CountPage
        {
            get => Countpage;
            set
            {
                Countpage = value;
                CountPages = CountList % value == 0 ? CountList / value : (CountList / value) + 1;
            }
        }

        private int Countlist; //Количество записей в списке
        public int CountList
        {
            get => Countlist;
            set
            {
                Countlist = value;
                CountPages = value % CountPage == 0 ? value / CountPage : 1 + (value / CountPage);
            }
        }
        private int Сurrentpage; //Текущая страница
        public int CurrentPage
        {
            get => Сurrentpage;
            set
            {
                Сurrentpage = value;
                if (Сurrentpage < 1)
                {
                    Сurrentpage = 1;
                }
                if (Сurrentpage >= CountPages)
                {
                    Сurrentpage = CountPages;
                }

                for (int i = 0; i < Countitems; i++)
                {
                    if (Сurrentpage < (1 + (Countitems / 2)) || CountPages < Countitems)
                    {
                        NPage[i] = i + 1; //Eсли страница в начале списка
                    }
                    else if (Сurrentpage > CountPages - ((Countitems / 2) + 1))
                    {
                        NPage[i] = CountPages - (Countitems - 1) + i; //Eсли страница в конце списка
                    }
                    else
                    {
                        NPage[i] = Сurrentpage + i - (Countitems / 2); //Eсли страница в середине списка
                    }
                }

                for (int i = 0; i < Countitems; i++)
                {
                    Gray[i] = NPage[i] == Сurrentpage ? "Gray" : "Black";
                }

                PropertyChanged(this, new PropertyChangedEventArgs("NPage"));
                PropertyChanged(this, new PropertyChangedEventArgs("Visible"));
                PropertyChanged(this, new PropertyChangedEventArgs("Gray"));
            }
        }

        public Pagination()
        {
            for (int i = 0; i < Countitems; i++)
            {
                if (i == 0)
                {
                    Visible[i] = "Visible";
                    Gray[i] = "Gray";
                }
                else
                {
                    Visible[i] = "Hidden";
                    Gray[i] = "Black";
                }

                NPage[i] = i + 1;
            }

            Сurrentpage = 1;
        }
    }
}
