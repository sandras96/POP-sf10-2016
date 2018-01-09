using System;
using System.Collections.Generic;
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
        public bool Obrisan { get; set; }*/


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
            set { Pocetak = value; OnPropertyChanged("Pocetak"); }
        }

        private DateTime kraj;

        public DateTime Kraj
        {
            get { return kraj; }
            set { Kraj = value; OnPropertyChanged("Kraj"); }
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
