using System;
using System.Xml;
using System.Xml.Serialization;

namespace JukeBox01
{
    public class Song
    {
        [XmlElement("songName")]
        public string name;
        [XmlElement("songLength")]
        public int length;
        [XmlElement("songText")]
        public string text;

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

        ////////////////////////////////////////////////////////////
        // PRINT METHODS

        public void printSong()
        {
            Console.WriteLine("Song name: {0}, length: {1}", this.name, this.length);
        }
        public void printSong(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("Song name: {0}, length: {1}", this.name, this.length);
            Console.ResetColor();
        }
        public void printSongAll()
        {
            Console.WriteLine("Song name: {0}, length: {1}", this.name, this.length);
            Console.WriteLine(this.text);
        }
        public void printSongAll(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("Song name: {0}, length: {1}", this.name, this.length);
            Console.WriteLine(this.text);
            Console.ResetColor();
        }

    }
}
