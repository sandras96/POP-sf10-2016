﻿using POP_10.Model;
using POP_10.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            cbUsluga.ItemsSource = filterActive(Projekat.Instance.dodatnaUsluga);
            cbUsluga.SelectedIndex = 0;

            dpDatumProdaje.DataContext = prodajaNamestaja;
            tbBrRacuna.DataContext = prodajaNamestaja;
            tbKupac.DataContext = prodajaNamestaja;
            cbUsluga.DataContext = prodajaNamestaja;
        }

        private ObservableCollection<DodatnaUsluga> filterActive(ObservableCollection<DodatnaUsluga> lista)
        {
            ObservableCollection<DodatnaUsluga> filteredList = new ObservableCollection<DodatnaUsluga>();
            foreach (DodatnaUsluga dodatnaUsluga in lista)
            {
                if(dodatnaUsluga.Obrisan == false)
                {
                    filteredList.Add(dodatnaUsluga);
                }
            }
            return filteredList;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            var listaProdaje = Projekat.Instance.prodajaNamestaja;

            switch (operacija)
            {
                case Operacija.DODAVANJE:

                    prodajaNamestaja.Id = listaProdaje.Count + 1;
                    prodajaNamestaja = ProdajaNamestaja.Create(prodajaNamestaja);
                   // listaProdaje.Add(prodajaNamestaja);
                    break;

                case Operacija.IZMENA:

                    foreach (var prodaja in listaProdaje)
                    {
                        if (prodaja.Id == prodajaNamestaja.Id)
                        {
                            prodaja.DatumProdaje = prodajaNamestaja.DatumProdaje;
                            prodaja.BrojRacuna = prodajaNamestaja.BrojRacuna;
                            prodaja.Kupac = prodajaNamestaja.Kupac;
                            prodaja.DodatnaUsluga = prodajaNamestaja.DodatnaUsluga;
                            prodaja.DodatnaUslugaID = prodajaNamestaja.DodatnaUslugaID;
                            prodaja.NamestajZaProdaju = prodajaNamestaja.NamestajZaProdaju;
                            ProdajaNamestaja.Update(prodaja);
                            break;
                        }
                    }

                    break;


            }
          //  GenericsSerializer.Serialize("prodajaNamestaja.xml", listaProdaje);

            this.prodajaNamestaja.izracunajCenu();
            MessageBox.Show(this.prodajaNamestaja.UkupnaCena.ToString(), caption: "Ukupna Cena", button: MessageBoxButton.OK, icon: MessageBoxImage.Information);

            this.Close();
        }


        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            NamestajProdaja np = new NamestajProdaja();
            np.Show();

          

           np.Closed += np_Closed;
        }

       
        private void np_Closed(object sender, EventArgs e)
         {
            var add = sender as NamestajProdaja;
            Boolean dodat = false;
            if(add.ProdajNamestaj != null)
            {
                foreach (Namestaj namestaj in prodajaNamestaja.NamestajZaProdaju)
                {
                    if (add.ProdajNamestaj.Id == namestaj.Id)
                    {
                        namestaj.Kolicina += add.ProdajNamestaj.Kolicina;
                        dodat = true;
                    }
                       
                }
                if (!dodat)
                {
                    prodajaNamestaja.NamestajZaProdaju.Add((add).ProdajNamestaj);
                }
               
            }
           
         
            
         }

        private void btnUkloni_Click(object sender, RoutedEventArgs e)
        {
            var selektovaniNamestaj = dgPNamestaj.SelectedItem as Namestaj;
            if (selektovaniNamestaj != null)
            {
                prodajaNamestaja.NamestajZaProdaju.Remove(selektovaniNamestaj);
                foreach (Namestaj namestaj in Projekat.Instance.namestaj)
                {
                    if (selektovaniNamestaj.Id == namestaj.Id)
                    {
                        namestaj.Kolicina += selektovaniNamestaj.Kolicina;
                    }
                }
            }
            else
            {
                MessageBox.Show($"Morate selektovati namestaj!","Obavestenje", MessageBoxButton.OK);
            }
            
        }

        private void btnPonisti_Click(object sender, RoutedEventArgs e)
        {
            foreach (Namestaj prodajniNamestaj in prodajaNamestaja.NamestajZaProdaju)
            {
                foreach (Namestaj originalNamestaj in Projekat.Instance.namestaj)
                {
                    if(prodajniNamestaj.Id == originalNamestaj.Id)
                    {
                        originalNamestaj.Kolicina += prodajniNamestaj.Kolicina;
                    }
                }
            }
            this.Close();
        }
    }
}
