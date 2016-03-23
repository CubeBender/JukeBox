using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JukeBox01
{
    // *************************************************
    // ***************** SONG CLASS ********************
    // *************************************************
    class Song
    {
        private string name;
        private int length;
        private string text;

        ////////////////////////////////////////////////////////////
        // CONSTRUCTORS

        // Constructors for Song class
        // Constructor with parameters
        public Song(string name = "unknown name", int length = 0, string text = "No text!")
        {
            this.name = name;
            this.length = length;
            this.text = text;
        }
        // Constructor without parameters
        public Song()
        {
            this.name = "unknown name";
            this.length = 0;
            this.text = "No text!";
        }

        ////////////////////////////////////////////////////////////
        // DATA METHODS

        // Methods for getting Song private data
        public string getSongName() { return name; }
        public int getLength() { return length; }
        public string getText() { return text; }

        // Methods for changing private data of Song class
        public void changeName(string name) { this.name = name; }
        public void changeLength(int length) { this.length = length; }
        public void changeText(string text) { this.text = text; }
    }

    // *************************************************
    // ***************** ALBUM CLASS *******************
    // *************************************************
    class Album
    {
        private string name;
        private string artist;
        private string genre;
        private int year;
        List<Song> songs;

        ////////////////////////////////////////////////////////////
        // CONSTRUCTORS

        // Constructors for Album class
        // Constructor with parameters
        public Album(string name = "unknown name", string artist = "unknown artist", string genre = "unknown genre", int year = 0)
        {
            this.name = name;
            this.artist = artist;
            this.genre = genre;
            this.year = year;
            this.songs = new List<Song>();
        }
        // Constructor without parameters
        public Album()
        {
            this.name = "unknown name";
            this.artist = "unknown artist";
            this.genre = "unknown genre";
            this.year = 0;
            this.songs = new List<Song>();
        }

        ////////////////////////////////////////////////////////////
        // DATA METHODS

        // Methods for getting Album private data
        public string getAlbumName() { return name; }
        public string getArtistName() { return artist; }
        public string getGenre() { return genre; }
        public int getYear() { return year; }

        // Methods for changing Album private data
        public void changeAlbumName(string name) { this.name = name; }
        public void changeArtistName(string artist) { this.artist = artist; }
        public void changeGenre(string genre) { this.genre = genre; }
        public void changeYear(int year) { this.year = year; }

        ////////////////////////////////////////////////////////////
        // SONGS METHODS

        // Creating and editing songs
        // addSong - add a song to the end or to a certain position
        // replaceSong - replace a song on certain position

        // addSong - add a song at the end
        public void addSong(string name, int length, string text) { songs.Add(new Song(name, length, text)); }
        public void addSong(Song song) { songs.Add(song); }
        // addSong - add a song to a certion position
        public void addSong(int position, string name, int length, string text) { songs.Insert(position, new Song(name, length, text)); }
        public void addSong(int position, Song song){ songs.Insert(position, song); }
        // replaceSong - replace a song on certain position
        public void replaceSong(int position, string name, int length, string text) { songs.RemoveAt(position); songs.Insert(position, new Song(name, length, text)); }
        public void replaceSong(int position, Song song) { songs.RemoveAt(position); songs.Insert(position, song); }

        // Song manipulation
        // swapSongs - swap songs at two positions

        // swapSongs - swap songs at two positions
        public void swapSongs(int first, int second)
        {
            Song temp = songs[first];
            songs[first] = songs[second];
            songs[second] = temp;
        }

        // Deleting songs
        // deleteSong - delete the last song, song at certain position or a specific song
        // deleteAllSongs - delete all songs in an album

        // deleteSong - delete the last song
        public void deleteSong() { songs.RemoveAt(songs.Count - 1); }
        // deleteSong - delete a song at certain position
        public void deleteSong(int position) { songs.RemoveAt(position); }
        // deleteSong - delete a specific song
        public void deleteSong(Song song) { songs.Remove(song); }
        // deleteAllSong - delete all songs in album
        public void deleteAllSongs() { songs.Clear(); }

        ////////////////////////////////////////////////////////////
        // UTILITY METHODS

        // Utility functions of Albums
        // getNumSongs - get the total number of songs in album

        // getNumSongs - get the total number of songs in album
        public int getNumSongs() { return songs.Count; }

    }

    // *************************************************
    // *************** JukeBox CLASS *******************
    // *************************************************
    class JukeBox
    {
        private string name;
        private string author;
        List<Album> albums;

        ////////////////////////////////////////////////////////////
        // CONSTRUCTORS

        // Constructor for Album class
        // Constructor with parameters
        public JukeBox(string name = "no name", string author = "unknown author")
        {
            this.name = name;
            this.author = author;
            this.albums = new List<Album>();
        }
        // Constructor without parameters
        public JukeBox()
        {
            this.name = "no name";
            this.author = "unknown author";
            this.albums = new List<Album>();
        }

        ////////////////////////////////////////////////////////////
        // DATA METHODS

        // Methods for getting Jukebox private data
        public string getJukeboxName() { return name; }
        public string getAuthorName() { return author; }

        // Methods for changing Jukebox private data
        public void changeJukeboxName(string name) { this.name = name; }
        public void changeAuthorName(string author) { this.author = author; }

        ////////////////////////////////////////////////////////////
        // ALBUMS METHODS

        // Creating and editing Albums
        // addAlbum - add an album to the end or to a certain position
        // replaceAlbum - replace an album on certain position

        // addAlbum - add an album at the end
        public void addAlbum(string name, string artist, string genre, int year) { albums.Add(new Album(name, artist, genre, year)); }
        public void addAlbum(Album album) { albums.Add(album); }
        // addAlbum - add an album to a certion position
        public void addAlbum(int position, string name, string artist, string genre, int year) { albums.Insert(position, new Album(name, artist, genre, year)); }
        public void addAlbum(int position, Album album) { albums.Insert(position, album); }
        // replaceAlbum - replace an album on certain position
        public void replaceAlbum(int position, string name, string artist, string genre, int year) { albums.RemoveAt(position); albums.Insert(position, new Album(name, artist, genre, year)); }
        public void replaceAlbum(int position, Album album) { albums.RemoveAt(position); albums.Insert(position, album); }

        // Song manipulation
        // swapAlbums - swap albums at two positions

        // swapAlbums - swap albums at two positions
        public void swapAlbumss(int first, int second)
        {
            Album temp = albums[first];
            albums[first] = albums[second];
            albums[second] = temp;
        }

        // Deleting albums
        // deleteAlbum - delete the last album, album at certain position or a specific album
        // deleteAllAlbums - delete all albums in a JukeBox

        // deleteAlbum - delete the last album
        public void deleteSong() { albums.RemoveAt(albums.Count - 1); }
        // deleteAlbum - delete an album at certain position
        public void deleteSong(int position) { albums.RemoveAt(position); }
        // deleteAlbum - delete a specific album
        public void deleteSong(Album album) { albums.Remove(album); }
        // deleteAllAlbums - delete all albums in JukeBox
        public void deleteAllSongs() { albums.Clear(); }

        ////////////////////////////////////////////////////////////
        // UTILITY METHODS

        // Utility functions of JukeBox
        // getNumAlbums - get the total number of albums in JukeBox

        // getNumAlbums - get the total number of albums in JukeBox
        public int getNumAlbums() { return albums.Count; }

    }

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
        }
    }
}
