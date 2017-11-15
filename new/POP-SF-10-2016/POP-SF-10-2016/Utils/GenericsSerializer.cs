using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace POP_10.Util
{
    public class GenericsSerializer
    {
        public static void Serialize<T>(string fileName, List<T> objToSerialize) where T : class
        {
           
                try
            {
                var serializer = new XmlSerializer(typeof(List<T>));
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

        public static List<T> Deserialize<T>(string fileName) where T : class 
        {
            

                try
            {
                var serializer = new XmlSerializer(typeof(List<T>));
                using (var sw = new StreamReader($@"../../Data/{fileName}"))
                {
                    return (List<T>)serializer.Deserialize(sw); 
                }
                
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
