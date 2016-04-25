using System;

namespace JukeBox01
{
    static class Constants
    {
        ////////////////////////////////////////////////////////////
        // PATHING

        // Local path
        public static string LOCALPATH = System.AppDomain.CurrentDomain.BaseDirectory;


        ////////////////////////////////////////////////////////////
        // FOLDER NAMES

        // Instance data path
        public const string DATAFOLDER = "DATA";

        // Export data path
        public const string EXPORTFOLDER = "EXPORT";

        ////////////////////////////////////////////////////////////
        // FILE NAMES

        // Perzistance data file name
        public const string DATAFILENAME = "InstanceData";

        // Default export file name

        public const string EXPORTFILENAME = "export";

        ////////////////////////////////////////////////////////////
        // CONSOLE COLOR

        public const ConsoleColor SUCCESSCOLOR = ConsoleColor.Green;

        public const ConsoleColor RESULTCOLOR = ConsoleColor.White;

        public const ConsoleColor ALERTCOLOR = ConsoleColor.Red;

        public const ConsoleColor COMMENTCOLOR = ConsoleColor.DarkGray;

        ////////////////////////////////////////////////////////////
        // HELP


        public const string HELP = "Now usable and working commands:"
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
                + "\n6. clear"
                + "\n7. close, exit, terminate, quit"
                + "\n   - use \"nosave\" or \"ns\" after one of the closing commands for quick exit without saving.";
    }
}
