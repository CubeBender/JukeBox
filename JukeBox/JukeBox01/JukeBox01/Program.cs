using System;
using System.Collections.Generic;
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
            if (path == "") { path = System.AppDomain.CurrentDomain.BaseDirectory + Constants.EXPORTFOLDER; }
            // Construct a serializer and set the type
            var serializer = new XmlSerializer(typeof(JukeBox));
            // Prepare readed data thru text stream
            try
            {
                TextReader reader = new StreamReader(@"" + path + "\\" + filename + ".xml");
                // Deserialize and return imported data
                // Conversion of data is necessary and saving to variable as well, since we need to close the reader before returning the values
                JukeBox import = (JukeBox)serializer.Deserialize(reader);
                // This is the necessary closing of stream. If we do not close, the file is still in use and we can't access it for saving the data at the end.
                reader.Close();
                return import;
            }
            catch (UnauthorizedAccessException)
            {
                printAlert("You do not have permission to access this file!");
                return null;
            }
        }

        static bool exportToXml(JukeBox jukebox, string path, string filename = Constants.EXPORTFILENAME)
        {
            try
            {
                System.IO.Directory.CreateDirectory(path);
                // Set path to program directory if it is not specified
                if (path == "") { path = System.AppDomain.CurrentDomain.BaseDirectory + Constants.EXPORTFOLDER; }
                // Construct a serializer and set the type
                var serializer = new XmlSerializer(typeof(JukeBox));
                // Prepare write data thru text stream
                TextWriter writer = new StreamWriter(@"" + path + "\\" + filename + ".xml");
                // Serialize the specified data to file
                serializer.Serialize(writer, jukebox);
                // Manually closing the writer might be necessary!
                writer.Close();
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                printAlert("You do not have permission to access this folder!");
                return false;
            }
        }

        ////////////////////////////////////////////////////////////
        // PRINT COMMANDS

        // Printing results in g
        static void printSuccess(string text = "Unknown success!")
        {
            Console.ForegroundColor = Constants.SUCCESSCOLOR;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        static void printResult(string text = "Empty result!")
        {
            Console.ForegroundColor = Constants.RESULTCOLOR;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        static void printAlert(string text = "Unknown Alert!")
        {
            Console.ForegroundColor = Constants.ALERTCOLOR;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        static void printComment(string text = "No comment!")
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
            ////////////////////////////////////////////////////////////
            // FILE AND FILEPATH INITIALIZATION


            // Setting default instance data file and path
            string instanceFileName = Constants.DATAFILENAME;
            string instanceFilePath = Constants.LOCALPATH + Constants.DATAFOLDER;
            string exportFilePath = Constants.LOCALPATH + Constants.EXPORTFOLDER;

            // Creating directories
            System.IO.Directory.CreateDirectory(instanceFilePath);
            System.IO.Directory.CreateDirectory(exportFilePath);
            
            // Loading saved data from default instance
            JukeBox jukeboxinstance = importFromXml(instanceFilePath, instanceFileName);
            

            string help = "You need help bro..."
                    + "\n1. print jukebox <all, author, name> or <-all, -author, -name>"
                    + "\n2. export <filename> or export jukebox <filename> - if filename is not declared, default will be used"
                    + "\n3. exit or quit";
         

            bool exit = false;
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
                            ////////////////////////////////////////////////////////////
                            // PRINT ALL
                            case "all":
                            case "jukebox":
                                jukeboxinstance.printJukeBox(Constants.RESULTCOLOR);
                                break;

                            case "album":
                                if(input.Length < 3)
                                {
                                    printAlert("You must specify by what you want to filter!");
                                    break;
                                }
                                switch (input[2].ToLowerInvariant())
                                {
                                    ////////////////////////////////////////////////////////////
                                    // PRINT ALBUM NAME
                                    case "name":
                                        if (input.Length >= 4)
                                        {
                                    // Join the rest of arguments into string
                                            string expression = input[3];
                                            int i = 4;
                                    while (i < input.Length)
                                    {
                                        expression = expression + " " + input[i];
                                        i++;
                                    }
                                            // Use the Joint argument to find the album(s)
                                            List<Album> albumList = jukeboxinstance.searchAlbumByName(expression);
                                    // If no album was found, we print alert
                                            if (albumList.Count > 0)
                                    {
                                                foreach (Album album in albumList)
                                                {
                                        album.printAlbum(Constants.RESULTCOLOR);
                                                }
                                        break;
                                    }
                                    else { printAlert("No matches found for \"" + expression + "\"!"); }
                                }
                                // If no argument was given, print alert
                                else { printAlert("You must specify the name of the album!"); }
                                break;

                                    ////////////////////////////////////////////////////////////
                                    // PRINT ALBUM ARTIST
                                    case "artist":
                                        if (input.Length >= 4)
                                        {
                                            // Join the rest of arguments into string
                                            string expression = input[3];
                                            int i = 4;
                                            while (i < input.Length)
                                            {
                                                expression = expression + " " + input[i];
                                                i++;
                                            }
                                            // Use the Joint argument to find the album(s)
                                            List<Album> albumList = jukeboxinstance.searchAlbumByArtist(expression);
                                            // If no album was found, we print alert
                                            if (albumList.Count > 0)
                                            {
                                                foreach (Album album in albumList)
                                                {
                                                    album.printAlbum(Constants.RESULTCOLOR);
                                                }
                                                break;
                                            }
                                            else { printAlert("No matches found for \"" + expression + "\"!"); }
                                        }
                                        // If no argument was given, print alert
                                        else { printAlert("You must specify the name of the artist!"); }
                                        break;

                                    ////////////////////////////////////////////////////////////
                                    // PRINT ALBUM YEAR
                                    case "year":
                                        if (input.Length >= 4)
                                        {
                                            string expression = input[3];
                                            List<Album> albumList = jukeboxinstance.searchAlbumByYear(expression);
                                            // If no album was found, we print alert
                                            if (albumList.Count > 0)
                                            {
                                                foreach (Album album in albumList)
                                                {
                                                    album.printAlbum(Constants.RESULTCOLOR);
                                                }
                                                break;
                                            }
                                            else { printAlert("No matches found for \"" + expression + "\"!"); }
                                        }
                                        // If no argument was given, print alert
                                        else { printAlert("You must specify the name of the artist!"); }
                                        break;

                                    ////////////////////////////////////////////////////////////
                                    // PRINT ALBUM DEFAULT
                                    default:
                                        printAlert("Cannot find command \"" + input[2] + "\". Type \"help\" or \"?\" for list of valid commands.");
                                        break;
                                }
                                break;
                            ////////////////////////////////////////////////////////////
                            // PRINT SONG
                            case "song":
                                if (input.Length < 3)
                                {
                                    printAlert("You must specify by what you want to filter!");
                                    break;
                                }
                                switch (input[2].ToLowerInvariant())
                                {
                                    ////////////////////////////////////////////////////////////
                                    // PRINT SONG NAME
                                    case "name":
                                        if (input.Length >= 4)
                                {
                                    // Join the rest of arguments into string
                                            string expression = input[3];
                                            int i = 4;
                                    while (i < input.Length)
                                    {
                                        expression = expression + " " + input[i];
                                        i++;
                                    }
                                            // Use the Joint argument to find the song(s)
                                            List<Song> songList = jukeboxinstance.searchSongByName(expression);
                                            if (songList.Count > 0)
                                            {
                                                foreach (Song song in songList)
                                    {
                                                    song.printSong(Constants.RESULTCOLOR);
                                                }
                                        break;
                                    }
                                    else { printAlert("No matches found for \"" + expression + "\"!"); }
                                }
                                // If no argument was given, print alert
                                else { printAlert("You must specify the name of the song!"); }
                                break;

                                    ////////////////////////////////////////////////////////////
                                    // PRINT SONG ARTIST
                                    case "artist":
                                        if (input.Length >= 4)
                                        {
                                            // Join the rest of arguments into string
                                            string expression = input[3];
                                            int i = 4;
                                            while (i < input.Length)
                                            {
                                                expression = expression + " " + input[i];
                                                i++;
                                            }
                                            // Use the Joint argument to find the song(s)
                                            List<Song> songList = jukeboxinstance.searchSongByArtist(expression);
                                            if (songList.Count > 0)
                                            {
                                                foreach (Song song in songList)
                                                {
                                                    song.printSong(Constants.RESULTCOLOR);
                                                }
                                                break;
                                            }
                                            else { printAlert("No matches found for \"" + expression + "\"!"); }
                                        }
                                        // If no argument was given, print alert
                                        else { printAlert("You must specify the artist of the song!"); }
                                        break;

                                    ////////////////////////////////////////////////////////////
                                    // PRINT SONG YEAR
                                    case "year":
                                        if (input.Length >= 4)
                                        {
                                            string expression = input[3];
                                            List<Song> songList = jukeboxinstance.searchSongByYear(expression);
                                            if (songList.Count > 0)
                                            {
                                                foreach (Song song in songList)
                                                {
                                                    song.printSong(Constants.RESULTCOLOR);
                                                }
                                                break;
                                            }
                                            else { printAlert("No matches found for \"" + expression + "\"!"); }
                                        }
                                        // If no argument was given, print alert
                                        else { printAlert("You must specify the year of the song!"); }
                                        break;

                                    ////////////////////////////////////////////////////////////
                                    // PRINT SONG LENGTH
                                    case "length":
                                        if (input.Length >= 4)
                                        {
                                            string expression = input[3];
                                            List<Song> songList = jukeboxinstance.searchSongByLength(expression);
                                            if (songList.Count > 0)
                                            {
                                                foreach (Song song in songList)
                                                {
                                                    song.printSong(Constants.RESULTCOLOR);
                                                }
                                                break;
                                            }
                                            else { printAlert("No matches found for \"" + expression + "\"!"); }
                                        }
                                        // If no argument was given, print alert
                                        else { printAlert("You must specify the length of the song!"); }
                                        break;

                                    ////////////////////////////////////////////////////////////
                                    // PRINT SONG DEFAULT
                                    default:
                                        printAlert("Cannot find command \"" + input[2] + "\". Type \"help\" or \"?\" for list of valid commands.");
                                        break;
                                }
                                break;

                            ////////////////////////////////////////////////////////////
                            // PRINT DEFAULT
                            default:
                                printAlert("Cannot find command \"" + input[1] + "\". Type \"help\" or \"?\" for list of valid commands.");
                                break;
                        }
                        break;
                        
                    ////////////////////////////////////////////////////////////
                    // CHANGE
                    case "change":
                        if (input.Length != 4 && input.Length < 4)
                        {
                            printAlert("You must specify what you want to change and a new name!");
                            break;
                        }
                        if (input.Length != 4 && input.Length > 4)
                        {
                            printAlert("Too many arguments!");
                            break;
                        }
                        else if (input[3] == "") { input[3] = "Unknown"; }
                        switch (input[1].ToLowerInvariant())
                        {
                            case "jukebox":
                                switch (input[2].ToLowerInvariant())
                                {
                                    case "name":
                                        string oldJukeboxName = jukeboxinstance.getJukeboxName();
                                        string newJukeboxName = input[3];
                                        jukeboxinstance.changeJukeboxName(newJukeboxName);
                                        printSuccess("Jukebox \"" + oldJukeboxName + "\" has been renamed as \"" + newJukeboxName + "\".");
                                        break;

                                    case "author":
                                        string oldAuthorName = jukeboxinstance.getAuthorName();
                                        string newAuthorName = input[3];
                                        jukeboxinstance.changeAuthorName(newAuthorName);
                                        printSuccess("Author \"" + oldAuthorName + "\" has been changed to \"" + newAuthorName + "\".");
                                        break;

                                    default:
                                        printAlert("Cannot find command \"" + input[2] + "\". Type \"help\" or \"?\" for list of valid commands.");
                                        break;
                                }
                                break;

                            default:
                                printAlert("Cannot find command \"" + input[1] + "\". Type \"help\" or \"?\" for list of valid commands.");
                                break;
                        }
                        break;
                    ////////////////////////////////////////////////////////////
                    // IMPORT
                    case "import":
                    case "load":
                        if (input.Length < 2)
                        {
                            printAlert("You must specify what to import!");
                            break;
                        }

                        break;

                    ////////////////////////////////////////////////////////////
                    // EXPORT
                    case "export":
                    case "saveas":
                        if (input.Length < 2)
                        {
                            printAlert("You must specify the name of the file!");
                            break;
                        }
                        else if (input.Length < 3)
                        {
                            if (exportToXml(jukeboxinstance, exportFilePath, input[1]))
                            {
                                printSuccess("File successully exported as \"" + input[1] + "\" to " + Constants.LOCALPATH + Constants.EXPORTFOLDER);
                            }
                            break;
                        }
                        else if (input.Length < 4)
                        {
                            try
                            {
                                System.IO.Directory.CreateDirectory(input[2]);
                            }
                            catch (UnauthorizedAccessException)
                            {
                                printAlert("You do not have permition to access this directory!");
                                break;
                            }
                            if (exportToXml(jukeboxinstance, input[2], input[1]))
                            {
                                printSuccess("File successully exported as \"" + input[1] + ".xml\" to " + input[2]);
                            }
                            break;
                        }
                        else
                        {
                            printAlert("Too many arguments!");
                        }
                        break;

                    ////////////////////////////////////////////////////////////
                    // OPEN
                    case "open":
                        if (input.Length < 2)
                        {
                            printAlert("You must specify the name of the file!");
                            break;
                        }
                        else if (input.Length < 3)
                        {
                            instanceFileName = input[1];
                            instanceFilePath = Constants.LOCALPATH + Constants.EXPORTFOLDER;
                            jukeboxinstance = importFromXml(instanceFilePath, instanceFileName);
                            if (jukeboxinstance == null)
                            {
                                printAlert("No data was loaded! File is empty or doesn't exist!");
                                break;
                            }
                            printSuccess("Successully loaded file \"" + instanceFileName + "\" from " + instanceFilePath);
                            break;
                        }
                        else if (input.Length < 4)
                        {
                            instanceFileName = input[1];
                            instanceFilePath = input[2];
                            jukeboxinstance = importFromXml(instanceFilePath, instanceFileName);
                            if (jukeboxinstance == null)
                            {
                                printAlert("No data was loaded! File is empty or doesn't exist!");
                                break;
                            }
                            printSuccess("Successully loaded file \"" + instanceFileName + "\" from " + instanceFilePath);
                            break;
                        }
                        else
                        {
                            printAlert("Too many arguments!");
                        }
                        break;

                    ////////////////////////////////////////////////////////////
                    // SAVE
                    case "save":
                        if (exportToXml(jukeboxinstance, instanceFilePath, instanceFileName))
                        {
                            printSuccess("Data saved to \"" + instanceFileName + ".xml\" at " + instanceFilePath);
                        }
                        break;

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
                        // Quicker way of exiting without saving
                        // Use "exit nosave" command
                        if (input.Length > 1)
                        {
                            if (input[1] == "nosave" || input[1] == "ns" )
                            {
                                Environment.Exit(0);
                            }
                        }
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
            // jukeboxinstance.printJukeBox();
            // Saving instance data
            exit = false;
            do
            {
                Console.WriteLine("Do you want to save? Y/N");
                char key = Console.ReadKey().KeyChar;
                if (key == 'y' || key == 'Y')
                {
                    Console.WriteLine("\nSaving data!");
                    exportToXml(jukeboxinstance, instanceFilePath, instanceFileName);
                    printSuccess("\nData saved! Legit!");
                    exit = true;
                }
                else if (key == 'n' || key == 'N')
                {
                    printAlert("\nExiting without saving!");
                    exit = true;
                }
            } while (exit != true);
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