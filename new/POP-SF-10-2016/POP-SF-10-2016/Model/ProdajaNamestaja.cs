using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_10.Model
{
    [Serializable]
    public class ProdajaNamestaja : INotifyPropertyChanged
    {

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        private DateTime datumProdaje;

        public DateTime DatumProdaje
        {
            get { return datumProdaje; }
            set { datumProdaje = value; OnPropertyChanged("DatumProdaje"); }
        }

        private string brojRacuna;

        public string BrojRacuna
        {
            get { return brojRacuna; }
            set { brojRacuna = value; OnPropertyChanged("BrojRacuna"); }
        }


        private string kupac;

        public string Kupac
        {
            get { return kupac; }
            set { kupac = value; OnPropertyChanged("Kupac"); }
        }

        private double ukupnaCena;

        public double UkupnaCena
        {
            get { return ukupnaCena; }
            set { ukupnaCena = value; OnPropertyChanged("UkupnaCena"); }
        }
        private bool obrisan;

        public bool Obrisan
        {
            get { return obrisan; }
            set { obrisan = value; OnPropertyChanged("Obrisan"); }
        }

        public static ProdajaNamestaja GetById(int Id)
        {
            foreach (var prodaja in Projekat.Instance.prodajaNamestaja)
            {
                if(prodaja.Id == Id)
                {
                    return prodaja;
                }
            }
            return null;
        }



        public const double PDV = 0.02;


        private DodatnaUsluga dodatnaUsluga;
        public DodatnaUsluga DodatnaUsluga
        {
            get
            {
                if (dodatnaUsluga == null)
                {
                    dodatnaUsluga = DodatnaUsluga.GetById(dodatnaUslugaID);
                }
                return dodatnaUsluga;
            }
            set {
                    dodatnaUsluga = value;
                    dodatnaUslugaID = dodatnaUsluga.Id;
                    OnPropertyChanged("DodatnaUsluga"); }

        }
        private int dodatnaUslugaID;
                
        public int DodatnaUslugaID
        {
            get { return dodatnaUslugaID; }
            set { dodatnaUslugaID = value; OnPropertyChanged("DodatnaUslugaID"); }
        }

        public ObservableCollection<Namestaj> NamestajZaProdaju { get; set; }
        //public ObservableCollection<int> DodatnaUslugaId { get; set; }

        /*public int Id { get; set; }
        public int BrojKomadaNamestaja { get; set; }
        public DateTime DatumProdaje { get; set; }
        public string BrojRacuna { get; set; }
        public string Kupac { get; set; }
        public List<DodatnaUsluga> DodatneUsluge { get; set; }
        public const double PDV = 0.02;
        public double UkupnaCena { get; set; }*/

        protected void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void izracunajCenu()
        {
            if (this.dodatnaUsluga != null)
            {
                this.ukupnaCena += this.dodatnaUsluga.Cena;
            }
            foreach (Namestaj namestaj in this.NamestajZaProdaju)
            {
                Akcija akcija = Akcija.GetByNamestaj(namestaj);
                int cena = namestaj.JedinicnaCena;
                if(akcija != null)
                {
                    int cenaPopust = cena - cena * (akcija.Popust / 100);
                    this.ukupnaCena += (cenaPopust + cenaPopust * PDV) * namestaj.Kolicina;
                }
                else
                {
                    this.ukupnaCena += (cena + cena * PDV) * namestaj.Kolicina;
                }
               
               
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public object Clone()
        {
            return new ProdajaNamestaja()
            {
                Id = id,
                DatumProdaje = datumProdaje,
                BrojRacuna = brojRacuna,
                Kupac = kupac,
                UkupnaCena = ukupnaCena,
                DodatnaUsluga = dodatnaUsluga,
                DodatnaUslugaID = dodatnaUslugaID,
                NamestajZaProdaju = new ObservableCollection<Namestaj>(NamestajZaProdaju),
                //DodatnaUslugaId = new ObservableCollection<int>(DodatnaUslugaId)
                
            };
        }
        #region Database
        public static ObservableCollection<ProdajaNamestaja> GetAll()
        {
            var prodajaNamestaja = new ObservableCollection<ProdajaNamestaja>();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM ProdajaNamestaja WHERE Obrisan=@Obrisan";

                cmd.Parameters.AddWithValue("Obrisan", false);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();


                da.SelectCommand = cmd;
                da.Fill(ds, "ProdajaNamestaja"); //Izvrsava se query nad bazom

                foreach (DataRow row in ds.Tables["ProdajaNamestaja"].Rows)
                {
                    var pn = new ProdajaNamestaja();
                    pn.Id = int.Parse(row["Id"].ToString());
                    if(row.IsNull("DodatnaUslugaID"))
                    {
                        pn.DodatnaUslugaID = 0;
                    }
                    else
                    {
                        pn.DodatnaUslugaID = int.Parse(row["DodatnaUslugaID"].ToString());
                    }
                    
                    pn.DatumProdaje = DateTime.Parse(row["DatumProdaje"].ToString());
                    pn.BrojRacuna = row["BrojRacuna"].ToString();
                    pn.Kupac = row["Kupac"].ToString();
                    pn.UkupnaCena = int.Parse(row["UkupnaCena"].ToString());

                    pn.Obrisan = bool.Parse(row["Obrisan"].ToString());

                    prodajaNamestaja.Add(pn);

                }
            }
            return prodajaNamestaja;
        }

        public static ObservableCollection<ProdajaNamestaja> GetSearch(String param)
        {
            var prodajaNamestaja = new ObservableCollection<ProdajaNamestaja>();
            param = "%" + param + "%";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM ProdajaNamestaja WHERE Kupac LIKE @Param OR BrojRacuna LIKE @Param OR DatumProdaje LIKE @Param";

                cmd.Parameters.AddWithValue("Param", param);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();


                da.SelectCommand = cmd;
                da.Fill(ds, "ProdajaNamestaja"); //Izvrsava se query nad bazom

                foreach (DataRow row in ds.Tables["ProdajaNamestaja"].Rows)
                {
                    var pn = new ProdajaNamestaja();
                    pn.Id = int.Parse(row["Id"].ToString());
                    pn.DodatnaUslugaID = int.Parse(row["DodatnaUslugaID"].ToString());
                    pn.DatumProdaje = DateTime.Parse(row["DatumProdaje"].ToString());
                    pn.BrojRacuna = row["BrojRacuna"].ToString();
                    pn.Kupac = row["Kupac"].ToString();
                    pn.UkupnaCena = int.Parse(row["UkupnaCena"].ToString());

                    pn.Obrisan = bool.Parse(row["Obrisan"].ToString());

                    prodajaNamestaja.Add(pn);

                }
            }
            return prodajaNamestaja;
        }

        public static ProdajaNamestaja Create(ProdajaNamestaja pn)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO Prodajanamestaja (DatumProdaje,dodatnaUslugaID,BrojRacuna,Kupac,UkupnaCena,Obrisan) VALUES (@DatumProdaje,@dodatnaUslugaID,@BrojRacuna,@Kupac,@UkupnaCena, @Obrisan);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("DatumProdaje", pn.DatumProdaje);
                if(pn.dodatnaUsluga != null)
                {
                    cmd.Parameters.AddWithValue("dodatnaUslugaID", pn.dodatnaUsluga.Id);
                }
                else
                {
                    cmd.Parameters.AddWithValue("dodatnaUslugaID", DBNull.Value);
                }
                
                cmd.Parameters.AddWithValue("BrojRacuna", pn.BrojRacuna);
                cmd.Parameters.AddWithValue("Kupac", pn.Kupac);
                cmd.Parameters.AddWithValue("UkupnaCena", pn.UkupnaCena);
                cmd.Parameters.AddWithValue("Obrisan", pn.Obrisan);

                int newId = int.Parse(cmd.ExecuteScalar().ToString()); //ExecuteScalar izvrsava query
                pn.Id = newId;
            }
            Projekat.Instance.prodajaNamestaja.Add(pn); //azurirano i citanje modela
            return pn;
        }

        public static void Update(ProdajaNamestaja pn)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE ProdajaNamestaja SET DatumProdaje=@DatumProdaje, BrojRacuna=@BrojRacuna, Kupac=@Kupac, UkupnaCena=@UkupnaCena, Obrisan = @Obrisan WHERE Id =@Id";
                cmd.Parameters.AddWithValue("Id", pn.Id);
                cmd.Parameters.AddWithValue("DatumProdaje", pn.DatumProdaje);
                cmd.Parameters.AddWithValue("BrojRacuna", pn.BrojRacuna);
                cmd.Parameters.AddWithValue("Kupac", pn.Kupac);
                cmd.Parameters.AddWithValue("UkupnaCena", pn.UkupnaCena);
                cmd.Parameters.AddWithValue("Naziv", pn.Obrisan);

                cmd.ExecuteNonQuery(); //azuriranje stanje modela

                foreach (var prodajaNamestaja in Projekat.Instance.prodajaNamestaja)
                {
                    if (prodajaNamestaja.Id == pn.Id)
                    {
                        prodajaNamestaja.DatumProdaje = pn.DatumProdaje;
                        prodajaNamestaja.BrojRacuna = pn.BrojRacuna;
                        prodajaNamestaja.Kupac = pn.Kupac;
                        prodajaNamestaja.UkupnaCena = pn.UkupnaCena;
                        prodajaNamestaja.Obrisan = pn.Obrisan;
                        break;
                    }
                }

            }

        }

        public static void Delete(ProdajaNamestaja pn)
        {
            pn.Obrisan = true;
            Update(pn);
        }
        #endregion

    }
}
