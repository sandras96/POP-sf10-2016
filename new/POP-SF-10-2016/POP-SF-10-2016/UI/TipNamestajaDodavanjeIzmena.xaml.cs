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
    /// Interaction logic for TipNamestajaDodavanjeIzmena.xaml
    /// </summary>
    public partial class TipNamestajaDodavanjeIzmena : Window
    {

        private TipNamestaja tipNamestaja;
        private Operacija operacija;


        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };


        public TipNamestajaDodavanjeIzmena(Operacija operacija, TipNamestaja tipNamestaja)
        {
            
            InitializeComponent();
            this.operacija = operacija;
            this.tipNamestaja = tipNamestaja;
            tbNaziv.DataContext = tipNamestaja;
            cbObrisan.DataContext = tipNamestaja;
            if(operacija == Operacija.DODAVANJE)
            {
                cbObrisan.Visibility = Visibility.Hidden;
            }
        }


        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            var postojeciTipNamestaja  = Projekat.Instance.tipNam;

            switch (operacija)
            {

                case Operacija.DODAVANJE:

                    var Id = postojeciTipNamestaja.Count + 1;
                    var obrisan = false;
                    tipNamestaja.Id = Id;
                    tipNamestaja.Obrisan = obrisan;
                    tipNamestaja = TipNamestaja.Create(tipNamestaja);
                   // postojeciTipNamestaja.Add(tipNamestaja);

                    break;

                case Operacija.IZMENA:
                    foreach (var n in postojeciTipNamestaja)
                    {
                        if (n.Id == tipNamestaja.Id)
                        {
                            n.Naziv = tipNamestaja.Naziv;
                            n.Obrisan = tipNamestaja.Obrisan;
                            TipNamestaja.Update(n);
                            break;
                        }
                    }
                    break;
            }
           // GenericsSerializer.Serialize("tipNamestaja.xml", postojeciTipNamestaja);
            this.Close();
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
