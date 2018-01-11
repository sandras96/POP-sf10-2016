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
        ICollectionView view;
        public Korisnik selektovaniKorisnik { get; set; }
        public enum Status
        {
            OBRISAN,
            NEOBRISAN
        };


        public KorisniciWindow()
        {
            InitializeComponent();
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.korisnik);
            view.Filter = FilterNeobrisan;
            dgKorisnik.IsSynchronizedWithCurrentItem = true;
            dgKorisnik.DataContext = this;
            dgKorisnik.ItemsSource = view;
            cbStatus.Items.Add(Status.NEOBRISAN);
            cbStatus.Items.Add(Status.OBRISAN);
            cbStatus.SelectedIndex = 0;
        }

        private bool FilterNeobrisan(object obj)
        {
            return ((Korisnik)obj).Obrisan == false;
        }

        private bool FilterObrisan(object obj)
        {
            return ((Korisnik)obj).Obrisan == true;
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
            view.Refresh();

        }
        private void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Izmeni(object sender, RoutedEventArgs e)
        {
            Korisnik selektovaniKorisnik = (Korisnik)dgKorisnik.SelectedItem;
            var copy = (Korisnik)selektovaniKorisnik.Clone;
            var kdi = new KorisniciDodavanjeIzmena(KorisniciDodavanjeIzmena.Operacija.IZMENA, copy);
            kdi.Show();
            view.Refresh();
            
        }
        private void Obrisi(object sender, RoutedEventArgs e)
        {
            Korisnik selektovaniKorisnik = (Korisnik)dgKorisnik.SelectedItem;
            var listaKorisnika = Projekat.Instance.korisnik;
           // var k= (Korisnik)dgKorisnik.SelectedItem;
            if (MessageBox.Show($"Da li ste sigurni da zelite da obrisete{selektovaniKorisnik.Ime} {selektovaniKorisnik.Prezime}?", "Brisanje", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (var k in listaKorisnika)
                {
                    if (k.Id == selektovaniKorisnik.Id)
                    {
                        k.Obrisan = true;

                        break;
                    }
                }
                GenericsSerializer.Serialize("korisnik.xml", listaKorisnika);
                view.Refresh();
            }
        }

        private void cbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Status status = (Status)cbStatus.SelectedItem;
            if(status == Status.OBRISAN)
            {
                view.Filter = FilterObrisan;
            }
            else
            {
                view.Filter = FilterNeobrisan;
            }
        }

        private void btnSort_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(dgKorisnik.ItemsSource);

            if (cbSort.SelectedIndex == 0)
            {
                dgKorisnik.Items.SortDescriptions.Clear();
                dgKorisnik.Items.SortDescriptions.Add(new SortDescription("Ime", ListSortDirection.Ascending));
            }
            else if (cbSort.SelectedIndex == 1)
            {
                dgKorisnik.Items.SortDescriptions.Clear();
                dgKorisnik.Items.SortDescriptions.Add(new SortDescription("Prezime", ListSortDirection.Ascending));
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var param = txtSearch.Text;

            dgKorisnik.ItemsSource = Korisnik.GetSearch(param);
        }
    }
}

