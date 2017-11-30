using POP_10.Model;
using POP_10.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for KorisniciWindow.xaml
    /// </summary>
    public partial class KorisniciWindow : Window
    {

        public KorisniciWindow()
        {
            InitializeComponent();

            dgKorisnik.IsSynchronizedWithCurrentItem = true;
            dgKorisnik.DataContext = this;
            dgKorisnik.ItemsSource = Projekat.Instance.korisnik;
        }

        private void Dodaj(object sender, RoutedEventArgs e)
        {
            var noviKorisnik = new Korisnik()
            {
                Ime = "",
                Prezime = "",
                KorisnickoIme = "",
                Lozinka = "",
                TipKorisnika = TipKorisnika.Administrator
            };
            var kdi = new KorisniciDodavanjeIzmena(KorisniciDodavanjeIzmena.Operacija.DODAVANJE, noviKorisnik);
            kdi.ShowDialog();

        }
        private void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Izmeni(object sender, RoutedEventArgs e)
        {
            var selektovaniKorisnik = (Korisnik)dgKorisnik.SelectedItem;
            var kdi = new KorisniciDodavanjeIzmena(KorisniciDodavanjeIzmena.Operacija.IZMENA, selektovaniKorisnik);
            kdi.Show();

        }
        private void Obrisi(object sender, RoutedEventArgs e)
        {

            var listaKorisnika = Projekat.Instance.korisnik;
            var k= (Korisnik)dgKorisnik.SelectedItem;
            if (MessageBox.Show($"Da li ste sigurni da zelite da obrisete{k.Ime} {k.Prezime}?", "Brisanje", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (var k1 in listaKorisnika)
                {
                    if (k1.Id == k.Id)
                    {
                        //kor.Obrisan = true;

                        break;
                    }
                }
                GenericsSerializer.Serialize("korisnik.xml", listaKorisnika);
            }
        }
    }
}

