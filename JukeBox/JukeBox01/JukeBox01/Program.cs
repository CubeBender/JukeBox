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

        static JukeBox importFromXml(string path, string filename = Constants.EXPORTFILENAME)
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

        static void exportToXml(JukeBox jukebox, string path, string filename = Constants.EXPORTFILENAME)
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
        // PRINT COMMANDS

        // Printing results in g
        static void printResult(string text)
        {
            Console.ForegroundColor = Constants.RESULTCOLOR;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        static void printAlert(string text)
        {
            Console.ForegroundColor = Constants.ALERTCOLOR;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        static void printComment(string text)
        {
            Console.ForegroundColor = Constants.COMMENTCOLOR;
            Console.WriteLine(text);
            Console.ResetColor();
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
            JukeBox jukeboxinstance = importFromXml(Constants.LOCALPATH + Constants.DATAFOLDER, Constants.DATAFILENAME);

            bool exit = false;



            string help = "You need help bro..."
                    + "\r\n1. print jukebox <all, author, name> or <-all, -author, -name>"
                    + "\r\n2. export <filename> or export jukebox <filename> - if filename is not declared, default will be used"
                    + "\r\n3. exit or quit";
            
            //while (!isValid)
            //{
            //    Console.ForegroundColor = ConsoleColor.Green;
            //    if (input == "?" || input == "jukebox") input = "help";
            //    if (input.Contains("export"))
            //    {
            //        filename = input.Replace("export", "");
            //        filename = filename.Replace("jukebox", "");
            //        if (filename == "") { filename = Constants.EXPORTFILENAME; }
            //        input = "export";
            //    }

            //    if (input.Contains("change"))
            //    {
            //        string tmp = input.Replace("change", "");
            //        newJukeboxName = tmp;
            //        newAuthorName = tmp;
            //        if (input.Contains("name"))
            //        {
            //            newJukeboxName = newJukeboxName.Replace("jukeboxname", "");
            //            input = "changejukeboxname";
            //        }
            //        if (input.Contains("author"))
            //        {
            //            newAuthorName = newAuthorName.Replace("jukeboxauthor", "");
            //            input = "changejukeboxauthor";
            //        }
            //        if (newJukeboxName == "" || newAuthorName == "")
            //        {
            //            newJukeboxName = jukeboxinstance.getJukeboxName();
            //            newAuthorName = jukeboxinstance.getAuthorName();
            //        }


            //    }


            //    switch (input.ToLowerInvariant())
            //    {
            //        case "printjukebox":
            //        case "printjukeboxall":
            //        case "printjukebox-all":
            //            Console.WriteLine("Jukebox:\r\n");
            //            jukeboxinstance.printJukeBox();
            //            break;

            //        case "printjukeboxname":
            //        case "printjukebox-name":
            //            Console.WriteLine("Jukebox name: " + jukeboxinstance.getJukeboxName());
            //            break;

            //        case "printjukeboxauthor":
            //        case "printjukebox-author":
            //            Console.WriteLine("Jukebox author: " + jukeboxinstance.getAuthorName());
            //            break;

            //        case "changejukeboxname":
            //        case "changejukebox-name":
            //            Console.WriteLine("Jukebox name " + jukeboxinstance.getJukeboxName()
            //                + " has been changed to " + newJukeboxName + ".");
            //            break;

            //        case "changejukeboxauthor":
            //        case "changejukebox-author":
            //            Console.WriteLine("Jukebox author " + jukeboxinstance.getAuthorName()
            //                + " has been changed to " + newAuthorName + ".");
            //            break;

            //        case "export":
            //            Console.WriteLine("Jukebox has been exported as " + filename + ".xml!");
            //            exportToXml(jukeboxinstance, Constants.LOCALPATH + Constants.EXPORTPATH, filename);
            //            break;
            //    }
            //}


            do
            {
                // Console.Clear();
                Console.WriteLine("Command:");
                string[] input = (Console.ReadLine().Split(' '));
                
                switch (input[0].ToLowerInvariant())
                {
                    ////////////////////////////////////////////////////////////
                    // HELP
                    case "help":
                    case "?":
                        printComment(help);
                        break;

                    ////////////////////////////////////////////////////////////
                    // PRINT
                    case "print":
                        if (input.Length < 2)
                        {
                            printAlert("You must specify what you want to print!");
                            break;
                        }

                        switch (input[1].ToLowerInvariant())
                        {
                            case "all":
                            case "jukebox":
                                jukeboxinstance.printJukeBox(Constants.RESULTCOLOR);
                                break;

                            case "album":
                                if (input.Length >= 3)
                                {
                                    // Join the rest of arguments into string
                                    string expression = input[2];
                                    int i = 3;
                                    while (i < input.Length)
                                    {
                                        expression = expression + " " + input[i];
                                        i++;
                                    }
                                    // Use the Joint argument to find the album
                                    Album album = jukeboxinstance.searchAlbumByName(expression);
                                    // If no album was found, we print alert
                                    if ( album != null)
                                    {
                                        album.printAlbum(Constants.RESULTCOLOR);
                                        break;
                                    }
                                    else { printAlert("No matches found for \"" + expression + "\"!"); }
                                }
                                // If no argument was given, print alert
                                else { printAlert("You must specify the name of the album!"); }
                                break;

                            case "song":
                                if (input.Length >= 3)
                                {
                                    // Join the rest of arguments into string
                                    string expression = input[2];
                                    int i = 3;
                                    while (i < input.Length)
                                    {
                                        expression = expression + " " + input[i];
                                        i++;
                                    }
                                    // Use the Joint argument to find the song
                                    Song song = jukeboxinstance.searchSongByName(expression);
                                    if (song != null)
                                    {
                                        song.printSongAll(Constants.RESULTCOLOR);
                                        break;
                                    }
                                    else { printAlert("No matches found for \"" + expression + "\"!"); }
                                }
                                // If no argument was given, print alert
                                else { printAlert("You must specify the name of the song!"); }
                                break;

                            default:
                                printAlert("Cannot find command \"" + input[1] + "\". Type \"help\" or \"?\" for list of valid commands.");
                                break;
                        }
                        break;
                    ////////////////////////////////////////////////////////////
                    // IMPORT

                    ////////////////////////////////////////////////////////////
                    // EXPORT

                    ////////////////////////////////////////////////////////////
                    // JUKEBOX

                    ////////////////////////////////////////////////////////////
                    // ALBUM

                    ////////////////////////////////////////////////////////////
                    // SONG

                    ////////////////////////////////////////////////////////////
                    // TERMINATION
                    case "exit":
                    case "quit":
                    case "close":
                    case "terminate":
                        exit = true;
                        break;

                    ////////////////////////////////////////////////////////////
                    // DEFAULT
                    default:
                        printAlert("Cannot find command \"" + input[0] + "\". Type \"help\" or \"?\" for list of valid commands.");
                        break;
                }

                Console.ResetColor();

            } while (!exit);

            // Printing loaded data
            jukeboxinstance.printJukeBox();
            // Saving instance data

            Console.WriteLine("Saving data!");
            exportToXml(jukeboxinstance, Constants.LOCALPATH + Constants.DATAFOLDER, Constants.DATAFILENAME);
            Console.WriteLine("Press any key to exit the JukeBox.");

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
        }
    }
}