using POP_10.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_10
{   
    class Program
    {
        public static List<Namestaj> Namestaj1 { get; set; } = new List<Namestaj>();
        static void Main(string[] args)
        {
            List<Namestaj> namestaj = Projekat.Instance.Namestaj;
            namestaj.Add(new Namestaj() { Id = 2, "novi namestaj" });
            Projekat.Instance.Namestaj = namestaj;

            foreach (var komad in namestaj)
            {
                Console.WriteLine($"{komad.Naziv}");
            }
            Console.ReadLine();

            var tn1 = new TipNamestaja()
            {
                Id = 1;
            Naziv = "Podna lampa"
               };
        var n1 = new Namestaj()
        {
            Id = 1,
            Naziv = "Super svetlo",
            JedinicnaCena = 28,
            KolicinivaUMagacinu = 3,
            Sifra = "SP12315",
            TipNamestaja = tn1,

        };
        var n2 = new Namestaj()
        {
            Id = 1,
            Naziv = "Super lampa",
            JedinicnaCena = 29,
            KolicinivaUMagacinu = 13,
            Sifra = "SP12695",
            TipNamestaja = tn1

        };
        Console.WriteLine("Serijalizacija...");
        GenericSerializer.Serialize("namestaj.xml", n1);
        GenericSerializer.Serialize("namestaj.xml", n2);

        Console.WriteLine("Finish serialization.");
        Console.ReadLine();    }

    Salon s1 = new Salon()
            {
                Id = 1,
                Naziv = "Formafinal",
                Adresa = "Trg Dositeja Obradovica 6",
                BrojZiroRacuna = "564864221-451-15415656",
                Email = "form@gmail.com",
                PIB = 3414135,
                MaticniBroj = 1234,
                Telefon = "021-528-951",
                Websajt = "http://www.ftn.uns.ac.rs"


            };
            var sofaTN = new TipNamestaja()
            {
                Id = 1,
                Naziv = "Sofa"
            };
            Namestaj1.Add(new Namestaj()
            { Id = 1,
             JedinicnaCena = 2314, 
                });
            Console.WriteLine($"Dobrodosli u salon namestaja {s1.Naziv}.");
            IspisiGlavniMeni();
            Console.ReadLine();
         }
        private static void IspisiGlavniMeni()
        {   int izbor =0;
            do
            {
                Console.WriteLine("-------Glavni Meni--------");
                Console.WriteLine("1. Rad sa namestajem");
                Console.WriteLine("2. Rad sa tipom namestaja");
                //Dodaj ostale
                Console.WriteLine("0. izlaz iz app");
                
                
            } while(izbor < 0 || izbor >2);

            izbor = int.Parse(Console.ReadLine());

            switch (izbor)
            {
                case 1:
                    IspisiMeniNamestaja();
                    break;
                case 2:

                    IspisiMeniTipNamestaja();
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
                default:

                    break;

            }
        }
        private static void IspisiMeniNamestaja()
        {
            int izbor = 0;
            do {
                Console.WriteLine("-------Namestaj-------");
                Console.WriteLine("1.Listing namestaja");
                Console.WriteLine("2.Dodaj novi namestaj");
                Console.WriteLine("3.Izmeni postojeci");
                Console.WriteLine("4.Obrisi postojeci");
                Console.WriteLine("0.Izadji");

            } while (izbor <0 || izbor >4);

            izbor = int.Parse(Console.ReadLine());
            switch (izbor)
            {
                case 1:IzlistajNamestaj();
                    break;
                case 2:
                    DodajNamestaj();
                    break;
                case 3:
                    IzmeniNamestaj();
                    break;
                case 4:
                    ObrisiNamestaj();
                    break;
                case 0:
                    IspisiGlavniMeni();
                    break;
                default:
                    break;

            }
        }

        private static void ObrisiNamestaj()
        {
            throw new NotImplementedException();
        }

        private static void IzmeniNamestaj()
        {
            throw new NotImplementedException();
        }

        private static void DodajNamestaj()
        {
            Console.WriteLine("Naziv:");
            String naziv=Console.ReadLine();
            Console.WriteLine("Cena:");
            String cena =Console.ReadLine();
            Console.WriteLine("Kolicina u magacinu:");
            String koliUmagacinu = Console.ReadLine();
        }
       
        private static void IspisiMeniTipNamestaja()
        {
        }
    private static void IzlistajNamestaj()
        {
        int index = 0;
        Console.WriteLine("------------Listing namestaja--------------");
        foreach (var namestaj in Namestaj1)
            {
            Console.WriteLine($"{ ++index}.{namestaj.Naziv}, cena: {namestaj.JedinicnaCena}");
            }
            IspisiMeniNamestaja();
        }
//pretraga, mehanizam za logovanje
      
}
