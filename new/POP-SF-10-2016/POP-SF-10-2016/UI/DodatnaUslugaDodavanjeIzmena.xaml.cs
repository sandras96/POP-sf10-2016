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
    /// Interaction logic for DodatnaUslugaDodavanjeIzmena.xaml
    /// </summary>
    public partial class DodatnaUslugaDodavanjeIzmena : Window
    {
        private DodatnaUsluga dodatnaUsluga;
        private Operacija operacija;

        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };

        public DodatnaUslugaDodavanjeIzmena(DodatnaUsluga dodatnaUsluga, Operacija operacija)
        {

            InitializeComponent();

            this.dodatnaUsluga = dodatnaUsluga;
            this.operacija = operacija;

            tbNaziv.DataContext = dodatnaUsluga;
            tbCena.DataContext = dodatnaUsluga;
        }

        private void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            var postojecaUsluga = Projekat.Instance.dodatnaUsluga;
            switch (operacija)
            {
                case Operacija.DODAVANJE:


                    dodatnaUsluga.Id = postojecaUsluga.Count + 1;
                    postojecaUsluga.Add(dodatnaUsluga);

                    break;

                case Operacija.IZMENA:
                    foreach (var usluga in postojecaUsluga)
                    {
                        if(usluga.Id == dodatnaUsluga.Id)
                        {
                            usluga.Naziv = dodatnaUsluga.Naziv;
                            usluga.Cena = dodatnaUsluga.Cena;

                            break;
                        }

                    }

                    break;
            }
            GenericsSerializer.Serialize("dodatnaUsluga.xml", postojecaUsluga);
            this.Close();
        }

        private void btnPonisti_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
