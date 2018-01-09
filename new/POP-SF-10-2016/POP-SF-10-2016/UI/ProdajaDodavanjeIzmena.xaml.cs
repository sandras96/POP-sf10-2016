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
    /// Interaction logic for ProdajaDodavanjeIzmena.xaml
    /// </summary>
    public partial class ProdajaDodavanjeIzmena : Window
    {

        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };

        private Operacija operacija;
        private ProdajaNamestaja prodajaNamestaja;

        public ProdajaDodavanjeIzmena(ProdajaNamestaja prodajaNamestaja, Operacija operacija)
        {
            InitializeComponent();

            this.prodajaNamestaja = prodajaNamestaja;
            this.operacija = operacija;

           // dgPNamestaj.DataContext = this;
          //  dgPNamestaj.IsSynchronizedWithCurrentItem = true;
            dgPNamestaj.ItemsSource = prodajaNamestaja.NamestajZaProdaju;

            dpDatumProdaje.DataContext = prodajaNamestaja;
            tbBrRacuna.DataContext = prodajaNamestaja;
            tbKupac.DataContext = prodajaNamestaja;
            cbUsluga.DataContext = prodajaNamestaja;

        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            var listaProdaje = Projekat.Instance.prodajaNamestaja;

            switch (operacija)
            {
                case Operacija.DODAVANJE:

                    prodajaNamestaja.Id = listaProdaje.Count + 1;
                    listaProdaje.Add(prodajaNamestaja);
                    break;

                case Operacija.IZMENA:

                    foreach (var prodaja in listaProdaje)
                    {
                        if (prodaja.Id == prodajaNamestaja.Id)
                        {
                            prodaja.DatumProdaje = prodajaNamestaja.DatumProdaje;
                            prodaja.BrojRacuna = prodajaNamestaja.BrojRacuna;
                            prodaja.Kupac = prodajaNamestaja.Kupac;
                            prodaja.NamestajZaProdaju = prodajaNamestaja.NamestajZaProdaju;
                            break;
                        }
                    }

                    break;


            }
            GenericsSerializer.Serialize("prodajaNamestaja.xml", listaProdaje);
            this.Close();
        }


        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            NamestajProdaja np = new NamestajProdaja();
            np.ShowDialog();

          

           np.Closed += np_Closed;
        }

       
        private void np_Closed(object sender, EventArgs e)
         {
            var add = sender as NamestajProdaja;
            prodajaNamestaja.NamestajZaProdaju.Add((add).Namestaj1);
         }
    }
}
