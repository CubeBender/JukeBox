using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;


namespace JukeBox01
{
    // *************************************************
    // *************** Program Class *******************
    // *************************************************
    class Program
    {
        // *************************************************
        // **************** Main program *******************
        // *************************************************
        static void Main(string[] args)
        {
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
            
            Console.ReadKey();
        }
    }
}
