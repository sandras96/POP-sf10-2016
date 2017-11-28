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

namespace POP_SF_10_2016.UI
{
    /// <summary>
    /// Interaction logic for GlavniProzor.xaml
    /// </summary>
    public partial class GlavniProzor : Window
    {
        public GlavniProzor(Tipkorisnika tipKorisnika)
        {
            InitializeComponent();
        }

        private void btnNamestaj(object sender, RoutedEventArgs e)
        {
            NamestajWindow nw = new NamestajWindow();
            nw.ShowDialog();
        }

       /* private void btnProdajaNamestaja(object sender, RoutedEventArgs e)
        {
            ProdajaNamestajaWindow pn = new ProdajaNamestajaWindow();
            pn.ShowDialog();
        }

        private void btnAkcija(object sender, RoutedEventArgs e)
        {
            AkcijaWindow aw = new AkcijaWindow();
            aw.ShowDialog();
        }

        private void btnKorisnici(object sender, RoutedEventArgs e)
        {
            KorisniciWindow kw = new KorisniciWindow();
            kw.ShowDialog();
        }*/
    }
}
