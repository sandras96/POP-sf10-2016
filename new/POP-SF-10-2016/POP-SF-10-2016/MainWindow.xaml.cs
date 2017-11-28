﻿using POP_10.Model;
using POP_10.Util;
using POP_SF_10_2016.UI;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace POP_SF_10_2016
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
      /*  ObservableCollection<Namestaj> lista = new ObservableCollection<Namestaj>();
        ObservableCollection<Korisnik> lista1 = new ObservableCollection<Korisnik>();
        ObservableCollection<TipNamestaja> lista2 = new ObservableCollection<TipNamestaja>();
        /*  ObservableCollection<Namestaj> lista = new ObservableCollection<Namestaj>();
          public MainWindow()
          {
              InitializeComponent();

              lista.Add(new Namestaj
              {
                  Id = 1,


              });
              GenericsSerializer.Serialize("namestaj.xml", lista);
              OsveziPrikaz(); brise se osveziprikaz, umesto listboxnamestaj ide DataGrid 

           dgNamestaj.ItemSource = Projekat.Instance.Namestaj;
           dgNamestaj.IsSynchronizedWithCurrentItem = true;

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
    }*/

        public MainWindow()
        {
            InitializeComponent();

            cbTipKorisnika.Items.Add(Tipkorisnika.Administrator);
            cbTipKorisnika.Items.Add(Tipkorisnika.Prodavac);
            cbTipKorisnika.SelectedIndex = 0;

          /*  lista.Add(new Namestaj
            {
                Id = 1,
                Naziv = "Sofa",
                JedinicnaCena = 63,
                Kolicina = 2,
                TipN = 1,
            });

            lista1.Add(new Korisnik
            {
                Id = 1,
                Ime = "a",
                Prezime = "a",
                KorisnickoIme = "a",
                Lozinka = "a",
                
                
            });

            lista2.Add(new TipNamestaja
            {
               Id = 1,
               Naziv = "Sofa",


            });

            GenericsSerializer.Serialize("namestaj.xml", lista);
            GenericsSerializer.Serialize("korisnik.xml", lista1);
            GenericsSerializer.Serialize("tipNamestaja.xml", lista2);
          */
        }
        private static bool Login(String korIme, String lozinka, String tip)
            {
                foreach (var korisnik in Projekat.Instance.korisnik)
                {
                    if (korIme == korisnik.KorisnickoIme && lozinka == korisnik.Lozinka && "Administrator" == tip)
                {
                    return true;
                }
                        
                if (korIme == korisnik.KorisnickoIme && lozinka == korisnik.Lozinka && "Prodavac" == tip)
                {
                    return true;
                }
                    
                 }
                return false;
            }
    private void Potvrdi(object sender, RoutedEventArgs e)
            {
                if(Login(tbKI.Text, tbLoz.Text, cbTipKorisnika.SelectedItem.ToString()) == true)
                {
                    var glavniProzor = new GlavniProzor((Tipkorisnika)cbTipKorisnika.SelectedItem);
                glavniProzor.ShowDialog();
                } 
                
                else { MessageBox.Show("Netacni podaci! Pokusajte ponovo", "Greska", MessageBoxButton.OK, MessageBoxImage.Error); }
            }

        private void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }

    }

