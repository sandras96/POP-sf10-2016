using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_10.Model
{   [Serializable]
    public class Namestaj
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public String Sifra { get; set; }
        public double JedinicnaCena { get; set; }
        public int KolicinivaUMagacinu { get; set; }
        public bool Obrisan { get; set; }
        public int  TipNamestaja { get; set; } /* public int IdTipaNamestaja { get; set; }

        //public TipNamestaja TipNamestaja { get; set; }

        public override string ToString()
        {
            return $"{Naziv}, {Cena}, {TipNamestaja.GetById(IdTipaNamestaja).Naziv}";
        }

        */ public static Namestaj GetById(int id)
        {
            foreach (var namestaj in Projekat.Instance.Namestaj)
            {
                if (namestaj.Id == id)
                {
                    return namestaj; //mozda nije id nego naziv ili nzm i onda pozivan Tipnamestaja.getbyid(idtipnamestaja)
                }
            }
            return null;
        }

        public Akcija Akcija { get; set; }
        
        public Namestaj()
        {

        }

        public override string ToString()
        {
            return $"{ Naziv }, {JedinicnaCena }"; 
        }
    }
}
