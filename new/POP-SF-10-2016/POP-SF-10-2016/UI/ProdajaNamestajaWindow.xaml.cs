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
       

        public ProdajaNamestajaWindow()
        {
            InitializeComponent();
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.prodajaNamestaja);
       
            dgProdajaNamestaja.IsSynchronizedWithCurrentItem = true;
            dgProdajaNamestaja.DataContext = this;
            dgProdajaNamestaja.ItemsSource = view;

          
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
           // ProdajaNamestaja selektovanaProdaja = (ProdajaNamestaja)dgProdajaNamestaja.SelectedItem;
            //var copy = (ProdajaNamestaja)selektovanaProdaja.Clone;
          //  var pdi = new ProdajaDodavanjeIzmena(copy, ProdajaDodavanjeIzmena.Operacija.IZMENA);
          //  pdi.Show();
          //  view.Refresh();


        }

        private void Ponisti_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Brisanje_Click(object sender, RoutedEventArgs e)
        {
            
        }

      
    }
}
