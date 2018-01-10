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
    /// Interaction logic for ProdajaNamestajaWindow.xaml
    /// </summary>
    public partial class ProdajaNamestajaWindow : Window
    {
        ICollectionView view;

        public ProdajaNamestaja selektovanaProdaja { get; set; }
        public enum Status
        {
            OBRISAN,
            NEOBRISAN
        };


        public ProdajaNamestajaWindow()
        {
            InitializeComponent();
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.prodajaNamestaja);
            view.Filter = FilterNeobrisan;


            dgProdajaNamestaja.IsSynchronizedWithCurrentItem = true;
            dgProdajaNamestaja.DataContext = this;
            dgProdajaNamestaja.ItemsSource = view;
            cbStatus.Items.Add(Status.NEOBRISAN);
            cbStatus.Items.Add(Status.OBRISAN);
            cbStatus.SelectedIndex = 0;


        }

        private bool FilterNeobrisan(object obj)
        {
            return ((ProdajaNamestaja)obj).Obrisan == false;
        }

        private bool FilterObrisan(object obj)
        {
            return ((ProdajaNamestaja)obj).Obrisan == true;
        }



        private void Dodavanje_Click(object sender, RoutedEventArgs e)
        {
            var novaProdaja = new ProdajaNamestaja()
            {
                DatumProdaje = DateTime.Today,
                BrojRacuna = "",
                Kupac = "",


                NamestajZaProdaju = new ObservableCollection<Namestaj>(),
            };

            ProdajaDodavanjeIzmena pdi = new ProdajaDodavanjeIzmena(novaProdaja, ProdajaDodavanjeIzmena.Operacija.DODAVANJE);
            pdi.ShowDialog();
            view.Refresh();

        }

        private void Izmena_Click(object sender, RoutedEventArgs e)
        {
            ProdajaNamestaja selektovanaProdaja = (ProdajaNamestaja)dgProdajaNamestaja.SelectedItem;
           
             var pdi = new ProdajaDodavanjeIzmena(selektovanaProdaja, ProdajaDodavanjeIzmena.Operacija.IZMENA);
             pdi.ShowDialog();
             view.Refresh();


        }

        private void Ponisti_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Brisanje_Click(object sender, RoutedEventArgs e)
        {
            var staraLista = Projekat.Instance.prodajaNamestaja;
            var prod = (ProdajaNamestaja)dgProdajaNamestaja.SelectedItem;

            if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete izabranu prodaju?", "Poruka o brisanju ", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (var p in staraLista)
                {
                    if (p.Id == prod.Id)
                    {
                        p.Obrisan = true;

                        break;
                    }

                }
                view.Refresh();
            }
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
    } }
    
    
