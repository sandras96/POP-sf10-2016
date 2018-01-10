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
using static POP_SF_10_2016.UI.DodatnaUslugaDodavanjeIzmena;

namespace POP_SF_10_2016.UI
{
    /// <summary>
    /// Interaction logic for DodatnaUslugaWindow.xaml
    /// </summary>
    public partial class DodatnaUslugaWindow : Window
    {
        ICollectionView view;
        public DodatnaUsluga selektovanaUsluga { get; set; }
        public enum Status
        {
            OBRISAN,
            NEOBRISAN
        };


        public DodatnaUslugaWindow()
        {
            InitializeComponent();

            view = CollectionViewSource.GetDefaultView(Projekat.Instance.dodatnaUsluga);
            view.Filter = FilterNeobrisan;

            dgDodatanaUsluga.ItemsSource = view;
            dgDodatanaUsluga.IsSynchronizedWithCurrentItem = true;
            dgDodatanaUsluga.DataContext = this;
            cbStatus.Items.Add(Status.NEOBRISAN);
            cbStatus.Items.Add(Status.OBRISAN);
            cbStatus.SelectedIndex = 0;
        }

       private bool FilterNeobrisan(object obj)
        {
            return ((DodatnaUsluga)obj).Obrisan == false;
        }

        private bool FilterObrisan(object obj)
        {
            return ((DodatnaUsluga)obj).Obrisan == true;
        }

    
        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            var novaUsluga = new DodatnaUsluga()
            {
                Naziv = "",
                Cena = 0
            };

            var dudi = new DodatnaUslugaDodavanjeIzmena(novaUsluga, DodatnaUslugaDodavanjeIzmena.Operacija.DODAVANJE);
            dudi.ShowDialog();
            view.Refresh();
        }


        private void btnIzmena_Click(object sender, RoutedEventArgs e)
        {
            DodatnaUsluga selektovanaUsluga = (DodatnaUsluga)dgDodatanaUsluga.SelectedItem;

            var dudi = new DodatnaUslugaDodavanjeIzmena(selektovanaUsluga, DodatnaUslugaDodavanjeIzmena.Operacija.IZMENA);
            dudi.ShowDialog();
            view.Refresh();
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            var staraListaDU = Projekat.Instance.dodatnaUsluga;
            var du = (DodatnaUsluga)dgDodatanaUsluga.SelectedItem;

            if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete izabranu dodatnu uslugu: {du.Naziv}?", "Poruka o brisanju ", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (var usluga in staraListaDU)
                {
                    if (usluga.Id == du.Id)
                    {
                        usluga.Obrisan = true;

                        break;
                    }
                }

                GenericsSerializer.Serialize("dodatnaUsluga", Projekat.Instance.dodatnaUsluga);
                view.Refresh();
            }
        }
        private void btnPonisti_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       

        private void cbStatus_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
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

        private void btnSort_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(dgDodatanaUsluga.ItemsSource);

            if (cbSort.SelectedIndex == 0)
            {
                dgDodatanaUsluga.Items.SortDescriptions.Clear();
                dgDodatanaUsluga.Items.SortDescriptions.Add(new SortDescription("Naziv", ListSortDirection.Ascending));
            }
            else if (cbSort.SelectedIndex == 1)
            {
                dgDodatanaUsluga.Items.SortDescriptions.Clear();
                dgDodatanaUsluga.Items.SortDescriptions.Add(new SortDescription("Cena", ListSortDirection.Ascending));
            }
        }
    }

} 


