namespace Lab2_MusicLibrary
{
    public class Track
    {
        private readonly string _name;
        private readonly int _year;
        private readonly Artist _artist;
        private readonly Genre _genre;


        public string name
        {
            get { return _name; }
        }
        public Artist artist {
            get { return _artist; }
        }

        public Genre genre
        {
            get { return _genre; }
        }

        public int year
        {
            get { return _year; }
        }

        public Track(string srcName, Artist srcArtist, int srcYear, Genre srcGenre)
        {
            _name = srcName;
            _artist = srcArtist;
            _year = srcYear;
            _genre = srcGenre;
        }

        public override string ToString()
        {
            return _name + " ~ " + _artist + "(" + _year + ")" + " - " + _genre;
        }
    }
}