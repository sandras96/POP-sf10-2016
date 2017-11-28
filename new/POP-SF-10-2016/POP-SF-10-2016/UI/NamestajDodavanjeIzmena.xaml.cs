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


        public NamestajDodavanjeIzmena(Namestaj noviNamestaj, Operacija operacija)
        {
            InitializeComponent();
            /* this.namestaj = noviNamestaj;
             cbTipNamestaja.ItemsSource = Projekat.Instance.tipNam;
             this.operacija = operacija;
             tbNaziv.DataContext = namestaj;
             cbTipNamestaja.DataContext = namestaj;
             foreach (var tn in Projekat.Instance.tipNam)
             {
                 cbTipNamestaja.Items.Add(tn);
                 cbTipNamestaja.SelectedIndex = 0;
             }*/

            InitializeComponent();
            this.namestaj = noviNamestaj;
            cbTipNamestaja.ItemsSource = Projekat.Instance.tipNam;
            this.operacija = operacija;
            tbNaziv.DataContext = namestaj;
            cbTipNamestaja.DataContext = namestaj;
            foreach (var tn in Projekat.Instance.tipNam)
            {
                cbTipNamestaja.Items.Add(tn);
                cbTipNamestaja.SelectedIndex = 0;
            }
        }

        private void Ponisti(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void SacuvajNamestaj(object sender, RoutedEventArgs e)
        {
            var postojeciNamestaj = Projekat.Instance.namestaj;

            switch (operacija)
            {

                case Operacija.DODAVANJE:

                    var Id = postojeciNamestaj.Count + 1;
                    postojeciNamestaj.Add(namestaj);

                    break;
                case Operacija.IZMENA:
                    foreach (var n in postojeciNamestaj)
                    {
                        if (n.Id == namestaj.Id)
                        {
                            n.Naziv = namestaj.Naziv;
                            n.Kolicina = namestaj.Kolicina;
                            n.JedinicnaCena = namestaj.JedinicnaCena;
                            n.TipN = namestaj.TipN;
                            break;
                        }
                    }
                    break;
                }
            GenericsSerializer.Serialize("namestaj.xml", postojeciNamestaj);
            this.Close();
        }

        private static int NoviIDzaNamestaj()
        {
            int j = 0;
            foreach (var namestaj in Projekat.Instance.namestaj)
            {
                if (j <= namestaj.Id)
                    j = namestaj.Id;

            }
            return j + 1;
        }

    }
}
