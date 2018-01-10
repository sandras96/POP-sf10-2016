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
    public class ProdajaNamestaja : INotifyPropertyChanged
    {

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        private DateTime datumProdaje;

        public DateTime DatumProdaje
        {
            get { return datumProdaje; }
            set { datumProdaje = value; OnPropertyChanged("DatumProdaje"); }
        }

        private string brojRacuna;

        public string BrojRacuna
        {
            get { return brojRacuna; }
            set { brojRacuna = value; OnPropertyChanged("BrojRacuna"); }
        }


        private string kupac;

        public string Kupac
        {
            get { return kupac; }
            set { kupac = value; OnPropertyChanged("Kupac"); }
        }

        private double ukupnaCena;

        public double UkupnaCena
        {
            get { return ukupnaCena; }
            set { ukupnaCena = value; OnPropertyChanged("UkupnaCena"); }
        }
        private bool obrisan;

        public bool Obrisan
        {
            get { return obrisan; }
            set { obrisan = value; OnPropertyChanged("Obrisan"); }
        }

        public static ProdajaNamestaja GetById(int Id)
        {
            foreach (var prodaja in Projekat.Instance.prodajaNamestaja)
            {
                if(prodaja.Id == Id)
                {
                    return prodaja;
                }
            }
            return null;
        }



        public const double PDV = 0.02;

        

        /*public DodatnaUsluga DodatnaUsluga
        {
            get { return dodatnaUsluga; }
            set { dodatnaUsluga = value; OnPropertyChanged("DodatnaUsluga"); }
        }*/

        public ObservableCollection<Namestaj> NamestajZaProdaju { get; set; }
        public ObservableCollection<int> DodatnaUslugaId { get; set; }

        /*public int Id { get; set; }
        public int BrojKomadaNamestaja { get; set; }
        public DateTime DatumProdaje { get; set; }
        public string BrojRacuna { get; set; }
        public string Kupac { get; set; }
        public List<DodatnaUsluga> DodatneUsluge { get; set; }
        public const double PDV = 0.02;
        public double UkupnaCena { get; set; }*/

        protected void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void izracunajCenu()
        {
            foreach(Namestaj namestaj in this.NamestajZaProdaju)
            {
                this.ukupnaCena += (namestaj.JedinicnaCena + namestaj.JedinicnaCena * PDV) * namestaj.Kolicina;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public object Clone()
        {
            return new ProdajaNamestaja()
            {
                Id = id,
                DatumProdaje = datumProdaje,
                BrojRacuna = brojRacuna,
                Kupac = kupac,
                UkupnaCena = ukupnaCena,
                NamestajZaProdaju = new ObservableCollection<Namestaj>(NamestajZaProdaju),
                DodatnaUslugaId = new ObservableCollection<int>(DodatnaUslugaId)
                
            };
        }
    }
}
