using System.Collections.Generic;

namespace Lab2_MusicLibrary
{
    public class Artist
    {
        private readonly string _artistName;
        public readonly HashSet<Track> _tracks;
        public readonly HashSet<Album> _albums;
        
        public Artist(string srcName)
        {
            _tracks = new HashSet<Track>();
            _albums = new HashSet<Album>();
            _artistName = srcName;
        }

        public void AddTrack(Track track)
        {
            _tracks.Add(track);
        }

        public void AddAlbum(Album album)
        {
            _albums.Add(album);
        }

        public override string ToString()
        {
            return _artistName;
        }
    }
}