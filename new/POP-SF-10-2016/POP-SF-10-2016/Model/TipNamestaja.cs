using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_10.Model
{
    [Serializable]
    public class TipNamestaja
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public bool Obrisan { get; set; }

        public static TipNamestaja GetById(int Id)
        {
            foreach (var tipN in Projekat.Instance.TipoviNamestaja)
            {
                if(tipN.Id== Id)
                {
                    return tipN;
                }
            }
            return null;
        }
    }
}
