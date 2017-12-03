using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_10.Model
{
    [Serializable]
    public enum TipKorisnika {

        Administrator,
        Prodavac

    }

    public class Korisnik : INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }
        private string ime;

        public string Ime
        {
            get { return ime; }
            set { ime = value; OnPropertyChanged("Ime"); }
        }

        private string prezime;

        public string Prezime
        {
            get { return prezime; }
            set { prezime = value; OnPropertyChanged("Prezime"); }
        }

        private string korisnickoIme;

        public string KorisnickoIme
        {
            get { return korisnickoIme; }
            set { korisnickoIme = value; OnPropertyChanged("KorisnickoIme"); }
        }

        private string lozinka;

        public string Lozinka
        {
            get { return lozinka; }
            set { lozinka = value; OnPropertyChanged("Lozinka"); }
        }

        private TipKorisnika tipKorisnka;

        public TipKorisnika TipKorisnika
        {
            get { return tipKorisnka; }
            set { tipKorisnka = value; OnPropertyChanged("TipKorisnika"); }
        }

        private bool obrisan;

        public bool Obrisan
        {
            get { return obrisan; }
            set { obrisan = value; OnPropertyChanged("Obrisan"); }
        }









        /* public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public TipKorisnika TipKorisnika { get; set; }
        public bool Obrisan { get; set; }*/

        public event PropertyChangedEventHandler PropertyChanged;

        public object Clone => new Korisnik()
        {
            Id = id,
            Ime = ime,
            Prezime = prezime,
            KorisnickoIme = korisnickoIme,
            Lozinka = lozinka,
            TipKorisnika = tipKorisnka,
            Obrisan = obrisan
        };
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
