using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2_MusicLibrary
{
    internal class Program
    {
        static void Main(string[] args){
            MusicLib lib = new MusicLib();
            lib.AddGenre("Rock");
            lib.AddGenre("Hard Rock", "Rock");
            lib.AddGenre("Rap");
            lib.AddGenre("Mumble", "Rap");
            
            
            
            lib.AddArtist("Nirvana");
            lib.AddAlbum("Nirvana", "Nevermind", 1991);
            
            
            lib.AddTrack("Smells like teen spirit", 1991, "Nirvana", "Rock");
            lib.AddTrack("About a girl", 1989, "Nirvana", "Rock");
            lib.AddTrack("School", 1991, "Nirvana", "Rock");
            
            lib.AddTrackToAlbum("Nevermind", "Smells like teen spirit", "Nirvana");
            lib.AddTrackToAlbum("Nevermind", "About a girl", "Nirvana");
            lib.AddTrackToAlbum("Nevermind", "School", "Nirvana");
            
            lib.AddArtist("Eminem");
            lib.AddAlbum("Eminem", "The Marshall Mathers LP2", 2013);
            lib.AddTrack("Rap God", 2013,"Eminem", "Rap");
            lib.AddTrack("Legacy", 2013, "Eminem", "Rap");
            lib.AddTrack("Berzerk", 2013, "Eminem", "Rap");
            
            lib.AddTrackToAlbum("The Marshall Mathers LP2", "Rap God", "Eminem");
            lib.AddTrackToAlbum("The Marshall Mathers LP2", "Legacy", "Eminem");
            lib.AddTrackToAlbum("The Marshall Mathers LP2", "Berzerk", "Eminem");


            lib.DisplayAllTracks();
            Console.WriteLine();


            Queue queue = new Queue(true);
            queue._year = 2013;

            lib.Get(queue).ForEach(Console.WriteLine);
            Console.WriteLine();


            queue._year = -1;
            queue._genre = "Rock";
            lib.Get(queue).ForEach(Console.WriteLine);
        }
    }
}