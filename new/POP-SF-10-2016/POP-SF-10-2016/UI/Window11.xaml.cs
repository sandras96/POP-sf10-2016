using POP_10.Model;
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
    /// Interaction logic for Window11.xaml
    /// </summary>
    public partial class Window11 : Window
    {
        public Window11()
        {
            InitializeComponent();

           /* TipNamestaja novododatiTip = TipNamestaja.Create(new TipNamestaja()
            {
                Naziv = "Cekam ID",
                Obrisan - false
            }); --Provera da li radi baza*/


            if(Projekat.Instance.ulogovanKorisnik.TipKorisnika.ToString() == "Prodavac")
            {
                btnKorisnici.Visibility = Visibility.Hidden;
                btnNamestaj.Visibility = Visibility.Hidden;
                btnTipNamestaja.Visibility = Visibility.Hidden;
                btnAkcija.Visibility = Visibility.Hidden;
                btnDodatnaUsluga.Visibility = Visibility.Hidden;
                btnSalon.Visibility = Visibility.Hidden;
            

            }
            if(Projekat.Instance.ulogovanKorisnik.TipKorisnika.ToString() == "Administrator")
            {
                btnProdajaNamestaja.Visibility = Visibility.Hidden;
            }
        }

        private void btnNamestaj_Click(object sender, RoutedEventArgs e)
        {
            NamestajWindow nw = new NamestajWindow();
            nw.ShowDialog();
        }
        
        private void btnKorisnici_Click(object sender, RoutedEventArgs e)
        {
            KorisniciWindow kw = new KorisniciWindow();
            kw.ShowDialog();
        }
        
       
       /*private void btnAkcija(object sender, RoutedEventArgs e)
        {
            AkcijaWindow aw = new AkcijaWindow();
            aw.ShowDialog();
        }*/

        private void btnTipNamestaja_Click(object sender, RoutedEventArgs e)
        {
            TipNamestajaWindow tnw = new TipNamestajaWindow();
            tnw.ShowDialog();
        }
        
        private void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnProdajaNamestaja_Click(object sender, RoutedEventArgs e)
        {
            ProdajaNamestajaWindow pnw = new ProdajaNamestajaWindow();
            pnw.ShowDialog();

        }

        private void btnDodatnaUsluga_Click(object sender, RoutedEventArgs e)
        {
            DodatnaUslugaWindow duw = new DodatnaUslugaWindow();
            duw.ShowDialog();
             
        }

        private void btnAkcija_Click(object sender, RoutedEventArgs e)
        {
            AkcijaWindow aw = new AkcijaWindow();
            aw.ShowDialog();
        }

        private void btnSalon_Click(object sender, RoutedEventArgs e)
        {
            SalonWindow sw = new SalonWindow(Projekat.Instance.salon);
            sw.ShowDialog();
        }
    }
}
