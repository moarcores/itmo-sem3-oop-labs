using System.Collections.Generic;

namespace Lab2_MusicLibrary
{
    public class Genre
    {
        private readonly string _genreName;
        public readonly HashSet<Genre> _childs;
        public readonly HashSet<Track> _tracks;

        public Genre(string name)
        {
            _childs = new HashSet<Genre>();
            _tracks = new HashSet<Track>();
            _genreName = name;
        }

        public void AddChild(Genre child)
        {
            if (!_childs.Contains(child))
            {
                _childs.Add(child);
            }
        }

        public void AddTrack(Track track)
        {
            _tracks.Add(track);
        }

        public override string ToString()
        {
            return _genreName;
        }
    }
}