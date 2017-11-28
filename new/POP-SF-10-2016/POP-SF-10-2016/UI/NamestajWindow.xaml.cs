using POP_10.Model;
using POP_10.Util;
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
using static POP_SF_10_2016.UI.NamestajDodavanjeIzmena;

namespace POP_SF_10_2016.UI
{
    /// <summary>
    /// Interaction logic for NamestajWindow.xaml
    /// </summary>
    public partial class NamestajWindow : Window
    {
        public NamestajWindow()
        {
            InitializeComponent();

            dgNamestaj.ItemsSource = Projekat.Instance.namestaj;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
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
            var namestajProzor = new NamestajDodavanjeIzmena(noviNamestaj, Operacija.DODAVANJE);
            namestajProzor.ShowDialog();
        }

        public void Izmena(object sender, RoutedEventArgs e)
        {
            var selektovaniNamestaj = (Namestaj)dgNamestaj.SelectedItem;
            var namestajProzor = new NamestajDodavanjeIzmena(selektovaniNamestaj, Operacija.IZMENA);
            namestajProzor.ShowDialog();
        }

        public void Brisanje(object sender, RoutedEventArgs e)
        {
            var staraListaN = Projekat.Instance.namestaj;
            var nam = (Namestaj)dgNamestaj.SelectedItem;

            if(MessageBox.Show($"Da li ste sigurni da zelite da obrisete {nam.Naziv}?", "Brisanje", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (var n in staraListaN)
                {
                    if(n.Id == nam.Id)
                    {
                        n.Obrisan = true;
                        break;
                    }
                }

            }
            GenericsSerializer.Serialize("namestaj.xml", Projekat.Instance.namestaj);
        }
    }
}
