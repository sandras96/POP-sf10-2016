using POP_10.Model;
using POP_SF_9_2016.UI;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace POP_SF_10_2016
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {


            InitializeComponent();
            OsveziPrikaz();
        }

            private void OsveziPrikaz()
            {
                listBoxNamestaj.Items.Clear();

                foreach (var namestaj in Projekat.Instance.Namestaj)
                {
                    if(namestaj.Obrisan == false)
                {
                    listBoxNamestaj.Items.Add(namestaj);
                }
               
                }

            listBoxNamestaj.SelectedIndex = 0;
            }

        private void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void DodajNamestaj(object sender, RoutedEventArgs e)
        {
            var noviNamestaj = new Namestaj()
            {
                Naziv = ""
            };
            var namestajProzor = new Window1(noviNamestaj, Window1.Operacija.DODAVANJE);
            namestajProzor.ShowDialog();

            OsveziPrikaz();
        }
        
        private void IzmeniNamestaj(object sender, RoutedEventArgs e)
        {
            var selektovaniNamestaj = (Namestaj)listBoxNamestaj.SelectedItem;
            var namestajProzor = new Window1(selektovaniNamestaj, Window1.Operacija.IZMENA);
            namestajProzor.ShowDialog();

            OsveziPrikaz();
        }

        private void btnObrisiNamestaj_Click(object sender, RoutedEventArgs e)
        {
            var izabraniNamestaj = ((Namestaj)listBoxNamestaj.SelectedItem);
            var ListaNamestaja = Projekat.Instance.Namestaj;
            
            if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete: { izabraniNamestaj.Naziv}", "Brisanje", MessageBoxButton.YesNo) == MessageBoxResult.Yes){
                foreach (var n in ListaNamestaja)
                {
                    Namestaj namestaj = null;
                    foreach (var n in ListaNamestaja)
                    {
                        if (n.Id == izabraniNamestaj.Id)
                        {
                            namestaj = n;
                        }
                    }
                   
                }
                //var namestaj = Namestaj.GetById(izabraniNamestaj.Id);
                namestaj.Obrisan = true;

                Projekat.Instance.Namestaj = ListaNamestaja;
                OsveziPrikaz();
            }
        }
        //data grid dodaj
    }
}
