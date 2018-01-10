using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_10.Model
{
    [Serializable]
    public class Akcija : INotifyPropertyChanged
    {
        /*public int Id { get; set; }
        public DateTime pocetak { get; set; }
        public decimal popust { get; set; }
        public DateTime kraj { get; set; }
        public bool Obrisan { get; set; }
        private ObservableCollection<int> namestajNaAkcijiId*/


        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        private DateTime pocetak;

        public DateTime Pocetak
        {
            get { return pocetak; }
            set { pocetak = value; OnPropertyChanged("Pocetak"); }
        }

        private DateTime kraj;

        public DateTime Kraj
        {
            get { return kraj; }
            set { kraj = value; OnPropertyChanged("Kraj"); }
        }

        private decimal popust;

        public decimal Popust
        {
            get { return popust; }
            set { popust = value; OnPropertyChanged("Popust");}
        }

        private bool obrisan;

        public bool Obrisan
        {
            get { return obrisan; }
            set { obrisan = value; OnPropertyChanged("Obrisan"); }
        }

       // private ObservableCollection<int> namestajNaAkcijiId;

        public ObservableCollection<int> NamestajNaAkcijiId { get; set; }
      /*  {
            get { return namestajNaAkcijiId;; }
            set { namestajNaAkcijiId = value; OnPropertyChanged("NamestajNaAkcijiId"); }
        }*/

        public override string ToString()
        {
            return $"{Pocetak.ToShortDateString()} - {Kraj.ToShortTimeString()}";
        }

        public static Akcija GetById(int Id)
        {
            foreach (var akcija in Projekat.Instance.akcija)
            {
                if(akcija.Id == Id)
                {
                    return akcija;
                }
            }
            return null;
        }
        public object Clone()
        {
            return new Akcija()
            {
                Id = id,
                Pocetak = pocetak,
                Kraj = kraj,
                Popust = popust,
                Obrisan = obrisan
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
