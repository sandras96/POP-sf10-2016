﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_10.Model
{
    [Serializable]
    public enum Tipkorisnika {

        Administrator,
        Prodavac

    }
    
    public class Korisnik
    {   public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public Tipkorisnika TipKorisnika { get; set; }
        public bool Obrisan { get; set; }
    }
}
