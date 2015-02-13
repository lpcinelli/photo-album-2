using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;


namespace Album_Photo
{
    class SerializeStuff
    {
        static public AlbumPhoto DeserializeFromXML(string file)
        {
            try
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(AlbumPhoto));
                TextReader textReader = new StreamReader(@file);

                AlbumPhoto myAlbum = (AlbumPhoto)deserializer.Deserialize(textReader);
                textReader.Close();
                if (myAlbum == null)
                    Console.WriteLine("aaaiia");

                return myAlbum;

            }
            catch { return null; }
        }

        static public void SerializeToXML(AlbumPhoto album, string file)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(AlbumPhoto));
            TextWriter textWriter = new StreamWriter(@file);
            serializer.Serialize(textWriter, album);
            textWriter.Close();
        }

    }
}
