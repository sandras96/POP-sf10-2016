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
    public enum TipKorisnika {

        Administrator,
        Prodavac

    }

    public class Korisnik : INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }
        private string ime;

        public string Ime
        {
            get { return ime; }
            set { ime = value; OnPropertyChanged("Ime"); }
        }

        private string prezime;

        public string Prezime
        {
            get { return prezime; }
            set { prezime = value; OnPropertyChanged("Prezime"); }
        }

        private string korisnickoIme;

        public string KorisnickoIme
        {
            get { return korisnickoIme; }
            set { korisnickoIme = value; OnPropertyChanged("KorisnickoIme"); }
        }

        private string lozinka;

        public string Lozinka
        {
            get { return lozinka; }
            set { lozinka = value; OnPropertyChanged("Lozinka"); }
        }

        private TipKorisnika tipKorisnka;

        public TipKorisnika TipKorisnika
        {
            get { return tipKorisnka; }
            set { tipKorisnka = value; OnPropertyChanged("TipKorisnika"); }
        }

        private bool obrisan;

        public bool Obrisan
        {
            get { return obrisan; }
            set { obrisan = value; OnPropertyChanged("Obrisan"); }
        }









        /* public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public TipKorisnika TipKorisnika { get; set; }
        public bool Obrisan { get; set; }*/

        public event PropertyChangedEventHandler PropertyChanged;

        public object Clone => new Korisnik()
        {
            Id = id,
            Ime = ime,
            Prezime = prezime,
            KorisnickoIme = korisnickoIme,
            Lozinka = lozinka,
            TipKorisnika = tipKorisnka,
            Obrisan = obrisan
        };
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #region Database
        public static ObservableCollection<Korisnik> GetAll()
        {
            var korisnik = new ObservableCollection<Korisnik>();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Korisnik WHERE Obrisan=@Obrisan";

                cmd.Parameters.AddWithValue("Obrisan", false);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();


                da.SelectCommand = cmd;
                da.Fill(ds, "Korisnik"); //Izvrsava se query nad bazom

                foreach (DataRow row in ds.Tables["Korisnik"].Rows)
                {
                    var kor = new Korisnik();
                    kor.Id = int.Parse(row["Id"].ToString());
                    kor.Ime = row["Ime"].ToString();
                    kor.Prezime = row["Prezime"].ToString();
                    kor.KorisnickoIme = row["KorisnickoIme"].ToString();
                    kor.Lozinka = row["Lozinka"].ToString();
                    kor.Obrisan = bool.Parse(row["Obrisan"].ToString());
                    bool b = bool.Parse(row["TipKorisnika"].ToString());
                    if (b == true)
                    {

                        kor.TipKorisnika = TipKorisnika.Administrator;
                    }
                    else
                    {
                        kor.TipKorisnika = TipKorisnika.Prodavac;
                    }
                    korisnik.Add(kor);

                }
            }
            return korisnik;
        }

        public static ObservableCollection<Korisnik> GetSearch(String param)
        {
            var korisnik = new ObservableCollection<Korisnik>();

            param = "%" + param + "%";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Korisnik WHERE Ime LIKE @Param OR Prezime LIKE @Param OR KorisnickoIme LIKE @Param";

                cmd.Parameters.AddWithValue("Param", param);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();


                da.SelectCommand = cmd;
                da.Fill(ds, "Korisnik"); //Izvrsava se query nad bazom

                foreach (DataRow row in ds.Tables["Korisnik"].Rows)
                {
                    var kor = new Korisnik();
                    kor.Id = int.Parse(row["Id"].ToString());
                    kor.Ime = row["Ime"].ToString();
                    kor.Prezime = row["Prezime"].ToString();
                    kor.KorisnickoIme = row["KorisnickoIme"].ToString();
                    kor.Lozinka = row["Lozinka"].ToString();
                    kor.Obrisan = bool.Parse(row["Obrisan"].ToString());
                    bool b = bool.Parse(row["TipKorisnika"].ToString());
                    if (b == true)
                    {

                        kor.TipKorisnika = TipKorisnika.Administrator;
                    }
                    else
                    {
                        kor.TipKorisnika = TipKorisnika.Prodavac;
                    }
                    korisnik.Add(kor);

                }
            }
            return korisnik;
        }

        public static Korisnik Create(Korisnik kor)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO Korisnik (Ime,Prezime,KorisnickoIme,Lozinka,Obrisan,TipKorisnika) VALUES (@Ime,@Prezime,@KorisnickoIme,@Lozinka, @Obrisan, @TipKorisnika);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("Ime", kor.Ime);
                cmd.Parameters.AddWithValue("Prezime", kor.Prezime);
                cmd.Parameters.AddWithValue("KorisnickoIme", kor.KorisnickoIme);
                cmd.Parameters.AddWithValue("Lozinka", kor.Lozinka);
                cmd.Parameters.AddWithValue("Obrisan", kor.Obrisan);
                cmd.Parameters.AddWithValue("TipKorisnika", kor.TipKorisnika);


                int newId = int.Parse(cmd.ExecuteScalar().ToString()); //ExecuteScalar izvrsava query
                kor.Id = newId;
            }
            Projekat.Instance.korisnik.Add(kor); //azurirano i citanje modela
            return kor;
        }

        public static void Update(Korisnik kor)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE Korisnik SET Ime = @Ime,Prezime = @Prezime,KorisnickoIme=@KorisnickoIme,Lozinka=@Lozinka, Obrisan = @Obrisan, TipKorisnika=@TipKorisnika WHERE Id =@Id";
                cmd.Parameters.AddWithValue("Id", kor.Id);
                cmd.Parameters.AddWithValue("Ime", kor.Ime);
                cmd.Parameters.AddWithValue("Prezime", kor.Prezime);
                cmd.Parameters.AddWithValue("KorisnickoIme", kor.KorisnickoIme);
                cmd.Parameters.AddWithValue("Lozinka", kor.Lozinka);
                cmd.Parameters.AddWithValue("Obrisan", kor.Obrisan);
                cmd.Parameters.AddWithValue("TipKorisnika", kor.TipKorisnika);

                cmd.ExecuteNonQuery(); //azuriranje stanje modela

                foreach (var korisnik in Projekat.Instance.korisnik)
                {
                    if (korisnik.Id == kor.Id)
                    {
                        korisnik.Ime = kor.Ime;
                        korisnik.Prezime = kor.Prezime;
                        korisnik.KorisnickoIme = kor.KorisnickoIme;
                        korisnik.Lozinka = kor.Lozinka;
                        korisnik.Obrisan = kor.Obrisan;
                        korisnik.TipKorisnika = kor.TipKorisnika;
                        break;
                    }
                }

            }

        }

        public static void Delete(Korisnik kor)
        {
            kor.Obrisan = true;
            Update(kor);
        }
        #endregion
    }
}
