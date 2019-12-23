using System.Collections.Generic;

namespace Lab2_MusicLibrary
{
    public class Year
    {
        private int _year;
        public readonly HashSet<Track> _tracks;
        public readonly HashSet<Album> _albums;
        
        public Year(int srcYear)
        {
            _tracks = new HashSet<Track>();
            _albums = new HashSet<Album>();
            _year = srcYear;
        }

        public void AddTrack(Track track)
        {
            _tracks.Add(track);
        }

        public void AddAlbum(Album album)
        {
            _albums.Add(album);
        }
    }
}