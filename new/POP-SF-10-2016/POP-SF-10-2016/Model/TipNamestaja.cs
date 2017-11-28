using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_10.Model
{
    [Serializable]
    public class TipNamestaja : INotifyPropertyChanged
    {

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        private bool obrisan;

        public bool Obrisan
        {
            get { return obrisan; }
            set { obrisan = value; OnPropertyChanged("Obrisan"); }
        }

        private string naziv;

        public string Naziv
        {
            get { return naziv; }
            set { naziv = value; OnPropertyChanged("Naziv"); }
        }


        public static TipNamestaja GetById(int id)
        {
            foreach (var tipNamestaja in Projekat.Instance.tipNam)
            {
                if (tipNamestaja.Id == id)
                {
                    return tipNamestaja;
                } 
        }
        return null;
    }

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            } 
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
