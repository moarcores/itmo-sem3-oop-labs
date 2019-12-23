using System;
using System.Collections.Generic;

namespace Lab2_MusicLibrary
{
    public class MusicLib
    {
        private Dictionary<string, Artist> Artists;
        private Dictionary<int, Year> Years;
        private Dictionary<string, Genre> Genres;
        
        private Dictionary<string, Track> uniqTracks;
        private Dictionary<string, List<Track>> nameTracks;
        
        private Dictionary<string, Album> uniqAlbums;
        private Dictionary<string, List<Album>> nameAlbums;

        public MusicLib()
        {
            Artists = new Dictionary<string, Artist>();
            Years = new Dictionary<int, Year>();
            Genres = new Dictionary<string, Genre>();
            uniqTracks = new Dictionary<string, Track>();
            nameTracks = new Dictionary<string, List<Track>>();
            uniqAlbums = new Dictionary<string, Album>();
            nameAlbums = new Dictionary<string, List<Album>>();
        }
        
        public List<string> Get(Queue queue)
        {
            if (queue.IsEmpty())
            {
                throw new ArgumentException();
            }

            List<string> result = new List<string>();
            if (queue._isTrack)
            {
                foreach (var variable in GetTracks(queue))
                { 
                    result.Add(variable.ToString());   
                }
            }
            else
            {
                foreach (var variable in GetAlbums(queue))
                { 
                    result.Add(variable.ToString());   
                }
            }
            return result;
        }

        private HashSet<Album> GetAlbums(Queue queue)
        {
            HashSet<Album> byYear = null;
            HashSet<Album> byArtist = null;
            HashSet<Album> byName = null;

            HashSet<Album> result = null;
            if (queue._year != -1)
            {
                byYear = new HashSet<Album>();
                if (Years.ContainsKey(queue._year))
                {
                    byYear.UnionWith(Years[queue._year]._albums);
                }

                result = byYear;
            }
            
            if (queue._artistName != null)
            {
                byArtist = new HashSet<Album>();
                if (Artists.ContainsKey(queue._artistName))
                {
                    byArtist.UnionWith(Artists[queue._artistName]._albums);
                }

                result = byArtist;
            }
            
            if (queue._name != null)
            {
                byName = new HashSet<Album>();
                if (nameAlbums.ContainsKey(queue._name))
                {
                    byName.UnionWith(nameAlbums[queue._name]);
                }

                result = byName;
            }


            if (byArtist != null)
            {
                result.IntersectWith(byArtist);
            }

            if (byName != null)
            {
                result.IntersectWith(byName);
            }

            if (byYear != null)
            {
                result.IntersectWith(byYear);
            }

            return result;
        }
        
        private HashSet<Track> GetTracks(Queue queue)
        {
            
            HashSet<Track> byGenre = null;
            HashSet<Track> byYear = null;
            HashSet<Track> byArtist = null;
            HashSet<Track> byName = null;

            HashSet<Track> result = null;
            
            if (queue._year != -1)
            {
                byYear = new HashSet<Track>();
                if (Years.ContainsKey(queue._year))
                {
                    byYear.UnionWith(Years[queue._year]._tracks);
                }

                result = byYear;
            }
            
            if (queue._artistName != null)
            {
                byArtist = new HashSet<Track>();
                if (Artists.ContainsKey(queue._artistName))
                {
                    byArtist.UnionWith(Artists[queue._artistName]._tracks);
                }

                result = byArtist;
            }
            
            if (queue._name != null)
            {
                byName = new HashSet<Track>();
                if (nameAlbums.ContainsKey(queue._name))
                {
                    byName.UnionWith(nameTracks[queue._name]);
                }

                result = byName;
            }
            
            if (queue._genre != null)
            {
                byGenre = new HashSet<Track>();
                if (Genres.ContainsKey(queue._genre))
                {
                    byGenre.UnionWith(Genres[queue._genre]._tracks);
                    foreach (var variable in Genres[queue._genre]._childs)
                    {
                        byGenre.UnionWith(variable._tracks);
                    }
                }
                result = byGenre;
            }
            
            if (byArtist != null)
            {
                result.IntersectWith(byArtist);
            }

            if (byName != null)
            {
                result.IntersectWith(byName);
            }

            if (byYear != null)
            {
                result.IntersectWith(byYear);
            }
            
            if (byGenre != null)
            {
                result.IntersectWith(byGenre);
            }

            return result;
        }

