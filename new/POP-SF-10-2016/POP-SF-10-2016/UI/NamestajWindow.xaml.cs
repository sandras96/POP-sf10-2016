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
using static POP_SF_10_2016.UI.NamestajDodavanjeIzmena;

namespace POP_SF_10_2016.UI
{
    /// <summary>
    /// Interaction logic for NamestajWindow.xaml
    /// </summary>
    public partial class NamestajWindow : Window
    {
        ICollectionView view;

        public Namestaj SelektovaniNamestaj { get; set; }

        public NamestajWindow()
        {
            InitializeComponent();

            view = CollectionViewSource.GetDefaultView(Projekat.Instance.namestaj);
            view.Filter = Filter;

            dgNamestaj.ItemsSource = view;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            dgNamestaj.DataContext = this;
        }
        private bool Filter(object obj)
        {
            return ((Namestaj)obj).Obrisan == false;
        }

        public void Ponisti(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void Dodavanje(object sender, RoutedEventArgs e)
        {
            var noviNamestaj = new Namestaj()
            {
                Naziv = ""
            };
            var ndi = new NamestajDodavanjeIzmena(noviNamestaj, NamestajDodavanjeIzmena.Operacija.DODAVANJE);
            ndi.ShowDialog();
        }

        public void Izmena(object sender, RoutedEventArgs e)
        {
            var selektovaniNamestaj = (Namestaj)dgNamestaj.SelectedItem;
            var ndi = new NamestajDodavanjeIzmena(selektovaniNamestaj, NamestajDodavanjeIzmena.Operacija.IZMENA);
            ndi.ShowDialog();
           
        }

        public void Brisanje(object sender, RoutedEventArgs e)
        {
            var listaNamestaja = Projekat.Instance.namestaj;

            if (MessageBox.Show($"Da li ste sigurni da zelite da obrisete{SelektovaniNamestaj.Naziv}?", "Brisanje", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (var nam in listaNamestaja)
                {
                    if (nam.Id == SelektovaniNamestaj.Id)
                    {
                        nam.Obrisan = true;
                        view.Filter = Filter;
                        break;
                    }
                }
             GenericsSerializer.Serialize("namestaj.xml", Projekat.Instance.namestaj);

            }
        }

    }
    
    }

