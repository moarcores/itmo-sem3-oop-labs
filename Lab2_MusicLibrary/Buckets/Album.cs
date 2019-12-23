using System.Collections.Generic;

namespace Lab2_MusicLibrary
{
    public class Album
    {
        private string name;
        private HashSet<Track> Tracks;
        
        public Album(string srcName)
        {
            name = srcName;
            Tracks = new HashSet<Track>();
        }

        public void AddTrack(Track track)
        {
            Tracks.Add(track);
        }
    }
}