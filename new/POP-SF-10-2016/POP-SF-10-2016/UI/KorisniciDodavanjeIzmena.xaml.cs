using POP_10.Model;
using POP_10.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace POP_SF_10_2016.UI
{
    /// <summary>
    /// Interaction logic for KorisniciDodavanjeIzmena.xaml
    /// </summary>
    public partial class KorisniciDodavanjeIzmena : Window
    {

        private Korisnik korisnik;
        private Operacija operacija;
        

        public enum Operacija
        {
            DODAVANJE,
            IZMENA,
        };


        public KorisniciDodavanjeIzmena(Operacija operacija, Korisnik korisnik)
        {
            InitializeComponent();
            this.korisnik = korisnik;
            this.operacija = operacija;

            tbIme.DataContext = korisnik;
            tbPrezime.DataContext = korisnik;
            tbKorisnickoIme.DataContext = korisnik;
            tbLozinka.DataContext = korisnik;
            cbTipKorisnika.DataContext = korisnik;
            cbTipKorisnika.ItemsSource = Enum.GetValues(typeof(TipKorisnika)).Cast<TipKorisnika>();

        }

       

        private void Izlaz(object sender, RoutedEventArgs w)
        {
            this.Close();
        }
        private void Sacuvaj(object sender, RoutedEventArgs e)
        {
            var listaKorisnika = Projekat.Instance.korisnik;

            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    korisnik.Id = listaKorisnika.Count + 1;
                    korisnik = Korisnik.Create(korisnik);
                   // listaKorisnika.Add(korisnik);
                    break;

                case Operacija.IZMENA:
                    foreach (var kor in listaKorisnika)
                    {
                        if (kor.Id == korisnik.Id)
                        { 
                            kor.Ime = korisnik.Ime;
                            kor.Prezime = korisnik.Prezime;
                            kor.KorisnickoIme = korisnik.KorisnickoIme;
                            kor.Lozinka = korisnik.Lozinka;
                            kor.TipKorisnika = korisnik.TipKorisnika;
                            Korisnik.Update(kor);
                            break;
                        }

                    }
                    break;
            }
           // GenericsSerializer.Serialize("korisnik.xml", listaKorisnika);
            this.Close();
        }
    }
}
