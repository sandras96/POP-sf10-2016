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
        public Namestaj selektovaniNamestaj { get; set; }
        public enum Status
        {
            OBRISAN,
            NEOBRISAN
        };



        public NamestajWindow()
        {
           InitializeComponent();

            view = CollectionViewSource.GetDefaultView(Projekat.Instance.namestaj);
            view.Filter = FilterNeobrisan;

            dgNamestaj.ItemsSource = view;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            dgNamestaj.DataContext = this;
            cbStatus.Items.Add(Status.NEOBRISAN);
            cbStatus.Items.Add(Status.OBRISAN);
            cbStatus.SelectedIndex = 0;
        }
        private bool FilterNeobrisan(object obj)
        {
            return ((Namestaj)obj).Obrisan == false;
        }

        private bool FilterObrisan(object obj)
        {
            return ((Namestaj)obj).Obrisan == true;
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
            view.Refresh();
        }

        public void Izmena(object sender, RoutedEventArgs e)
        {
            Namestaj selektovaniNamestaj = (Namestaj)dgNamestaj.SelectedItem;
            var ndi = new NamestajDodavanjeIzmena(selektovaniNamestaj, NamestajDodavanjeIzmena.Operacija.IZMENA);
            ndi.ShowDialog();
            view.Refresh();

        }
        private void Brisanje(object sender, RoutedEventArgs e)
        {
            var staraListaN = Projekat.Instance.namestaj;
            var nam = (Namestaj)dgNamestaj.SelectedItem;

            if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete izabrani namestaj: {nam.Naziv}?", "Poruka o brisanju ", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (var n in staraListaN)
                {
                    if (n.Id == nam.Id)
                    {
                        n.Obrisan = true;
                        
                        break;
                    }
                
                
            }
         /*   public void Brisanje(object sender, RoutedEventArgs e)
        {
            Namestaj selektovaniNamestaj = (Namestaj)dgNamestaj.SelectedItem;

            if (MessageBox.Show(messageBoxText: $"Da li ste sigurni da zelite da obrisete{selektovaniNamestaj.Naziv}?", caption: "Brisanje", button: MessageBoxButton.YesNo, icon: MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                
                foreach (var nam in Projekat.Instance.namestaj)
                {
                    if (nam.Id == selektovaniNamestaj.Id)
                    {
                        nam.Obrisan = true;
                       // view.Filter = Filter;
                        break;
                    }
                }*/
             GenericsSerializer.Serialize("namestaj.xml", Projekat.Instance.namestaj);
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
    }
    
    }

