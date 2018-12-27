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
    class Serializer
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream;

        protected void Serialize(object targetItem, string destination)
        {
            stream = new FileStream(destination, FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, targetItem);
            stream.Close();
        }

        protected object Deserialize(string sourcePath)
        {
            stream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read);
            object deserializedItem = formatter.Deserialize(stream);
            return deserializedItem;
        }
    }
}
