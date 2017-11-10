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
    /// Interaction logic for NamestajWindow.xaml
    /// </summary>
    public partial class NamestajWindow : Window
    {
        private Namestaj namestaj;
        private Operacija operacija;

        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };
        public NamestajWindow(Namestaj namestaj, Operacija opetacija = Operacija.DODAVANJE) //u slucaju da neko ne navede da li je dodavanje ili izmena, bice dodavanje
        {

            InitializeComponent();
            InicijalizujVrednost(namestaj, operacija);
        
           
        }
        private void InicijalizujVrednost(Namestaj namestaj, Operacija operacija)
        {
            this.namestaj = namestaj;
            this.operacija = operacija;

            tbNaziv.Text = namestaj.Naziv;
        }

        private void SacuvajIzmene(object sender, RoutedEventArgs e)
        {
            List<Namestaj> postojeciNamestaj = Projekat.Instance.Namestaj;

            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    var noviNamestaj = new Namestaj()
                    {
                        Naziv =tbNaziv.Text
                    };
                    postojeciNamestaj.Add(noviNamestaj);
                    break;
                case Operacija.IZMENA:
                    foreach (var n in postojeciNamestaj)
                    {
                        if(n.Id == namestaj.Id)
                        {
                            n.Naziv = tbNaziv.Text;
                            break;
                        }
                    }
                    

                    break;
            }
            Projekat.Instance.Namestaj = postojeciNamestaj;
            this.Close();
        }
    }
}