        public void DisplayAllTracks()
        {
            foreach (var v in uniqTracks.Values)
            {
                Console.WriteLine(v);
            }
        }
        
        public List<string> FindArtists(string src)
        {
            List<string> result = new List<string>();
            foreach (var v in Artists.Keys)
            {
                if (v.Contains(src))
                {
                    result.Add(v.ToString());
                }
            }
            return result;
        }
        
        public void AddTrackToAlbum(string album, string track, string artist)
        {
            if (!uniqAlbums.ContainsKey(album + "~" + artist) 
                || !uniqTracks.ContainsKey(artist + "~" + track))
            {
                throw new ArgumentException();
            }
            
            uniqAlbums[album + "~" + artist].AddTrack(uniqTracks[artist + "~" + track]);
        }

        public void AddAlbum(string srcArtist, string srcAlbum, int srcYear)
        {
            if (uniqAlbums.ContainsKey(srcAlbum + "~" + srcArtist)
                ||!Artists.ContainsKey(srcArtist))
            {
                throw new ArgumentException();
            }

            Album album = new Album(srcAlbum);
            
            if (!Years.ContainsKey(srcYear))
            {
                Year year = new Year(srcYear);
                Years.Add(srcYear, year);
            }
            Years[srcYear].AddAlbum(album);
            
            uniqAlbums.Add(srcAlbum + "~" + srcArtist, album);
            if (!nameAlbums.ContainsKey(srcAlbum))
            {
                nameAlbums.Add(srcAlbum, new List<Album>());
            }
            nameAlbums[srcAlbum].Add(album);

            Artists[srcArtist].AddAlbum(album);
        }

        public void AddTrack(string srcName, int srcYear, string srcArtist, string srcGenre)
        {
            if (uniqTracks.ContainsKey(srcArtist + "~" + srcName) 
                || !Artists.ContainsKey(srcArtist) || !Genres.ContainsKey(srcGenre))
            {
                throw new ArgumentException();
            }

            Artist artist = Artists[srcArtist];
            Genre genre = Genres[srcGenre];
            Track track = new Track(srcName, artist, srcYear, genre);
            
            if (!Years.ContainsKey(srcYear))
            {
                Year year = new Year(srcYear);
                Years.Add(srcYear, year);
            }
            Years[srcYear].AddTrack(track);
            
            uniqTracks.Add(srcArtist + "~" + srcName, track);
            if (!nameTracks.ContainsKey(srcName))
            {
                nameTracks.Add(srcName, new List<Track>());
            }
            nameTracks[srcName].Add(track);
            
            genre.AddTrack(track);
            artist.AddTrack(track);
        }

        public void AddArtist(string artistName)
        {
            if (!Artists.ContainsKey(artistName))
            {
                Artist artist = new Artist(artistName);
                Artists.Add(artistName, artist);
            }
        }
        
        public void AddGenre(string childName, string parentName)
        {
            if (!Genres.ContainsKey(parentName))
            {
                throw new ArgumentException();
            }
            
            if (!Genres.ContainsKey(childName))
            {
                Genre child = new Genre(childName);
                Genres.Add(childName, child);
                Genres[parentName].AddChild(child);
            }
        }

        public void AddGenre(string genreName)
        {
            if (!Genres.ContainsKey(genreName))
            {
                Genre genre = new Genre(genreName);
                Genres.Add(genreName, genre);
            }
        }
    }
}