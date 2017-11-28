using POP_10.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_10.Model
{
    //singlton

   public class Projekat
    {
        public static Projekat Instance { get; private set; } = new Projekat();

       
        public ObservableCollection<Namestaj> namestaj;
        public ObservableCollection<Korisnik> korisnik;
        public ObservableCollection<TipNamestaja> tipNam;
       
        private Projekat()
        {
           
            namestaj = new ObservableCollection<Namestaj>(GenericsSerializer.Deserialize<Namestaj>("namestaj.xml"));
            korisnik = new ObservableCollection<Korisnik>(GenericsSerializer.Deserialize<Korisnik>("korisnik.xml"));
            tipNam = new ObservableCollection<TipNamestaja>(GenericsSerializer.Deserialize<TipNamestaja>("tipNamestaja.xml"));
        }
    }
}
