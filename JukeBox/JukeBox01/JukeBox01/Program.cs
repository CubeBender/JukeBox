using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
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

        // Import
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
            catch (FileNotFoundException)
            {
                printAlert("No data was imported! File doesn't exist!");
                return null;
            }
            catch (UnauthorizedAccessException)
            {
                printAlert("You do not have permission to access this file!");
                return null;
            }
        }
        // Export
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

        // Printing results in specific color. Colors are deternimed in Constants class
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

        // *************************************************
        // **************** Main program *******************
        // *************************************************
        static void Main(string[] args)
        {
            ////////////////////////////////////////////////////////////
            // FILE AND FILEPATH INITIALIZATION

            /* Making tmp file for exiting without opened file but want to save file.
            string tmpFile = "tmp_" + DateTime.Now.ToString("G");
            tmpFile = tmpFile.Replace(":", "");
            tmpFile = tmpFile.Replace(" ", "_");
            */
            // Creating instance variables' dummys
            string dummyinstancefilename = null;
            string dummyinstancefilepath = null;

            // Setting default instance data file and path
            string instancefilename = Constants.DATAFILENAME;
            string instancefilepath = Constants.LOCALPATH + Constants.DATAFOLDER;
            string exportFilePath = Constants.LOCALPATH + Constants.EXPORTFOLDER;

            // Creating directories
            System.IO.Directory.CreateDirectory(instancefilepath);
            System.IO.Directory.CreateDirectory(exportFilePath);

            // Creating instance dummy
            JukeBox dummyjukeboxinstance = null;
            JukeBox jukeboxinstance = null;

            // Loading saved data from default instance
            dummyjukeboxinstance = importFromXml(instancefilepath, instancefilename);

            // Creating new instance of jukebox if we load null
            if (dummyjukeboxinstance == null)
            {
                printAlert("No previous instance found or file is empty. Creating a new empty instance!");
                printResult("What is your name?");
                string jukeboxName = Console.ReadLine();
                printResult("What is the name of this JukeBox?");
                string jukeboxAuthor = Console.ReadLine();
                jukeboxinstance = new JukeBox(jukeboxName, jukeboxAuthor);
                exportToXml(jukeboxinstance, instancefilepath, instancefilename);
                printResult("New instance created! Rebooting JukeBox!");
                Thread.Sleep(3000);
                Console.Clear();
            }
            // If load is successfull, continue with jukeboxinstance
            else { jukeboxinstance = dummyjukeboxinstance; }
            
            printComment( "Instance file: " + instancefilename + ".xml" 
                + "\nJukebox name: " + jukeboxinstance.getJukeboxName()
                + "\nCreated by: " + jukeboxinstance.getAuthorName());

            string help = "Now usable and working commands:"
                    + "\n1. print"
                    + "\n   a) print all"
                    + "\n       - print jukebox"
                    + "\n       - print jukebox all"
                    + "\n   b) print album"
                    + "\n       - print album name <name>"
                    + "\n       - print album artist <artist>"
                    + "\n       - print album year <year>"
                    + "\n       - print album genre <genre>"
                    + "\n   c) print song"
                    + "\n       - print song name <name>"
                    + "\n       - print song artist <artist>"
                    + "\n       - print song year <year>"
                    + "\n       - print song genre <genre>"
                    + "\n       - print song length <length> - in seconds"
                    + "\n2. add/create"
                    + "\n   a) add/create album"
                    + "\n       - add album <name>, create album <name>"
                    + "\n   c) add,create song"
                    + "\n       - add song <name>, create song <name>"
                    + "\n3. change"
                    + "\n   a) change jukebox"
                    + "\n       - change jukebox name <new name>"
                    + "\n       - change jukebox author <new author>"
                    + "\n   c) change song"
                    + "\n       - change song name"
                    + "\n       NOTE: You will be prompted to choose new name after it finds specific song."
                    + "\n4. play"
                    + "\n   a) play all"
                    + "\n       - play jukebox"
                    + "\n   b) play shuffle" //random order of songs
                    + "\n       - play random" //one random song
                    + "\n   c) play album"
                    + "\n       - play album name"
                    + "\n       - play album artist"
                    + "\n       - play album year"
                    + "\n       - play album genre"
                    + "\n   c) play song"
                    + "\n       - play song name"
                    + "\n       - play song artist"
                    + "\n       - play song year"
                    + "\n       - play song genre"
                    + "\n       - play song length"
                    + "\n5. work with files"
                    + "\n   a) open <filename>, save"
                    + "\n   b) import <filename>, load <filename>"
                    + "\n   c) export <filename>, saveas <filename>"
                    + "\n6. close, exit, terminate, quit"
                    + "\n   - use \"nosave\" or \"ns\" after one of the closing commands for quick exit without saving.";

            // bool fileimported = false;
            bool fileOpened = false;
            bool exit = false;
            do
            {
                // Console.Clear();
                Console.WriteLine("\nCommand:");
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
                                if (jukeboxinstance != null)
                                {
                                    jukeboxinstance.printJukeBox(Constants.RESULTCOLOR);
                                    break;
                                }
                                else
                                {
                                    printAlert("Jukebox is empty :(");
                                }
                                break;

                            case "name":
                                printResult("Jukebox name: " + jukeboxinstance.getJukeboxName());
                                break;

                            case "author":
                                printResult("Jukebox author: " + jukeboxinstance.getAuthorName());
                                break;

                            case "album":
                                if (input.Length < 3)
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
                                    // PRINT ALBUM GENRE
                                    case "genre":
                                        if (input.Length >= 4)
                                        {
                                            string expression = input[3];
                                            List<Album> albumList = jukeboxinstance.searchAlbumByGenre(expression);
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
                                        else { printAlert("You must specify the genre!"); }
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
                                    // PRINT SONG GENRE
                                    case "genre":
                                        if (input.Length >= 4)
                                        {
                                            string expression = input[3];
                                            List<Song> songList = jukeboxinstance.searchSongByGenre(expression);
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
                                        else { printAlert("You must specify the genre of the song!"); }
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
                    // PLAY
                    case "play":
                        if (input.Length < 2)
                        {
                            printAlert("You must specify what you want to play!");
                            break;
                        }
                        switch (input[1].ToLowerInvariant())
                        {
                            ////////////////////////////////////////////////////////////
                            // PLAY ALL
                            case "all":
                            case "jukebox":
                                // Function, which will play songs in order.
                                printComment("To be implemented..");
                                break;

                            case "shuffle":
                                // Function, which will play songs in random order.
                                printComment("To be implemented..");
                                break;

                            case "random":
                                // Function, which will play a random song.
                                printComment("To be implemented..");
                                break;

                            case "album":
                                if (input.Length < 3)
                                {
                                    printAlert("You must specify by what you want to filter!");
                                    break;
                                }
                                switch (input[2].ToLowerInvariant())
                                {
                                    ////////////////////////////////////////////////////////////
                                    // PLAY ALBUM NAME
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
                                                    album.printAlbumAll(Constants.RESULTCOLOR);
                                                }
                                                break;
                                            }
                                            else { printAlert("No matches found for \"" + expression + "\"!"); }
                                        }
                                        // If no argument was given, print alert
                                        else { printAlert("You must specify the name of the album!"); }
                                        break;

                                    ////////////////////////////////////////////////////////////
                                    // PLAY ALBUM ARTIST
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
                                                    album.printAlbumAll(Constants.RESULTCOLOR);
                                                }
                                                break;
                                            }
                                            else { printAlert("No matches found for \"" + expression + "\"!"); }
                                        }
                                        // If no argument was given, print alert
                                        else { printAlert("You must specify the name of the artist!"); }
                                        break;

                                    ////////////////////////////////////////////////////////////
                                    // PLAY ALBUM YEAR
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
                                                    album.printAlbumAll(Constants.RESULTCOLOR);
                                                }
                                                break;
                                            }
                                            else { printAlert("No matches found for \"" + expression + "\"!"); }
                                        }
                                        // If no argument was given, print alert
                                        else { printAlert("You must specify the name of the artist!"); }
                                        break;

                                    ////////////////////////////////////////////////////////////
                                    // PLAY ALBUM GENRE
                                    case "genre":
                                        if (input.Length >= 4)
                                        {
                                            string expression = input[3];
                                            List<Album> albumList = jukeboxinstance.searchAlbumByGenre(expression);
                                            // If no album was found, we print alert
                                            if (albumList.Count > 0)
                                            {
                                                foreach (Album album in albumList)
                                                {
                                                    album.printAlbumAll(Constants.RESULTCOLOR);
                                                }
                                                break;
                                            }
                                            else { printAlert("No matches found for \"" + expression + "\"!"); }
                                        }
                                        // If no argument was given, print alert
                                        else { printAlert("You must specify the genre!"); }
                                        break;

                                    ////////////////////////////////////////////////////////////
                                    // PLAY ALBUM DEFAULT
                                    default:
                                        printAlert("Cannot find command \"" + input[2] + "\". Type \"help\" or \"?\" for list of valid commands.");
                                        break;
                                }
                                break;
                            ////////////////////////////////////////////////////////////
                            // PLAY SONG
                            case "song":
                                if (input.Length < 3)
                                {
                                    printAlert("You must specify by what you want to filter!");
                                    break;
                                }
                                switch (input[2].ToLowerInvariant())
                                {
                                    ////////////////////////////////////////////////////////////
                                    // PLAY SONG NAME
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
                                                    song.printSongAll(Constants.RESULTCOLOR);
                                                }
                                                break;
                                            }
                                            else { printAlert("No matches found for \"" + expression + "\"!"); }
                                        }
                                        // If no argument was given, print alert
                                        else { printAlert("You must specify the name of the song!"); }
                                        break;

                                    ////////////////////////////////////////////////////////////
                                    // PLAY SONG ARTIST
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
                                                    song.printSongAll(Constants.RESULTCOLOR);
                                                }
                                                break;
                                            }
                                            else { printAlert("No matches found for \"" + expression + "\"!"); }
                                        }
                                        // If no argument was given, print alert
                                        else { printAlert("You must specify the artist of the song!"); }
                                        break;

                                    ////////////////////////////////////////////////////////////
                                    // PLAY SONG YEAR
                                    case "year":
                                        if (input.Length >= 4)
                                        {
                                            string expression = input[3];
                                            List<Song> songList = jukeboxinstance.searchSongByYear(expression);
                                            if (songList.Count > 0)
                                            {
                                                foreach (Song song in songList)
                                                {
                                                    song.printSongAll(Constants.RESULTCOLOR);
                                                }
                                                break;
                                            }
                                            else { printAlert("No matches found for \"" + expression + "\"!"); }
                                        }
                                        // If no argument was given, print alert
                                        else { printAlert("You must specify the year of the song!"); }
                                        break;

                                    ////////////////////////////////////////////////////////////
                                    // PLAY SONG LENGTH
                                    case "length":
                                        if (input.Length >= 4)
                                        {
                                            string expression = input[3];
                                            List<Song> songList = jukeboxinstance.searchSongByLength(expression);
                                            if (songList.Count > 0)
                                            {
                                                foreach (Song song in songList)
                                                {
                                                    song.printSongAll(Constants.RESULTCOLOR);
                                                }
                                                break;
                                            }
                                            else { printAlert("No matches found for \"" + expression + "\"!"); }
                                        }
                                        // If no argument was given, print alert
                                        else { printAlert("You must specify the length of the song!"); }
                                        break;

                                    ////////////////////////////////////////////////////////////
                                    // PLAY SONG GENRE
                                    case "genre":
                                        if (input.Length >= 4)
                                        {
                                            string expression = input[3];
                                            List<Song> songList = jukeboxinstance.searchSongByGenre(expression);
                                            if (songList.Count > 0)
                                            {
                                                foreach (Song song in songList)
                                                {
                                                    song.printSongAll(Constants.RESULTCOLOR);
                                                }
                                                break;
                                            }
                                            else { printAlert("No matches found for \"" + expression + "\"!"); }
                                        }
                                        // If no argument was given, print alert
                                        else { printAlert("You must specify the genre of the song!"); }
                                        break;

                                    ////////////////////////////////////////////////////////////
                                    // PLAY SONG DEFAULT
                                    default:
                                        printAlert("Cannot find command \"" + input[2] + "\". Type \"help\" or \"?\" for list of valid commands.");
                                        break;
                                }
                                break;

                            ////////////////////////////////////////////////////////////
                            // PLAY DEFAULT
                            default:
                                printAlert("Cannot find command \"" + input[1] + "\". Type \"help\" or \"?\" for list of valid commands.");
                                break;
                        }
                        break;

                    ////////////////////////////////////////////////////////////
                    // ADD
                    case "add":
                    case "create":
                        if (input.Length < 3)
                        {
                            // If too few arguments are given
                            printAlert("You must specify what you want to add!"
                                + "\nType \"help\" or \"?\" for list of valid commands.");
                            break;
                        }
                        switch (input[1].ToLowerInvariant())
                        {
                            ////////////////////////////////////////////////////////////
                            // ADD ALBUM
                            case "album":
                                if (input[2] == "")
                                {   //If string contains nothing, print alert and break
                                    printAlert("You must specify what you want to add!"
                                        + "\nType \"help\" or \"?\" for list of valid commands.");
                                    break;
                                }
                                try
                                {
                                    string albumName = input[2];
                                    int i = 3;
                                    //If album name has more than one word, it will join them to single string.
                                    while (i < input.Length)
                                    {
                                        albumName = albumName + " " + input[i];
                                        i++;
                                    }
                                    //Trimming whitespaces at the start/end
                                    albumName = albumName.TrimStart(' ');
                                    albumName = albumName.TrimEnd(' ');
                                    printSuccess("\nCreating new album with name \"" + albumName + "\".");
                                    printResult("\nEnter Artist: ");
                                    string albumArtist = Console.ReadLine();
                                    //Trimming whitespaces at the start/end
                                    albumArtist = albumArtist.TrimStart(' ');
                                    albumArtist = albumArtist.TrimEnd(' ');
                                    printResult("\nEnter Genre: ");
                                    string albumGenre = Console.ReadLine();
                                    //Trimming whitespaces at the start/end
                                    albumGenre = albumGenre.TrimStart(' ');
                                    albumGenre = albumGenre.TrimEnd(' ');
                                    //Getting current year
                                    int currentDate = DateTime.Now.Year;
                                    int albumYear = 0;
                                    bool valid = true;
                                    //Year must be between 0 and our current year
                                    do
                                    {
                                        valid = true;
                                        printResult("\nEnter Year: ");
                                        try { albumYear = int.Parse(Console.ReadLine()); } //If there is any error
                                        catch { valid = false; } //Year is not valid
                                        if (!valid || albumYear < 0 || albumYear > currentDate) { printAlert("You did not enter valid year.\nTry again, please."); }
                                    } while (albumYear < 0 || albumYear > currentDate || !valid);
                                    //Creating album
                                    jukeboxinstance.addAlbum(albumName, albumArtist, albumGenre, albumYear);
                                    printSuccess("\nNew album \"" + albumName + "\" created!");
                                }
                                //If there is any error within TRY, do what is in CATCH
                                catch { printAlert("There was an error with creating new album."); }

                                break;

                            case "song":
                                if (input[2] == "")
                                {   //If string contains nothing, print alert and break
                                    printAlert("You must specify what you want to add!"
                                        + "\nType \"help\" or \"?\" for list of valid commands.");
                                    break;
                                }
                                try
                                {
                                    bool valid = true;
                                    string songName = input[2];
                                    int i = 3;
                                    //If song name has more than one word, it will join them to single string.
                                    while (i < input.Length)
                                    {
                                        songName = songName + " " + input[i];
                                        i++;
                                    }
                                    //Trimming whitespaces at the start/end
                                    songName = songName.TrimStart(' ');
                                    songName = songName.TrimEnd(' ');
                                    printSuccess("\nCreating song \"" + songName + "\".");
                                    string songAlbum = "Unknown album";
                                    int albumIndex = -1;
                                    do
                                    {
                                        int x = 1;
                                        valid = true;
                                        List<Album> albums = jukeboxinstance.getAllAlbums();
                                        foreach(Album album in albums)
                                        {
                                            printComment(x + ") Album: " + album.name + " - Artist: " + album.artist);
                                            x++;
                                        }
                                        printResult("\nEnter album: ");
                                        songAlbum = Console.ReadLine();
                                        albumIndex = jukeboxinstance.searchForAlbumIndex(songAlbum);
                                        if (albumIndex < 0) { valid = false; printAlert("This album doesn't exist." 
                                            + "\nTry again, please."); }
                                    } while (!valid);
                                    //If length is valid
                                    int songLength = 0;
                                    do
                                    {
                                        valid = true;
                                        printResult("\nEnter length: ");
                                        try { songLength = int.Parse(Console.ReadLine()); } //If there is any error
                                        catch { valid = false; } //length is not valid
                                        if (!valid || songLength < 0) { printAlert("You did not enter valid length.\nTry again, please."); }
                                    } while (!valid || songLength < 0);

                                    printResult("\nEnter lyrics: ");
                                    string songText = Console.ReadLine();
                                    //Creating song
                                    Song newSong = new Song(songName, songLength, songText);
                                    jukeboxinstance.albums[albumIndex].addSong(newSong);
                                    //Printing success
                                    printSuccess("\nNew song \"" + songName + "\" added to album \"" + songAlbum + "\".");
                                }
                                //If there is any error within TRY, do what is in CATCH
                                catch { printAlert("There was an error with creating new album."); }

                                break;

                            default:
                                printAlert("Cannot find command \"" + input[1] + "\". Type \"help\" or \"?\" for list of valid commands.");
                                break;
                        }
                        break;

                    ////////////////////////////////////////////////////////////
                    // CHANGE
                    case "change":
                        // If we want to expand the change command list, we need to handle the arguments differently (see print or play command)
                        if (input.Length < 4)
                        {
                            // If too few arguments are given
                            printAlert("You must specify what you want to change and a new name!"
                                + "\nType \"help\" or \"?\" for list of valid commands.");
                            break;
                        }

                        //if (input.Length > 4)
                        //{
                        //    // Too many arguments are given - This prevents multi-word names! 
                        //    printAlert("Too many arguments!");
                        //    break;
                        //}

                        switch (input[1].ToLowerInvariant())
                        {
                            ////////////////////////////////////////////////////////////
                            // CHANGE JUKEBOX
                            case "jukebox":
                                switch (input[2].ToLowerInvariant())
                                {
                                    ////////////////////////////////////////////////////////////
                                    // CHANGE JUKEBOX NAME
                                    case "name":
                                        // Changing the name of jukebox
                                        string oldJukeboxName = jukeboxinstance.getJukeboxName();
                                        string newJukeboxName = input[3];
                                        jukeboxinstance.changeJukeboxName(newJukeboxName);
                                        printSuccess("Jukebox \"" + oldJukeboxName + "\" has been renamed as \"" + newJukeboxName + "\".");
                                        break;

                                    ////////////////////////////////////////////////////////////
                                    // CHANGE AUTHOR
                                    case "author":
                                        string oldAuthorName = jukeboxinstance.getAuthorName();
                                        string newAuthorName = input[3];
                                        jukeboxinstance.changeAuthorName(newAuthorName);
                                        printSuccess("Author \"" + oldAuthorName + "\" has been changed to \"" + newAuthorName + "\".");
                                        break;

                                    ////////////////////////////////////////////////////////////
                                    // CHANGE JUKEBOX DEFAULT
                                    default:
                                        printAlert("Cannot find command \"" + input[2] + "\". Type \"help\" or \"?\" for list of valid commands.");
                                        break;
                                }
                                break;

                            ////////////////////////////////////////////////////////////
                            // CHANGE SONG
                            case "song":
                                switch (input[2].ToLowerInvariant())
                                {
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

                                            bool isGood = true;
                                            int x = 0;
                                            do
                                            {
                                                isGood = true;
                                                i = 0;
                                                foreach (Song song in songList)
                                                {
                                                    i++;
                                                    printResult(i + ") Name: " + song.name + " - Length: " + song.length);
                                                }
                                                printAlert("Choose a song!");
                                                try
                                                {
                                                    x = int.Parse(Console.ReadKey().KeyChar.ToString());
                                                }
                                                catch (Exception)
                                                {
                                                    isGood = false;
                                                    printAlert("\nYou must enter a number!");
                                                }
                                                if (x > i)
                                                {
                                                    isGood = false;
                                                    printAlert("\nYou must enter a VALID number!");
                                                }
                                            } while (!isGood);
                                            printResult("\nPlease enter new name:");

                                            expression = Console.ReadLine();

                                            songList[x - 1].changeName(expression);
                                            printSuccess();

                                        }
                                        else { printAlert("No matches found for \"" + input[2] + "\"!"); }

                                        break;
                                }
                                break;

                            ////////////////////////////////////////////////////////////
                            // CHANGE DEFAULT
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
                            printAlert("You must specify the name of the file!");
                            break;
                        }
                        else if (input.Length < 3)
                        {
                            // Trying to load a file
                            dummyinstancefilename = input[1];
                            dummyinstancefilepath = Constants.LOCALPATH + Constants.EXPORTFOLDER;
                            dummyjukeboxinstance = importFromXml(dummyinstancefilepath, dummyinstancefilename);
                            if (dummyjukeboxinstance == null)
                            {
                                // Failure
                                printAlert("No data loaded! Reverting to previous instance.");
                                break;
                            }
                            // Success loading the file and data
                            instancefilename = dummyinstancefilename;
                            // Changing the instance
                            jukeboxinstance = dummyjukeboxinstance;
                            printSuccess("Successully loaded file \"" + instancefilename + "\" from " + instancefilepath);
                            fileOpened = false;
                            printComment("\nJukebox name: " + jukeboxinstance.getJukeboxName()
                                + "\nCreated by: " + jukeboxinstance.getAuthorName());
                            break;
                        }
                        else if (input.Length < 4)
                        {
                            // Trying to load a file
                            dummyinstancefilename = input[1];
                            dummyinstancefilepath = input[2];
                            dummyjukeboxinstance = importFromXml(dummyinstancefilepath, dummyinstancefilename);
                            if (dummyjukeboxinstance == null)
                            {
                                // Failure
                                printAlert("No data loaded! Reverting to previous instance.");
                                break;
                            }
                            // Success loading the file and data
                            instancefilename = dummyinstancefilename;
                            instancefilepath = dummyinstancefilepath;
                            jukeboxinstance = dummyjukeboxinstance;
                            printSuccess("Successully loaded file \"" + instancefilename + "\" from " + instancefilepath);
                            fileOpened = true;
                            break;
                        }
                        else
                        {
                            // If too many arguments are given
                            printAlert("Too many arguments!");
                        }
                        break;

                    ////////////////////////////////////////////////////////////
                    // EXPORT
                    case "export":
                    case "saveas":
                        if (input.Length < 2)
                        {
                            // If no filename was given
                            printAlert("You must specify the name of the file!");
                            break;
                        }
                        else if (input.Length < 3)
                        {
                            if (exportToXml(jukeboxinstance, exportFilePath, input[1]))
                            {
                                printSuccess("File successully exported as \"" + input[1] + ".xml\" to " + Constants.LOCALPATH + Constants.EXPORTFOLDER);
                                break;
                            }
                            printAlert("Something went wrong");
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
                                break;
                            }
                            printAlert("Something went wrong");
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
                            dummyinstancefilename = input[1];
                            dummyinstancefilepath = Constants.LOCALPATH + Constants.EXPORTFOLDER;
                            // Trying to load a file
                            dummyjukeboxinstance = importFromXml(dummyinstancefilepath, dummyinstancefilename);
                            if (dummyjukeboxinstance == null)
                            {
                                // Failure
                                printAlert("No data loaded! Reverting to previous instance.");
                                break;
                            }
                            // Success loading the file and data
                            instancefilename = dummyinstancefilename;
                            instancefilepath = dummyinstancefilepath;
                            jukeboxinstance = dummyjukeboxinstance;
                            printSuccess("Successully loaded file \"" + instancefilename + ".xml\" from " + instancefilepath);
                            fileOpened = true;
                            printComment("\nJukebox name: " + jukeboxinstance.getJukeboxName()
                                + "\nCreated by: " + jukeboxinstance.getAuthorName());
                            break;
                        }
                        else if (input.Length < 4)
                        {
                            dummyinstancefilename = input[1];
                            dummyinstancefilepath = input[2];
                            // Trying to load a file
                            dummyjukeboxinstance = importFromXml(dummyinstancefilepath, dummyinstancefilename);
                            if (dummyjukeboxinstance == null)
                            {
                                // Failure
                                printAlert("No data loaded! Reverting to previous instance.");
                                break;
                            }
                            // Success loading the file and data
                            instancefilename = dummyinstancefilename;
                            instancefilepath = dummyinstancefilepath;
                            jukeboxinstance = dummyjukeboxinstance;
                            printSuccess("Successully loaded file \"" + instancefilename + ".xml\" from " + instancefilepath);
                            fileOpened = true;
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
                        if (exportToXml(jukeboxinstance, instancefilepath, instancefilename))
                        {
                            if (fileOpened == true)
                            {
                                printSuccess("Data saved to \"" + instancefilename + ".xml\" at " + instancefilepath);
                            }
                            else
                            {
                                printSuccess("Data saved.");
                            }
                        }
                        else
                        {
                            printAlert("Data not saved!");
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
                        if (exportToXml(jukeboxinstance, instancefilepath, instancefilename))
                        {
                            printSuccess("Data saved to \"" + instancefilename + ".xml\" at " + instancefilepath);
                        }
                        else
                        {
                            printAlert("Data not saved! At this point I think you're to blame..");
                        }
                   
                    
                    
                    exit = true;
                    
                }
                else if (key == 'n' || key == 'N')
                {
                    printAlert("\nExiting without saving!");
                    exit = true;
                }
                Console.WriteLine("\n");
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