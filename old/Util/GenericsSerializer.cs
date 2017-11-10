using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_10.Util
{
    public class GenericsSerializer
    {
        public static void Serialize<T>(string fileName, T objToSerialize) where T : class
        {
            var serializer()
                try
            {
                var serializer = new XmlSerializer(typeof(T));
                using (var sw = new StreamWtriter($@"../../Data/{fileName}")) 
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

        public static List<T> DeSerialize<T>(string fileName,) where T : class 
        {
            var serializer()
                try
            {
                var serializer = new XmlSerializer(typeof(T));
                using (var sw = new StreamReader($@"../../Data/{fileName}"))
                {
                    return (List<T>)serializer.DeSerialize(sw); 
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
