using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace POP_10.Model
{
    [Serializable]
    public class Namestaj : INotifyPropertyChanged
    {
       // public ObservableCollection<Namestaj> Namestaji { get; set; }

        private int id;

        public int Id
        {
            get { return id; }
            set {
                id = value; OnPropertyChanged("Id");}
        }

        private string naziv;

        public string Naziv
        {
            get { return naziv; }
            set { naziv = value; OnPropertyChanged("Naziv"); }
        }
     

        private int jedinicnaCena;

        public int JedinicnaCena
        {
            get { return jedinicnaCena; }
            set { jedinicnaCena = value; OnPropertyChanged("JedinicnaCena"); }
        }

        private int kolicina;

        public int Kolicina
        {
            get { return kolicina; }
            set { kolicina = value; OnPropertyChanged("Kolicina"); }
        }

        private bool obrisan;

        public bool Obrisan
        {
            get { return obrisan; }
            set { obrisan = value; OnPropertyChanged("Obrisan"); }
        }

        private TipNamestaja tipNamestaja;
        [XmlIgnore]
        public TipNamestaja TipNamestaja
        {
            get
            { if (tipNamestaja == null)
                {
                    tipNamestaja = TipNamestaja.GetById(tipNID);
                }
                return tipNamestaja;
            }
            set {
                tipNamestaja = value;
                tipNID = tipNamestaja.Id;
                OnPropertyChanged("TipNamsetaja"); }
        }

        private int tipNID;

        public int TipNID
        {
            get { return tipNID; }
            set { tipNID = value; OnPropertyChanged("TipNID"); }
        }

        


        public int akcija;
     


        /*
        public override string ToString()
        {
            return $"Naziv: {Naziv},{JedinicnaCena},{TipNamestaja.GetById(TipNID).Naziv}";
        }*/

        protected void OnPropertyChanged  (string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public object Clone()
        {
            return new Namestaj()
            {
                Id = id,
                Naziv = naziv,
                JedinicnaCena = jedinicnaCena,
                TipNamestaja = tipNamestaja,
                TipNID = tipNID,
                Kolicina = kolicina,
                Obrisan = obrisan
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;


        public static Namestaj GetById(int id)
        {
            foreach (var namestaj in Projekat.Instance.namestaj)
            {
                if (namestaj.Id == id)
                {
                    return namestaj;
                }
            }
            return null;
        }
    }














    

}
