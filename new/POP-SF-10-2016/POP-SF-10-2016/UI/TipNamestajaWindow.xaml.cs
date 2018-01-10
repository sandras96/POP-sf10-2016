using POP_10.Model;
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
    /// Interaction logic for TipNamestajaWindow.xaml
    /// </summary>
    public partial class TipNamestajaWindow : Window
    {
        ICollectionView view;
        public enum Status
        {
            OBRISAN,
            NEOBRISAN
        };
        public TipNamestajaWindow()
        {

            InitializeComponent();

            view = CollectionViewSource.GetDefaultView(Projekat.Instance.tipNam);
            view.Filter = FilterNeobrisan;

            dgTipNamestaja.ItemsSource = view;
            dgTipNamestaja.IsSynchronizedWithCurrentItem = true;
            dgTipNamestaja.DataContext = this;
            cbStatus.Items.Add(Status.NEOBRISAN);
            cbStatus.Items.Add(Status.OBRISAN);
            cbStatus.SelectedIndex = 0;
        }

        
    private bool FilterNeobrisan(object obj)
    {
        return ((TipNamestaja)obj).Obrisan == false;
    }

    private bool FilterObrisan(object obj)
    {
        return ((TipNamestaja)obj).Obrisan == true;
    }

    private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            var noviTipNamestaja = new TipNamestaja()
            {
                Naziv = ""
            };

            var tndi = new TipNamestajaDodavanjeIzmena(TipNamestajaDodavanjeIzmena.Operacija.DODAVANJE, noviTipNamestaja);
            tndi.ShowDialog();
            view.Refresh();
        }

       

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            TipNamestaja selektovaniTipNamestaja = (TipNamestaja)dgTipNamestaja.SelectedItem;
            TipNamestajaDodavanjeIzmena tndi = new TipNamestajaDodavanjeIzmena(TipNamestajaDodavanjeIzmena.Operacija.IZMENA, selektovaniTipNamestaja);
            tndi.ShowDialog();
            view.Refresh();
        }

        private void Obrisi_Click(object sender, RoutedEventArgs e)
        {
            TipNamestaja selektovaniTipNamestaja = (TipNamestaja)dgTipNamestaja.SelectedItem;
            selektovaniTipNamestaja.Obrisan = true;

            if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete izabrani namestaj: {selektovaniTipNamestaja.Naziv}?", "Poruka o brisanju ", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                selektovaniTipNamestaja.Obrisan = true;
            }
            view.Refresh();
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

        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(dgTipNamestaja.ItemsSource);

            if (cbSort.SelectedIndex == 0)
            {
                dgTipNamestaja.Items.SortDescriptions.Clear();
                dgTipNamestaja.Items.SortDescriptions.Add(new SortDescription("Naziv", ListSortDirection.Ascending));
            }
        }
    }
}
