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
    /// Interaction logic for AkcijaDodavanjeIzmena.xaml
    /// </summary>
    public partial class AkcijaDodavanjeIzmena : Window
    {
        public enum Operacija {
            DODAVANJE,
            IZMENA
        };

        private Operacija operacija;
        private Akcija akcija; 

        public AkcijaDodavanjeIzmena(Akcija akcija, Operacija operacija)
        {
            InitializeComponent();

            this.akcija = akcija;
            this.operacija = operacija;

            dgNamestajNaAkciji.ItemsSource = akcija.NamestajNaAkcijiId;

            tbPopust.DataContext = akcija;
            dpPocetak.DataContext = akcija;
            dpKraj.DataContext = akcija;
            dgNamestajNaAkciji.DataContext = akcija;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            var listaAkcija = Projekat.Instance.akcija;

            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    akcija.Id = listaAkcija.Count + 1;
                    listaAkcija.Add(akcija);
                    break;

                case Operacija.IZMENA:
                    foreach (var a in listaAkcija)
                    {
                        if( a.Id == akcija.Id)
                        {
                            a.Pocetak = akcija.Pocetak;
                            a.Kraj = akcija.Kraj;
                            a.Popust = akcija.Popust;
                            a.NamestajNaAkcijiId = akcija.NamestajNaAkcijiId;
                        }

                    }


                    break;
            }
            GenericsSerializer.Serialize("akcija.xml", listaAkcija);
            this.Close();

        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnPonisti_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
