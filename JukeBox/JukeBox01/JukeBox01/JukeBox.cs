using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace JukeBox01
{
    public class JukeBox
    {
        [XmlElement("jukeboxName")]
        public string name;
        [XmlElement("jukeboxAuthor")]
        public string author;
        [XmlElement("jukeboxAlbums")]
        public List<Album> albums;

        ////////////////////////////////////////////////////////////
        // CONSTRUCTORS

        // Constructor for Album class
        // Constructor without parameters
        public JukeBox()
        {
            this.name = "no name";
            this.author = "unknown author";
            this.albums = new List<Album>();
        }
        // Copy constructor
        public JukeBox(JukeBox jukebox)
        {
            this.name = jukebox.name;
            this.author = jukebox.author;
            this.albums = new List<Album>(jukebox.albums);
        }
        // Constructor with parameters
        public JukeBox(string name = "no name", string author = "unknown author")
        {
            this.name = name;
            this.author = author;
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
        public void swapAlbums(int first, int second)
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
        // SEARCH METHODS

        // Search albums by name
        public List<Album> searchAlbumByName(string name)
        {
            List<Album> resultList = new List<Album>();
            foreach (Album album in albums)
            {
                if (album.name.ToLowerInvariant().Contains(name.ToLowerInvariant()))
                {
                    resultList.Add(album);
                }
            }
            return resultList;
        }
        // Search songs by name
        public List<Song> searchSongByName(string name)
        {
            List<Song> resultList = new List<Song>();
            foreach (Album album in albums)
            {
                foreach (Song song in album.songs)
                {
                    if (song.name.ToLowerInvariant().Contains(name.ToLowerInvariant()))
                    {
                        resultList.Add(song);
                    }
                }
            }
            return resultList;
        }
        // Search albums by artist
        public List<Album> searchAlbumByArtist(string artist)
        {
            List<Album> resultList = new List<Album>();
            foreach (Album album in albums)
            {
                if (album.artist.ToLowerInvariant().Contains(artist.ToLowerInvariant()))
                {
                    resultList.Add(album);
                }
            }
            return resultList;
        }
        // Seach songs by artist
        public List<Song> searchSongByArtist(string artist)
        {
            List<Song> resultList = new List<Song>();
            foreach (Album album in albums)
            {
                if (album.artist.ToLowerInvariant().Contains(artist.ToLowerInvariant()))
                {
                    foreach (Song song in album.songs)
                    {
                        resultList.Add(song);
                    }
                }
            }
            return resultList;
        }
        // Search albums by year
        public List<Album> searchAlbumByYear(string year)
        {
            List<Album> resultList = new List<Album>();
            foreach (Album album in albums)
            {
                if (album.year.ToString() == year)
                {
                    resultList.Add(album);
                }
            }
            return resultList;
        }
        // Search song by year
        public List<Song> searchSongByYear(string year)
        {
            List<Song> resultList = new List<Song>();
            foreach (Album album in albums)
            {
                if (album.year.ToString() == year)
                {
                    foreach (Song song in album.songs)
                    {
                        resultList.Add(song);
                    }
                }
            }
            return resultList;
        }
        // Search albums by genre
        public List<Album> searchAlbumByGenre(string genre)
        {
            List<Album> resultList = new List<Album>();
            foreach (Album album in albums)
            {
                if (album.genre.ToLowerInvariant().Contains(genre.ToLowerInvariant()))
                {
                    resultList.Add(album);
                }
            }
            return resultList;
        }
        // Search song by genre
        public List<Song> searchSongByGenre(string genre)
        {
            List<Song> resultList = new List<Song>();
            foreach (Album album in albums)
            {
                if (album.genre.ToLowerInvariant().Contains(genre.ToLowerInvariant()))
                {
                    foreach (Song song in album.songs)
                    {
                        resultList.Add(song);
                    }
                }
            }
            return resultList;
        }
        // Search song by length
        public List<Song> searchSongByLength(string length)
        {
            List<Song> resultList = new List<Song>();
            foreach (Album album in albums)
            {
                foreach (Song song in album.songs)
                {
                    if (song.length.ToString() == length)
                    {
                        resultList.Add(song);
                    }
                }
            }
            return resultList;
        }

        ////////////////////////////////////////////////////////////
        // PRINT METHODS

        public void printJukeBox()
        {
            Console.WriteLine("JukeBox name: {0}, author: {1}", this.name, this.author);
            foreach (Album album in albums)
            {
                album.printAlbumContent();
            }
        }

        public void printJukeBox(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("JukeBox name: {0}, author: {1}", this.name, this.author);
            foreach (Album album in albums)
            {
                album.printAlbumContent();
            }
            Console.ResetColor();
        }

        ////////////////////////////////////////////////////////////
        // UTILITY METHODS

        // Utility functions of JukeBox
        // getNumAlbums - get the total number of albums in JukeBox
        public int getNumAlbums() { return albums.Count; }

    }
}
