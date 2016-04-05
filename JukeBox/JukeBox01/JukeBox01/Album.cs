using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace JukeBox01
{
    public class Album
    {
        [XmlElement("albumName")]
        public string name;
        [XmlElement("albumArtist")]
        public string artist;
        [XmlElement("albumGenre")]
        public string genre;
        [XmlElement("albumYear")]
        public int year;
        [XmlElement("albumSongs")]
        public List<Song> songs;

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
        public void addSong(int position, Song song) { songs.Insert(position, song); }
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
        public bool deleteSong(Song song) { return songs.Remove(song); }
        // deleteAllSong - delete all songs in album
        public void deleteAllSongs() { songs.Clear(); }

        ////////////////////////////////////////////////////////////
        // SEARCH METHODS

        public List<Song> searchByName(string name)
        {
            List<Song> resultList = new List<Song>();
            foreach (Song song in songs)
            {
                if (song.name == name)
                {
                    resultList.Add(song);
                }
            }
            return resultList;
        }

        ////////////////////////////////////////////////////////////
        // PRINT METHODS

        public void printAlbum()
        {
            Console.WriteLine("Album name: {0}, Artist: {1}, Year: {2}", this.name, this.artist, this.year);
        }
        public void printAlbum(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("Album name: {0}, Artist: {1}, Year: {2}", this.name, this.artist, this.year);
            Console.ResetColor();
        }
        public void printAlbumAll()
        {
            Console.WriteLine("Album name: {0}, Artist: {1}, Year: {2}", this.name, this.artist, this.year);
            foreach (Song song in songs)
            {
                song.printSong();
            }
        }
        public void printAlbumAll(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("Album name: {0}, Artist: {1}, Year: {2}", this.name, this.artist, this.year);
            foreach (Song song in songs)
            {
                song.printSong();
            }
            Console.ResetColor();
        }

        ////////////////////////////////////////////////////////////
        // UTILITY METHODS

        // Utility functions of Albums
        // getNumSongs - get the total number of songs in album

        // getNumSongs - get the total number of songs in album
        public int getNumSongs() { return songs.Count; }

    }
}
