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
        public ObservableCollection<ProdajaNamestaja> prodajaNamestaja;
        public ObservableCollection<DodatnaUsluga> dodatnaUsluga;
        public ObservableCollection<Akcija> akcija;
      
        public Korisnik ulogovanKorisnik { get; set; }

        private Projekat()
        {
           //tipNam = TipNamestaja.GetAll();
             namestaj = new ObservableCollection<Namestaj>(GenericsSerializer.Deserialize<Namestaj>("namestaj.xml"));
             korisnik = new ObservableCollection<Korisnik>(GenericsSerializer.Deserialize<Korisnik>("korisnik.xml"));
             tipNam = new ObservableCollection<TipNamestaja>(GenericsSerializer.Deserialize<TipNamestaja>("tipNamestaja.xml"));
             prodajaNamestaja = new ObservableCollection<ProdajaNamestaja>(GenericsSerializer.Deserialize<ProdajaNamestaja>("prodajaNamestaja.xml"));
             dodatnaUsluga = new ObservableCollection<DodatnaUsluga>(GenericsSerializer.Deserialize<DodatnaUsluga>("dodatnaUsluga.xml"));
            akcija = new ObservableCollection<Akcija>(GenericsSerializer.Deserialize<Akcija>("akcija.xml"));
        }
    }
}
