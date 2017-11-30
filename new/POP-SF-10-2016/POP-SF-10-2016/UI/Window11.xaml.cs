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
    /// Interaction logic for Window11.xaml
    /// </summary>
    public partial class Window11 : Window
    {
        public Window11()
        {
            InitializeComponent();
        }

        private void btnNamestaj(object sender, RoutedEventArgs e)
        {
            NamestajWindow nw = new NamestajWindow();
            nw.ShowDialog();
        }
        
        private void btnKorisnici(object sender, RoutedEventArgs e)
        {
            KorisniciWindow kw = new KorisniciWindow();
            kw.ShowDialog();
        }
        /*
        private void btnProdajaNamestaja(object sender, RoutedEventArgs e)
        {
            ProdajaNamestajaWindow pnw = new ProdajaNamestajaWindow();
            pnw.ShowDialog();
        }
        private void btnAkcija(object sender, RoutedEventArgs e)
        {
            AkcijaWindow aw = new AkcijaWindow();
            aw.ShowDialog();
        }
        private void btnTipNamestaja(object sender, RoutedEventArgs e)
        {
            TipNamestajaWindow tnw = new TipNamestajaWindow();
            tnw.ShowDialog();
        }
        */
        private void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
