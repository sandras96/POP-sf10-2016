using POP_10.Model;
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

namespace POP_SF_9_2016.UI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        private Namestaj namestaj;
        private Operacija operacija;
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };
/*        public Window1()
        {
            InitializeComponent();
        }*/

        public Window1(Namestaj namestaj, Operacija operacija)  //= Operacija.DODAVANJE) //u slucaju da neko ne navede da li je dodavanje ili izmena, bice dodavanje
        {
            InitializeComponent();
            InicijalizujVrednost(namestaj, operacija);
        }

        private void InicijalizujVrednost(Namestaj namestaj, Operacija operacija)
        {
            this.namestaj = namestaj;
            this.operacija = operacija;

            tbNaziv.Text = namestaj.Naziv;

            foreach (var tipNamestaja in Projekat.Instance.TipoviNamestaja)
            
            {
                cbTipNamestaja.Items.Add(tipNamestaja);
            }

            foreach (TipNamestaja tipNamestaja in cbTipNamestaja.Items)
            {
                if(tipNamestaja.Id == namestaj.TipNamestaja)
                {
                    cbTipNamestaja.SelectedItem = tipNamestaja;
                }
          
            }

                 }

        private void SacuvajIzmene(object sender, RoutedEventArgs e)
        {
            List<Namestaj> postojeciNamestaj = Projekat.Instance.Namestaj;
            Namestaj namestajZaIzmenu = null;
            int tipNamestajaId = ((TipNamestaja)cbTipNamestaja.SelectedItem0).Id;

            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    var noviNamestaj = new Namestaj()
                    {
                        Id = postojeciNamestaj.Count + 1
                        Naziv = tbNaziv.Text
                        TipNamestaja = tipNamestajaId;
                    };
                    postojeciNamestaj.Add(noviNamestaj);
                    break;

                case Operacija.IZMENA:
                    foreach (var n in postojeciNamestaj)
                    {
                        if(n.Id == namestaj.Id)
                        {
                            namestajZaIzmenu = n;
                        }
                    }
                    namestajZaIzmenu.Naziv = tbNaziv.Text;
                    namestajZaIzmenu.TipNamestaja = tipNamestajaId;
                    break;
                   }


                    break;
            }
            Projekat.Instance.Namestaj = postojeciNamestaj;
            this.Close();
        }
        private void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
