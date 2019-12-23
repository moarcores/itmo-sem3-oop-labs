namespace Lab2_MusicLibrary
{
    public class Queue
    {
        public bool _isTrack;
        public int _year;
        public string _genre;
        public string _artistName;
        public string _name;

        public Queue(bool isTrack)
        {
            _isTrack = isTrack;
            _year = -1;
            _genre = null;
            _artistName = null;
            _name = null;
        }

        public bool IsEmpty()
        {
            return _year == -1 && _genre == null && _artistName == null && _name == null;
        }

        public static Queue LookupByName(Queue queue, string name)
        {
            queue._name = name;
            return queue;
        }
    }
}