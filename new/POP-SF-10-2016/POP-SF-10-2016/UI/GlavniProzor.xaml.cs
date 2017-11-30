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
        public GlavniProzor()
        {
            InitializeComponent();

         //   OsveziPrikaz(tipKorisnika);
            //listBox.SelectedIndex = 0;
        }

     /*   private void OsveziPrikaz(TipKorisnika tip)
        {
            if (tip == TipKorisnika.Administrator)
            {
                listBox.Items.Clear();
                listBox.Items.Add("Namestaj");
                listBox.Items.Add("Tip namestaja");
                listBox.Items.Add("Korisnici");
                listBox.Items.Add("Prodaja namestaja");

            }else
            {
                listBox.Items.Clear();
                listBox.Items.Add("Prodaja namestaja");
            }
        }
        
        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            var selectedItem = listBox.SelectedItem;
            switch (selectedItem)
            {
                case "Namestaj":
                    var namestajWindow = new MainWindow1(MainWindow1.Window.Namestaj);
                    namestajWindow.ShowDialog();
                    break;
                case "Tip namestaja":
                    var tipWindow = new MainWindow1(MainWindow1.Window.TipNamestaja);
                    tipWindow.ShowDialog();
                    break;
                case "Korisnici":
                    var korisniciWindow = new MainWindow1(MainWindow1.Window.Korisnici);
                    korisniciWindow.ShowDialog();
                    break;
                case "Prodaja namestaja":
                    var prodajaWindow = new MainWindow1(MainWindow1.Window.ProdajaNamestaja);
                    prodajaWindow.ShowDialog();
                    break;
            }
        }
        private void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        */
    }
}
