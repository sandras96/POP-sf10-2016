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
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace POP_SF_10_2016.UI
{
    /// <summary>
    /// Interaction logic for NamestajProdaja.xaml
    /// </summary>
    public partial class NamestajProdaja : Window
    {
        public Namestaj SelektovanNamestaj { get; set; }

        public Namestaj ProdajNamestaj { get; set; }


        private ICollectionView view;

        public NamestajProdaja()
        {
            InitializeComponent();
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.namestaj);
            dgNamestaj.ItemsSource = view;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            dgNamestaj.DataContext = this;
            this.DataContext = ProdajNamestaj;
        }

        private void Ponisti_Click(object sender, RoutedEventArgs e)
        {
            ProdajNamestaj = null;
            this.Close();
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
          // var Namestaj1 = (Namestaj)dgNamestaj.SelectedItem;


           if ((dgNamestaj.SelectedItem != null) && (dgNamestaj.SelectedItem is Namestaj)) 
           {
                int kolicina = Convert.ToInt32(cmbKolicina.SelectedItem);
                SelektovanNamestaj = dgNamestaj.SelectedItem as Namestaj;
                ProdajNamestaj = SelektovanNamestaj.Clone() as Namestaj;
                ProdajNamestaj.Kolicina = kolicina;
                SelektovanNamestaj.Kolicina -= kolicina;

            }
                this.Close();

            
        }

        private void dgNamestaj_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelektovanNamestaj = dgNamestaj.SelectedItem as Namestaj;
            cmbKolicina.Items.Clear();
            for (int i = 1; i <= SelektovanNamestaj.Kolicina; i++)
            {
                
                cmbKolicina.Items.Add(i);
            }
            cmbKolicina.SelectedIndex = 0;
        }
    }
}
