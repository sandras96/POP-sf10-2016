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
    /// Interaction logic for KolicinaWindow.xaml
    /// </summary>
    public partial class KolicinaWindow : Window
    {
        //public int Kolicina { get; set; }

        //public Namestaj SelektovanNamestaj { get; set; }

        public KolicinaWindow()
        {
            InitializeComponent();
            //this.SelektovanNamestaj = namestaj;
        }

       /* private void btnOk_Click(object sender, RoutedEventArgs e)
        {
         /*   try
            {
                int numb;
                Boolean konverzija = Int32.TryParse(txtKolicina.Text,out numb);
                if (!konverzija)
                {
                    throw new Exception("Kolicina mora biti broj!");
                }
                
                if(numb <= 0)
                {
                    throw new Exception("Kolicina mora biti veca od nule!");
                }

                if(numb > this.SelektovanNamestaj.Kolicina)
                {
                    throw new Exception("Nemamo toliko na lageru!");
                }

            }catch(Exception exc)
            {
                MessageBox.Show(exc.Message, "Greska ", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.Close();
        }

        private void btnPonisti_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }*/
    }
}
