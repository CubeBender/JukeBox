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

        ////////////////////////////////////////////////////////////
        // IMPORT / EXPORT FUNCTIONS

        static JukeBox importFromXml(string path, string filename = "export")
        {
            // Set path to program directory if it is not specified
            if (path == "") { path = System.AppDomain.CurrentDomain.BaseDirectory; }
            // Construct a serializer and set the type
            var serializer = new XmlSerializer(typeof(JukeBox));
            // Prepare readed data thru text stream
            TextReader reader = new StreamReader(@"" + path + "\\" + filename + ".xml");
            // Deserialize and return imported data
            // Conversion of data is necessary and saving to variable as well, since we need to close the reader before returning the values
            JukeBox import = (JukeBox)serializer.Deserialize(reader);
            // This is the necessary closing of stream. If we do not close, the file is still in use and we can't access it for saving the data at the end.
            reader.Close();
            return import;
        }

        static void exportToXml(JukeBox jukebox, string path, string filename = "export")
        {
            // Set path to program directory if it is not specified
            if (path == "") { path = System.AppDomain.CurrentDomain.BaseDirectory; }
            // Construct a serializer and set the type
            var serializer = new XmlSerializer(typeof(JukeBox));
            // Prepare write data thru text stream
            TextWriter writer = new StreamWriter(@"" + path + "\\" + filename + ".xml");
            // Serialize the specified data to file
            serializer.Serialize(writer, jukebox);
            // Manually closing the writer might be necessary!
            writer.Close();
        }

        ////////////////////////////////////////////////////////////
        // INSTANCE INITIALIZATION

        // TO DO ?


        // *************************************************
        // **************** Main program *******************
        // *************************************************
        static void Main(string[] args)
        {
            // Testing XML Perzistance

            // Loading saved data from last instance
            JukeBox jukeboxinstance = importFromXml(Constants.LOCALPATH + Constants.DATAPATH, Constants.EXPORTFILENAME);

            bool exit = false;

            do
            {
                // Console.Clear();
                Console.WriteLine("Press \"A\"");
                char imput = Console.ReadKey().KeyChar;


                switch (imput)
                {
                    case 'a':
                    case 'A':
                        Console.WriteLine(" is a VALID imput!");
                        break;
                    case 'x':
                    case 'X':
                        Console.WriteLine(" is a deadly spell! You have escaped!");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine(" is NOT a VALID imput!");
                        break;

                }

            } while (!exit);

            // Printing loaded data
            jukeboxinstance.printJukeBox();
            // Saving instance data
            exportToXml(jukeboxinstance, Constants.LOCALPATH + Constants.DATAPATH, Constants.EXPORTFILENAME);

            //// Testing data imput
            //Song song1 = new Song("Jsi moje mama", 183, "Jsi moje mama, moje mama, kterou ja mam nadevse rad. Jsi jak kouzelna vila, co mi dava chut se smat!");
            //Album album1 = new Album("Mama", "Lunetic", "pop", 1996);
            //album1.addSong(song1);
            //JukeBox jukebox1 = new JukeBox("TomiJukebox", "CubeBender");
            //jukebox1.addAlbum(album1);

            //album1.addSong("Jsi moje tata", 194, "Jsi moje tata, moje tata, kterou ja mam nadevse rad. Jsi jak kouzelny vil, co mi dava chut se smat!");
            //jukebox1.albums[0].addSong("Jsi moje sestra", 162, "Jsi moje sestra, moje sestra, kterou ja mam nadevse rad. Jsi jak kouzelna dcera vily, co mi dava chut se smat");
            //jukebox1.albums[0].addSong(song1);

            //jukebox1.printJukeBox();
            //song1.printSongAll();

            //// Testing data export
            //exportToXml(jukebox1, "C:\\Export", "export");

            //// Testing data import
            //JukeBox jukebox2 = new JukeBox();
            //jukebox2 = importFromXml("C:\\Export", "dummy");
            //jukebox2.printJukeBox();

            //// Testing imported data export
            //exportToXml(jukebox2, "C:\\Export", "jukebox2");

            //// Path to executable
            //Console.WriteLine(System.AppDomain.CurrentDomain.BaseDirectory);

            Console.ReadKey();

            ///////TESTÍČEK :D
        }
    }
}