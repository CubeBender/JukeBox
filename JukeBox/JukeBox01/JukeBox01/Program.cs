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

        ///////////////////////fffffff/////////////////////////////////////
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

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //DAVID'S SANDBOX - DONT TOUCH MY SAND!
            Console.WriteLine("#################### Trying editing methods ####################\r\n");

            string help = "1. print jukebox <all, author, name> or <-all, -author, -name>"
                    + "\r\n2. export <filename> or export jukebox <filename> - if you wont type filename, it will be automaticly named 'tmp'"
                    + "\r\n3. exit or quit";

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Type HELP or ? to show commands\r\n"
                + "Program automaticly deletes white spaces, type without or with spaces");
            Console.ResetColor();
            string filename = "tmp";
            bool isValid = false;
            while (!isValid)
            {
                Console.WriteLine("Command:");
                string input = Console.ReadLine();
                input = input.Replace(" ", "");

                Console.ForegroundColor = ConsoleColor.Green;
                if (input == "?" || input == "jukebox") input = "help";
                if (input.Contains("export"))
                {
                    filename = input.Replace("export", "");
                    filename = filename.Replace("jukebox", "");
                    input = "export";
                }
                switch (input.ToLowerInvariant())
                {
                    case "printjukebox":
                    case "printjukeboxall":
                    case "printjukebox-all":
                        Console.WriteLine("Jukebox:\r\n");
                        jukebox2.printJukeBox();
                        break;

                    case "printjukeboxname":
                    case "printjukebox-name":
                        Console.WriteLine("Jukebox Name: " + jukebox2.getJukeboxName());
                        break;

                    case "printjukeboxauthor":
                    case "printjukebox-author":
                        Console.WriteLine("Jukebox Author: " + jukebox2.getAuthorName());
                        break;

                    case "export":
                        Console.WriteLine("Jukebox has been exported as " + filename + ".xml!");
                        exportToXml(jukebox2, "Export\\", filename);
                        break;

                    case "help":
                        Console.WriteLine(help);
                        break;

                    case "exit":
                    case "quit":
                        Console.WriteLine("Exiting...");
                        System.Environment.Exit(0);
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Try again stupid...\r\n");
                        Console.ResetColor();
                        break;

                }
                Console.ResetColor();
            }
            Console.ReadLine();

            Console.WriteLine("\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n"
                + "Exit program with any key m8...");
            Console.ReadLine();
        }
    }
}