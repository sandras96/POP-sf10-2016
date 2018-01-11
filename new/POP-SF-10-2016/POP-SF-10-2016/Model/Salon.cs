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
    public class Salon : INotifyPropertyChanged
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        private string naziv;

        public string Naziv
        {
            get { return naziv; }
            set { naziv = value; OnPropertyChanged("Naziv"); }
        }

        private string adresa;

        public string Adresa
        {
            get { return adresa; }
            set { adresa = value; OnPropertyChanged("Adresa"); }
        }
        private string telefon;

        public string Telefon
        {
            get { return telefon; }
            set { telefon = value; OnPropertyChanged("Telefon"); }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged("Email"); }
        }

        private string webSite;

        public string WebSite
        {
            get { return webSite; }
            set { webSite = value; OnPropertyChanged("WebSite"); }
        }

        private int pib;

        public int PIB
        {
            get { return pib; }
            set { pib = value; OnPropertyChanged("PIB"); }
        }

        private int maticniBroj;

        public int MaticniBroj
        {
            get { return maticniBroj; }
            set { maticniBroj = value; OnPropertyChanged("MaticniBroj"); }
        }

        private string brojZiroRacuna;

        public string BrojZiroRacuna
        {
            get { return brojZiroRacuna; }
            set { brojZiroRacuna = value; OnPropertyChanged("BrojZiroRacuna"); }
        }

        /* public int Id { get; set; }
         public string Naziv { get; set; }
         public string Adresa { get; set; }
         public string Telefon { get; set; }
         public string Email { get; set; }
         public string Websajt { get; set; }
         public int PIB { get; set; }
         public int MaticniBroj { get; set; }
         public string BrojZiroRacuna { get; set; }*/

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public object Clone()
        {
            return new Salon()
            {
                Id = id,
                Naziv = naziv,
                Adresa = adresa,
                Telefon = telefon,
                Email = email,
                WebSite = webSite,
                PIB = pib,
                MaticniBroj = maticniBroj,
                BrojZiroRacuna = brojZiroRacuna
            };
        }

        #region Database
        public static Salon GetAll()
        {
            Salon salon = null;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Salon";

                
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();


                da.SelectCommand = cmd;
                da.Fill(ds, "Salon"); //Izvrsava se query nad bazom

                foreach (DataRow row in ds.Tables["Salon"].Rows)
                {
                    var s = new Salon();
                    s.Id = int.Parse(row["Id"].ToString());
                    s.Naziv = row["Naziv"].ToString();
                    s.Adresa = row["Adresa"].ToString();
                    s.Telefon = row["Telefon"].ToString();
                    s.Email = row["Email"].ToString();
                    s.WebSite = row["WebSite"].ToString();
                    s.PIB = int.Parse(row["PIB"].ToString());
                    s.MaticniBroj = int.Parse(row["MaticniBroj"].ToString());
                    s.BrojZiroRacuna = row["BrojZiroRacuna"].ToString();

                    salon = s;

                }
            }
            return salon;
        }
        public static void Update(Salon s)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE Namestaj SET Naziv = @Naziv, Adresa=@Adresa, Telefon=@Telefon, Email=@Email, WebSite=@WebSite, PIB=@PIB, MaticniBroj=@MaticniBroj, BrojZiroRacuna=@BrojZiroRacuna WHERE Id =@Id";
                cmd.Parameters.AddWithValue("Id", s.Id);
                cmd.Parameters.AddWithValue("Naziv", s.Naziv);
                cmd.Parameters.AddWithValue("Adresa", s.Adresa);
                cmd.Parameters.AddWithValue("Telefon", s.Telefon);
                cmd.Parameters.AddWithValue("Email", s.Email);
                cmd.Parameters.AddWithValue("WebSite", s.WebSite);
                cmd.Parameters.AddWithValue("PIB", s.PIB);
                cmd.Parameters.AddWithValue("MaticniBroj", s.MaticniBroj);
                cmd.Parameters.AddWithValue("BrojZiroRacuna", s.BrojZiroRacuna);

                cmd.ExecuteNonQuery(); //azuriranje stanje modela
                   
                

            }

        }
        #endregion
    }

}
