using POP_10.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_10.Model
{
    //singlton

   public class Projekat
    {
        public static Projekat Instance { get; private set; } = new Projekat();

        public List<TipNamestaja> TN = new List<TipNamestaja>();

        private List<Namestaj> namestaj = new List<Namestaj>();

        public List<Namestaj> Namestaj
        {
            get {
                this.namestaj = GenericsSerializer.Deserialize<Namestaj>("namestaj.xml") ;
                return this.namestaj;
            }
            set {
                this.namestaj = value;
                GenericsSerializer.Serialize<Namestaj>("namestaj.xml", this.namestaj);
                    }
        }
    }
}
