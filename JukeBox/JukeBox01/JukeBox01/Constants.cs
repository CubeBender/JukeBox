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

        // Instance data path
        public const string DATAPATH = "DATA";

        // Export data path
        public const string EXPORTPATH = "EXPORT";

        ////////////////////////////////////////////////////////////
        // FILE NAMES

        // Perzistance data file name
        public const string EXPORTFILENAME = "InstanceData";

    }
}
