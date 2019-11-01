using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace GameLinker.Helpers
{
    public class Serializer
    {
        static IFormatter formatter = new BinaryFormatter();
        static Stream stream;

        protected static void Serialize(object targetItem, string destination)
        {
            stream = new FileStream(destination, FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, targetItem);
            stream.Close();
        }

        protected static object Deserialize(string sourcePath)
        {
            stream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read);
            object deserializedItem = formatter.Deserialize(stream);
            stream.Close();
            return deserializedItem;
        }

        protected static object DeserializeBytes(byte[] data)
        {
            stream = new MemoryStream(data);
            object deserializedItem = formatter.Deserialize(stream);
            stream.Close();
            return deserializedItem;
        }
    }
}
