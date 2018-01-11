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
    
    public class DodatnaUsluga : INotifyPropertyChanged

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

        private double cena;

        public double Cena
        {
            get { return cena; }
            set { cena = value; OnPropertyChanged("Cena"); }
        }

        private bool obrisan;

        public bool Obrisan
        {
            get { return obrisan; }
            set { obrisan = value; }
        }

        public object Clone()
        {
            return new DodatnaUsluga()
            {
                Id = id,
                Naziv = naziv,
                Cena = cena,
                Obrisan = obrisan

            };
        }
        
        public static DodatnaUsluga GetById(int Id)
        {
            foreach (var usluga in Projekat.Instance.dodatnaUsluga)
            {
                if(usluga.Id == Id)
                {
                    return usluga;
                }
            }
            return null;
        }

        public override string ToString()
        {
            return $"{Naziv}";
        }



        public event PropertyChangedEventHandler PropertyChanged;
        
        protected void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region Database
        public static ObservableCollection<DodatnaUsluga> GetAll()
        {
            var dodatnaUsluga = new ObservableCollection<DodatnaUsluga>();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM DodatnaUsluga WHERE Obrisan=@Obrisan";

                cmd.Parameters.AddWithValue("Obrisan", false);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();


                da.SelectCommand = cmd;
                da.Fill(ds, "DodatnaUsluga"); //Izvrsava se query nad bazom

                foreach (DataRow row in ds.Tables["DodatnaUsluga"].Rows)
                {
                    var du = new DodatnaUsluga();
                    du.Id = int.Parse(row["Id"].ToString());
                    du.Naziv = row["Naziv"].ToString();
                    du.Cena = double.Parse(row["Cena"].ToString());
                    du.Obrisan = bool.Parse(row["Obrisan"].ToString());

                    dodatnaUsluga.Add(du);

                }
            }
            return dodatnaUsluga;
        }

        public static DodatnaUsluga Create(DodatnaUsluga du)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO DodatnaUsluga (Naziv,Cena,Obrisan) VALUES (@Naziv,@Cena, @Obrisan);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("Naziv", du.Naziv);
                cmd.Parameters.AddWithValue("Cena", du.Cena);
                cmd.Parameters.AddWithValue("Obrisan", du.Obrisan);

                int newId = int.Parse(cmd.ExecuteScalar().ToString()); //ExecuteScalar izvrsava query
                du.Id = newId;
            }
            Projekat.Instance.dodatnaUsluga.Add(du); //azurirano i citanje modela
            return du;
        }

        public static void Update(DodatnaUsluga du)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE DodatnaUsluga SET Naziv = @Naziv,Cena = @Cena, Obrisan = @Obrisan WHERE Id =@Id";
                cmd.Parameters.AddWithValue("Id", du.Id);
                cmd.Parameters.AddWithValue("Naziv", du.Naziv);
                cmd.Parameters.AddWithValue("Cena", du.Cena);
                cmd.Parameters.AddWithValue("Obrisan", du.Obrisan);

                cmd.ExecuteNonQuery(); //azuriranje stanje modela

                foreach (var dodatnaUsluga in Projekat.Instance.dodatnaUsluga)
                {
                    if (dodatnaUsluga.Id == du.Id)
                    {
                        dodatnaUsluga.Naziv = du.Naziv;
                        dodatnaUsluga.Naziv = du.Naziv;
                        dodatnaUsluga.Obrisan = du.Obrisan;
                        break;
                    }
                }

            }

        }

        public static void Delete(DodatnaUsluga du)
        {
            du.Obrisan = true;
            Update(du);
        }
        #endregion
    }
}
