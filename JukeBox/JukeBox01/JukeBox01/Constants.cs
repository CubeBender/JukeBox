using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public const string DATAFOLDER = "\\DATA";

        // Export data path
        public const string EXPORTFOLDER = "\\EXPORT";

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

    }
}
