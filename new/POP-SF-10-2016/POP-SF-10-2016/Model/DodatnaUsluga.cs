using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_10.Model
{
    
    public class DodatnaUsluga : INotifyPropertyChanged

    {   
        
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }

        }

        private string naziv;

        public string Naziv
        {
            get { return naziv; }
            set { naziv = value; OnPropertyChanged("Naziv"); }
        }

        private double cena;

        public double Cena
        {
            get { return cena; }
            set { cena = value; OnPropertyChanged("Cena"); }
        }

        private bool obrisan;

        public bool Obrisan
        {
            get { return obrisan; }
            set { obrisan = value; }
        }

        public object Clone()
        {
            return new DodatnaUsluga()
            {
                Id = id,
                Naziv = naziv,
                Cena = cena,
                Obrisan = obrisan

            };
        }
        
        public static DodatnaUsluga GetById(int Id)
        {
            foreach (var usluga in Projekat.Instance.dodatnaUsluga)
            {
                if(usluga.Id == Id)
                {
                    return usluga;
                }
            }
            return null;
        }

       /* public override string ToString()
        {
            return $"{Naziv},{Cena}";
        }*/



        public event PropertyChangedEventHandler PropertyChanged;
        
        protected void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
