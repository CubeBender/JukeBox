using System;
using System.IO;
using System.Xml.Serialization;

namespace JukeBox01
{
    // *************************************************
    // *************** Program Class *******************
    // *************************************************
    class Program
    {
        static JukeBox importFromXml(string path, string filename = "export")
        {
            // Set path to program directory if it is not specified
            if (path == "") { path = System.AppDomain.CurrentDomain.BaseDirectory; }
            // Construct a serializer and set the type
            var serializer = new XmlSerializer(typeof(JukeBox));
            // Prepare readed data thru text stream
            TextReader reader = new StreamReader(@"" + path + filename + ".xml");
            // Deserialize and return imported data
            // Conversion of data is necessary!
            return (JukeBox)serializer.Deserialize(reader);
        }

        static void exportToXml(JukeBox jukebox, string path, string filename = "export")
        {
            // Set path to program directory if it is not specified
            if (path == "") { path = System.AppDomain.CurrentDomain.BaseDirectory; }
            // Construct a serializer and set the type
            var serializer = new XmlSerializer(typeof(JukeBox));
            // Prepare write data thru text stream
            TextWriter writer = new StreamWriter(@"" + path + filename + ".xml");
            // Serialize the specified data to file
            serializer.Serialize(writer, jukebox);
        }
        // *************************************************
        // **************** Main program *******************
        // *************************************************
        static void Main(string[] args)
        {
            // Testing data imput
            Song song1 = new Song("Jsi moje mama", 183, "Jsi moje mama, moje mama, kterou ja mam nadevse rad. Jsi jak kouzelna vila, co mi dava chut se smat!");
            Album album1 = new Album("Mama", "Lunetic", "pop", 1996);
            album1.addSong(song1);
            JukeBox jukebox1 = new JukeBox("TomiJukebox", "CubeBender");
            jukebox1.addAlbum(album1);

            album1.addSong("Jsi moje tata", 194, "Jsi moje tata, moje tata, kterou ja mam nadevse rad. Jsi jak kouzelny vil, co mi dava chut se smat!");
            jukebox1.albums[0].addSong("Jsi moje sestra", 162, "Jsi moje sestra, moje sestra, kterou ja mam nadevse rad. Jsi jak kouzelna dcera vily, co mi dava chut se smat");
            jukebox1.albums[0].addSong(song1);

            jukebox1.printJukeBox();
            song1.printSongAll();

            // Testing data export
            exportToXml(jukebox1, "C:\\Export\\", "export");

            // Testing data import
            JukeBox jukebox2 = new JukeBox();
            jukebox2 = importFromXml("C:\\Export\\", "dummy");
            jukebox2.printJukeBox();

            // Testing imported data export
            exportToXml(jukebox2, "C:\\Export\\", "jukebox2");

            // Path to executable
            Console.WriteLine(System.AppDomain.CurrentDomain.BaseDirectory);

            Console.ReadKey();
        }
    }
}