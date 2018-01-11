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
using System.Xml.Serialization;

namespace POP_10.Model
{
    [Serializable]
    public class Namestaj : INotifyPropertyChanged
    {
       

        private int id;

        public int Id
        {
            get { return id; }
            set {
                id = value; OnPropertyChanged("Id");}
        }

        private string naziv;

        public string Naziv
        {
            get { return naziv; }
            set { naziv = value; OnPropertyChanged("Naziv"); }
        }
     

        private int jedinicnaCena;

        public int JedinicnaCena
        {
            get { return jedinicnaCena; }
            set { jedinicnaCena = value; OnPropertyChanged("JedinicnaCena"); }
        }

        private int kolicina;

        public int Kolicina
        {
            get { return kolicina; }
            set { kolicina = value; OnPropertyChanged("Kolicina"); }
        }

        private bool obrisan;

        public bool Obrisan
        {
            get { return obrisan; }
            set { obrisan = value; OnPropertyChanged("Obrisan"); }
        }

        private TipNamestaja tipNamestaja;
        [XmlIgnore]
        public TipNamestaja TipNamestaja
        {
            get
            { if (tipNamestaja == null)
                {
                    tipNamestaja = TipNamestaja.GetById(tipNID);
                }
                return tipNamestaja;

            }
            set {
                tipNamestaja = value;
                tipNID = tipNamestaja.Id;
                OnPropertyChanged("TipNamestaja"); }

        }

        private int tipNID;

        public int TipNID
        {
            get { return tipNID; }
            set { tipNID = value; OnPropertyChanged("TipNID"); }
        }

        


        public int akcija;
     


        /*
        public override string ToString()
        {
            return $"Naziv: {Naziv},{JedinicnaCena},{TipNamestaja.GetById(TipNID).Naziv}";
        }*/

        protected void OnPropertyChanged  (string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public object Clone()
        {
            return new Namestaj()
            {
                Id = id,
                Naziv = naziv,
                JedinicnaCena = jedinicnaCena,
                TipNamestaja = tipNamestaja,
                TipNID = tipNID,
                Kolicina = kolicina,
                Obrisan = obrisan
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;


        public static Namestaj GetById(int id)
        {
            foreach (var namestaj in Projekat.Instance.namestaj)
            {
                if (namestaj.Id == id)
                {
                    return namestaj;
                }
            }
            return null;
        }
        #region Database
        public static ObservableCollection<Namestaj> GetAll()
        {
            var namestaj = new ObservableCollection<Namestaj>();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Namestaj WHERE Obrisan=@Obrisan";

                cmd.Parameters.AddWithValue("Obrisan", false);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();


                da.SelectCommand = cmd;
                da.Fill(ds, "Namestaj"); //Izvrsava se query nad bazom

                foreach (DataRow row in ds.Tables["Namestaj"].Rows)
                {
                    var n = new Namestaj();
                    n.Id = int.Parse(row["Id"].ToString());
                    n.TipNID = int.Parse(row["TipNID"].ToString());
                    n.Naziv = row["Naziv"].ToString();
                    n.JedinicnaCena = int.Parse(row["Cena"].ToString());
                    n.Kolicina = int.Parse(row["Kolicina"].ToString());
                    n.Obrisan = bool.Parse(row["Obrisan"].ToString());
                    
                    

                    namestaj.Add(n);

                }
            }
            return namestaj;
        }

        public static ObservableCollection<Namestaj> GetByTipNamestaja(TipNamestaja tipNamestaja)
        {
            var namestaj = new ObservableCollection<Namestaj>();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "select * from Namestaj where tipNID = @TipID and Obrisan=@Obrisan";

                cmd.Parameters.AddWithValue("TipID", tipNamestaja.Id);
                cmd.Parameters.AddWithValue("Obrisan", false);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();


                da.SelectCommand = cmd;
                da.Fill(ds, "Namestaj"); //Izvrsava se query nad bazom

                foreach (DataRow row in ds.Tables["Namestaj"].Rows)
                {
                    foreach(Namestaj nam in Projekat.Instance.namestaj)
                    { 
                        if(nam.Id == int.Parse(row["Id"].ToString()))
                        {
                            namestaj.Add(nam);
                        }
                    }
                    var n = new Namestaj();
                    n.Id = int.Parse(row["Id"].ToString());
                    n.TipNID = int.Parse(row["TipNID"].ToString());
                    n.Naziv = row["Naziv"].ToString();
                    n.JedinicnaCena = int.Parse(row["Cena"].ToString());
                    n.Kolicina = int.Parse(row["Kolicina"].ToString());
                    n.Obrisan = bool.Parse(row["Obrisan"].ToString());



                    namestaj.Add(n);

                }
            }
            return namestaj;
        }

        public static Namestaj GetDatabaseID(int id)
        {
            var namestaj = new ObservableCollection<Namestaj>();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Namestaj WHERE Id=@Id";

                cmd.Parameters.AddWithValue("Id", id);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();


                da.SelectCommand = cmd;
                da.Fill(ds, "Namestaj"); //Izvrsava se query nad bazom

                foreach (DataRow row in ds.Tables["Namestaj"].Rows)
                {
                    var n = new Namestaj();
                    n.Id = int.Parse(row["Id"].ToString());
                    n.TipNID = int.Parse(row["TipNID"].ToString());
                    n.Naziv = row["Naziv"].ToString();
                    n.JedinicnaCena = int.Parse(row["Cena"].ToString());
                    n.Kolicina = int.Parse(row["Kolicina"].ToString());
                    n.Obrisan = bool.Parse(row["Obrisan"].ToString());



                    namestaj.Add(n);

                }
            }
            return namestaj.First();
        }

