using POP_10.Model;
using POP_10.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for NamestajDodavanjeIzmena.xaml
    /// </summary>
    public partial class NamestajDodavanjeIzmena : Window
    {
        private Namestaj namestaj;
        private Operacija operacija;

        public enum Operacija
        {
            DODAVANJE,
            IZMENA,
        };


        public NamestajDodavanjeIzmena(Namestaj namestaj, Operacija operacija)
        {
            InitializeComponent();
            this.namestaj =  namestaj;
            this.operacija = operacija;
            cbTipNID.ItemsSource = filterActive(Projekat.Instance.tipNam);
            cbTipNID.SelectedIndex = 0;

            tbNaziv.DataContext = namestaj;
            tbCena.DataContext = namestaj;
            tbKolicina.DataContext = namestaj;
            cbTipNID.DataContext = namestaj;
        }

        private ObservableCollection<TipNamestaja> filterActive(ObservableCollection<TipNamestaja> lista)
        {
            ObservableCollection<TipNamestaja> filteredList = new ObservableCollection<TipNamestaja>();
            foreach(TipNamestaja tipNamestaja in lista)
            {
                if(tipNamestaja.Obrisan == false)
                {
                    filteredList.Add(tipNamestaja);
                }
            }
            return filteredList;
        }

        private void Ponisti(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            var postojeciNamestaj = Projekat.Instance.namestaj;

            switch (operacija)
            {

                case Operacija.DODAVANJE:

                    var Id = postojeciNamestaj.Count + 1;
                    namestaj.Id = Id;
                    namestaj = Namestaj.Create(namestaj);
                   // postojeciNamestaj.Add(namestaj);
                    break;

                case Operacija.IZMENA:
                    foreach (var n in postojeciNamestaj)
                    {
                        if (n.Id == namestaj.Id)
                        {
                            n.Naziv = namestaj.Naziv;
                            n.Kolicina = namestaj.Kolicina;
                            n.JedinicnaCena = namestaj.JedinicnaCena;
                            n.TipNID = namestaj.TipNID;
                            n.TipNamestaja = namestaj.TipNamestaja;
                            Namestaj.Update(n);
                            break;
                        }
                    }
                    break;
                }
            

            //GenericsSerializer.Serialize("namestaj.xml", postojeciNamestaj);
            this.Close();
        }

    }
}
