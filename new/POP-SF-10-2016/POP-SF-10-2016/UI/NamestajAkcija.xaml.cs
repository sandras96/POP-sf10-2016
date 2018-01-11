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
    /// Interaction logic for NamestajAkcija.xaml
    /// </summary>
    public partial class NamestajAkcija : Window
    {
        public Namestaj SelektovanNamestaj { get; set; }
        public Akcija selektovanaAkcija { get; set; }

        private ICollectionView view;
        public NamestajAkcija(Akcija akcija)
        {
            InitializeComponent();
            this.selektovanaAkcija = akcija;
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.namestaj);
            dgNamestaj.ItemsSource = view;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            dgNamestaj.DataContext = this;
           
            
        }

        /*private ObservableCollection<Namestaj> listaNamestaja()
        {
            ObservableCollection<Namestaj> retVal = new ObservableCollection<Namestaj>();


            foreach (var namestaj in Projekat.Instance.namestaj) 
            {
                foreach (var namestajAkcija in this.selektovanaAkcija.NamestajNaAkciji)
                {
                    if(namestaj.Id != namestajAkcija.Id)
                    {
                        retVal.Add(namestaj);
                        break;
                    }
                }
            }

            return retVal;
        }*/

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            if ((dgNamestaj.SelectedItem != null) && (dgNamestaj.SelectedItem is Namestaj))
            {

                SelektovanNamestaj = dgNamestaj.SelectedItem as Namestaj;
                                                
            }
            this.Close();
        }

        private void btnPonisti_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}