        public static ObservableCollection<Namestaj> GetSearch(String param)
        {
            var namestaj = new ObservableCollection<Namestaj>();

            param = "%" + param + "%";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Namestaj n LEFT JOIN TipNamestaja tn ON n.tipNID = tn.id where tn.Naziv LIKE @Param OR n.Naziv LIKE @param";

                cmd.Parameters.AddWithValue("Param", param);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();


                da.SelectCommand = cmd;
                da.Fill(ds, "Namestaj"); //Izvrsava se query nad bazom

                foreach (DataRow row in ds.Tables["Namestaj"].Rows)
                {
                    var n = new Namestaj();
                    n.Id = int.Parse(row["Id"].ToString());
                    n.TipNID = int.Parse(row["TipNID"].ToString());
                    n.Naziv = row["Naziv"].ToString();
                    n.JedinicnaCena = int.Parse(row["Cena"].ToString());
                    n.Kolicina = int.Parse(row["Kolicina"].ToString());
                    n.Obrisan = bool.Parse(row["Obrisan"].ToString());



                    namestaj.Add(n);

                }
            }
            return namestaj;
        }

        public static Namestaj Create(Namestaj nam)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO Namestaj (Naziv,Cena,Kolicina,Obrisan,TipNID) VALUES (@Naziv,@JedinicnaCena,@Kolicina, @Obrisan,@TipNamestaja);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("Naziv", nam.Naziv);
                cmd.Parameters.AddWithValue("JedinicnaCena", nam.JedinicnaCena);
                cmd.Parameters.AddWithValue("Kolicina", nam.Kolicina);
                cmd.Parameters.AddWithValue("Obrisan", nam.Obrisan);
                cmd.Parameters.AddWithValue("TipNamestaja", nam.TipNID);

                int newId = int.Parse(cmd.ExecuteScalar().ToString()); //ExecuteScalar izvrsava query
                nam.Id = newId;
            }
            Projekat.Instance.namestaj.Add(nam); //azurirano i citanje modela
            return nam;
        }

        public static void Update(Namestaj nam)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE Namestaj SET Naziv = @Naziv, Cena=@JedinicnaCena, Kolicina=@Kolicina, Obrisan = @Obrisan, tipNID=@TipNamestaja WHERE Id =@Id";
                cmd.Parameters.AddWithValue("Id", nam.Id);
                cmd.Parameters.AddWithValue("Naziv", nam.Naziv);
                cmd.Parameters.AddWithValue("JedinicnaCena", nam.JedinicnaCena);
                cmd.Parameters.AddWithValue("Kolicina", nam.Kolicina);
                cmd.Parameters.AddWithValue("Obrisan", nam.Obrisan);
                cmd.Parameters.AddWithValue("TipNamestaja", nam.TipNID);

                cmd.ExecuteNonQuery(); //azuriranje stanje modela

                foreach (var namestaj in Projekat.Instance.namestaj)
                {
                    if (namestaj.Id == nam.Id)
                    {
                        namestaj.Naziv = nam.Naziv;
                       // namestaj.Naziv = nam.JedinicnaCena;
                        //namestaj.Naziv = nam.Kolicina;
                        namestaj.Obrisan = nam.Obrisan;
                        namestaj.TipNID = nam.TipNID;
                        break;
                    }
                }

            }

        }

        public static void Delete(Namestaj nam)
        {
            nam.Obrisan = true;
            Update(nam);
        }
        #endregion

    }
















}
