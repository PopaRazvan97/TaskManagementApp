using System.IO;
using System.Xml.Serialization;

namespace Tema_2.Utils
{
    public static class FileUtils
    {
        public static void SerializeToFile(string filename, object obj)
        {
            using (var stream = new FileStream(filename, FileMode.Create))
            {
                var serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(stream, obj);
            }
        }

        public static T DeserializeFromFile<T>(string filename)
        {
            using (var stream = new FileStream(filename, FileMode.Open))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stream);
            }
        }
    }
}