﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace POP_10.Util
{
    public class GenericsSerializer
    {
        public static void Serialize<T>(string fileName, ObservableCollection<T> objToSerialize) where T : class
        {
           
                try
            {
                var serializer = new XmlSerializer(typeof(ObservableCollection<T>));
                using (var sw = new StreamWriter($@"../../Data/{fileName}")) 
                {
                    serializer.Serialize(sw, objToSerialize);
                }
                     //u trenutku kad streamwriter radi, niko drugi nema pristup zato se koristi using keyword iznad
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static ObservableCollection<T> Deserialize<T>(string fileName) where T : class 
        {
            

                try
            {
                var serializer = new XmlSerializer(typeof(ObservableCollection<T>));
                using (var sw = new StreamReader($@"../../Data/{fileName}"))
                {
                    return (ObservableCollection<T>)serializer.Deserialize(sw); 
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
