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
    public class Akcija : INotifyPropertyChanged
    {
        /*public int Id { get; set; }
        public DateTime pocetak { get; set; }
        public decimal popust { get; set; }
        public DateTime kraj { get; set; }
        public bool Obrisan { get; set; }
        private ObservableCollection<int> namestajNaAkcijiId*/


        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        private DateTime pocetak;

        public DateTime Pocetak
        {
            get { return pocetak; }
            set { pocetak = value; OnPropertyChanged("Pocetak"); }
        }

        private DateTime kraj;

        public DateTime Kraj
        {
            get { return kraj; }
            set { kraj = value; OnPropertyChanged("Kraj"); }
        }

        private int popust;

        public int Popust
        {
            get { return popust; }
            set { popust = value; OnPropertyChanged("Popust");}
        }

        private bool obrisan;

        public bool Obrisan
        {
            get { return obrisan; }
            set { obrisan = value; OnPropertyChanged("Obrisan"); }
        }

       // private ObservableCollection<int> namestajNaAkcijiId;

        public ObservableCollection<Namestaj> NamestajNaAkciji { get; set; }
        /*{
            get { return NamestajNaAkciji; }
            set { NamestajNaAkciji = value; OnPropertyChanged("NamestajNaAkciji"); }
        }*/

        public override string ToString()
        {
            return $"{Pocetak.ToShortDateString()} - {Kraj.ToShortTimeString()}";
        }

        public static Akcija GetById(int Id)
        {
            foreach (var akcija in Projekat.Instance.akcija)
            {
                if(akcija.Id == Id)
                {
                    return akcija;
                }
            }
            return null;
        }
        public object Clone()
        {
            return new Akcija()
            {
                Id = id,
                Pocetak = pocetak,
                Kraj = kraj,
                Popust = popust,
                Obrisan = obrisan,
                NamestajNaAkciji = new ObservableCollection<Namestaj>(NamestajNaAkciji)
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #region Database
        public static ObservableCollection<Akcija> GetAll()
        {
            var akcija = new ObservableCollection<Akcija>();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Akcija WHERE Obrisan=@Obrisan";

                cmd.Parameters.AddWithValue("Obrisan", false);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();


                da.SelectCommand = cmd;
                da.Fill(ds, "Akcija"); //Izvrsava se query nad bazom

                foreach (DataRow row in ds.Tables["Akcija"].Rows)
                {
                    var a = new Akcija();
                    a.Id = int.Parse(row["Id"].ToString());
                    a.Pocetak = DateTime.Parse(row["Pocetak"].ToString());
                    a.Kraj = DateTime.Parse(row["Kraj"].ToString());
                    a.Popust = int.Parse(row["Popust"].ToString());
                    a.Obrisan = bool.Parse(row["Obrisan"].ToString());
                    a.NamestajNaAkciji = GetNamestajNaAkciji(a.id);

                    akcija.Add(a);

                }
            }
            return akcija;
        }

        public static Akcija GetByNamestaj(Namestaj namestaj)
        {
            var akcija = new ObservableCollection<Akcija>();
            Akcija retVal = null;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "select * from Akcija a JOIN NamestajNaAkciji nna on nna.AkcijaID = a.Id where nna.NamestajID = @NamestajID AND a.Kraj > a.Pocetak AND a.Kraj >= GETDATE()";

                cmd.Parameters.AddWithValue("NamestajID", namestaj.Id);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();


                da.SelectCommand = cmd;
                da.Fill(ds, "Akcija"); //Izvrsava se query nad bazom
                foreach (DataRow row in ds.Tables["Akcija"].Rows)
                {
                    foreach (Akcija ak in Projekat.Instance.akcija)
                    {
                        if (ak.id == int.Parse(row["AkcijaID"].ToString()))
                        {
                            akcija.Add(ak);
                            break;
                        }
                    }
                    if(akcija.Count != 0)
                       retVal = akcija.First();

                }
            }
            return retVal;
        }

        public static ObservableCollection<Namestaj> GetNamestajNaAkciji(int akcijaId)
        {
            var namestaj = new ObservableCollection<Namestaj>();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM NamestajNaAkciji WHERE AkcijaID=@AkcijaID";

                cmd.Parameters.AddWithValue("AkcijaID", akcijaId);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();


                da.SelectCommand = cmd;
                da.Fill(ds, "NamestajNaAkciji"); //Izvrsava se query nad bazom

                foreach (DataRow row in ds.Tables["NamestajNaAkciji"].Rows)
                {
                    
                    Namestaj nam = Namestaj.GetDatabaseID(int.Parse(row["NamestajID"].ToString()));
                    namestaj.Add(nam);
                }
            }
            return namestaj;
        }

        public static Akcija Create(Akcija a)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO Akcija (Pocetak, Kraj, Popust, Obrisan) VALUES (@Pocetak,@Kraj,@Popust, @Obrisan);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("Pocetak", a.Pocetak);
                cmd.Parameters.AddWithValue("Kraj", a.Kraj);
                cmd.Parameters.AddWithValue("Popust", a.Popust);
                cmd.Parameters.AddWithValue("Obrisan", a.Obrisan);

                int newId = int.Parse(cmd.ExecuteScalar().ToString()); //ExecuteScalar izvrsava query
                a.Id = newId;
            }
            Projekat.Instance.akcija.Add(a); //azurirano i citanje modela
            return a;
        }

        public static void Update(Akcija a)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE Akcija SET Pocetak=@Pocetak, Kraj=@Kraj, Popust=@Popust, Obrisan = @Obrisan WHERE Id =@Id";
                cmd.Parameters.AddWithValue("Id", a.Id);
                cmd.Parameters.AddWithValue("Pocetak", a.Pocetak);
                cmd.Parameters.AddWithValue("Kraj", a.Kraj);
                cmd.Parameters.AddWithValue("Popust", a.Popust);
                cmd.Parameters.AddWithValue("Obrisan", a.Obrisan);

                cmd.ExecuteNonQuery(); //azuriranje stanje modela

                foreach (var akcija in Projekat.Instance.akcija)
                {
                    if (akcija.Id == a.Id)
                    {
                        akcija.Pocetak = a.Pocetak;
                        akcija.Kraj = a.Kraj;
                        akcija.Popust = a.Popust;
                        akcija.Obrisan = a.Obrisan;
                        break;
                    }
                }

            }

        }

        public static void addNamestajNaAkciju(Akcija a, Namestaj n)
        {
            if(n != null && a != null)
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "INSERT INTO NamestajNaAkciji (AkcijaID, NamestajID) VALUES (@AkcijaID,@NamestajID)";
                    cmd.Parameters.AddWithValue("AkcijaID", a.Id);
                    cmd.Parameters.AddWithValue("NamestajID", n.Id);
                    cmd.ExecuteNonQuery();

                }
            }
            
        }

        public static void removeNamestajNaAkciju(Akcija a, Namestaj n)
        {
            if (n != null && a != null)
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "Delete NamestajNaAkciji WHERE AkcijaID = @AkcijaID AND NamestajID=@NamestajID";
                    cmd.Parameters.AddWithValue("AkcijaID", a.Id);
                    cmd.Parameters.AddWithValue("NamestajID", n.Id);
                    cmd.ExecuteNonQuery();

                }
            }

        }

        public static void Delete(Akcija a)
        {
            a.Obrisan = true;
            Update(a);
        }
        #endregion

    }
}
