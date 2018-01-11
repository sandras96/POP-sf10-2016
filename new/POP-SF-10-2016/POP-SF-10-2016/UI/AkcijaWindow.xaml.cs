using POP_10.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AkcijaWindow.xaml
    /// </summary>
    public partial class AkcijaWindow : Window
    {
        ICollectionView view;
        public Akcija selektovanaAkcija { get; set; }
        public enum Status
        {
            OBRISAN,
            NEOBRISAN,
        };


        public AkcijaWindow()
        {
            InitializeComponent();

            view = CollectionViewSource.GetDefaultView(Projekat.Instance.akcija);
            view.Filter = FilterNeobrisan;

            dgAkcija.ItemsSource = view;
            dgAkcija.IsSynchronizedWithCurrentItem = true;
            dgAkcija.DataContext = this;
            cbStatus.Items.Add(Status.NEOBRISAN);
            cbStatus.Items.Add(Status.OBRISAN);
            cbStatus.SelectedIndex = 0;

        }

        private bool FilterNeobrisan(object obj)
        {
            return ((Akcija)obj).Obrisan == false;
        }

        private bool FilterObrisan(object obj)
        {
            return ((Akcija)obj).Obrisan == true;
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            var novaAkcija = new Akcija()
            {
                Popust = 0,
                Pocetak = DateTime.Today,
                Kraj = DateTime.Today,
                NamestajNaAkciji = new ObservableCollection<Namestaj>()
            };
            var adi = new AkcijaDodavanjeIzmena(novaAkcija, AkcijaDodavanjeIzmena.Operacija.DODAVANJE);
            adi.ShowDialog();
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            Akcija selektovanaAkcija = (Akcija)dgAkcija.SelectedItem;
            var adi = new AkcijaDodavanjeIzmena(selektovanaAkcija, AkcijaDodavanjeIzmena.Operacija.IZMENA);
            adi.ShowDialog();
            view.Refresh();

        }

        private void Obrisi_Click(object sender, RoutedEventArgs e)
        {
            var staraLista = Projekat.Instance.akcija;
            var ak = (Akcija)dgAkcija.SelectedItem;

            if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete izabranu akciju?", "Poruka o brisanju ", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (var a in staraLista)
                {
                    if (a.Id == ak.Id)
                    {
                        a.Obrisan = true;

                        break;
                    }
                }
                view.Refresh();
            }
        }
        private void Ponisti_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Status status = (Status)cbStatus.SelectedItem;
            if (status == Status.OBRISAN)
            {
                view.Filter = FilterObrisan;
            }
            else
            {
                view.Filter = FilterNeobrisan;
            }
        }
    }
}
