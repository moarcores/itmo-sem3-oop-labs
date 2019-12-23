using System;
using System.Collections.Generic;

namespace Lab1_RationalFraction
{
    public class CachedFractionList : IFractionCollection
    {
        private ICache cache;
        private List<RationalFraction> storage;
        private ulong version;
        
        public RationalFraction GetMax()
        {
            if (storage.Count == 0) // if collection is empty return null
            {
                return null; 
            }
            
            Query query = new Query(QueryType.GET_MAX); //form query to cache
            List<RationalFraction> result = cache.Get(query, version); // trying to get from cache
            RationalFraction tmp = storage[0];
            
            if (result == null)  // if cache returned no result then find answer
            {
                result = new List<RationalFraction>();
                foreach (RationalFraction fraction in storage)
                {
                    if (fraction > tmp)
                    {
                        tmp = fraction;
                    }
                }
                cache.Put(query, result, version);  //put answer in cache
            }
            return tmp;
        }

        public RationalFraction GetMin()
        {
            if (storage.Count == 0) // if collection is empty return null
            {
                return null;
            }
            
            Query query = new Query(QueryType.GET_MIN); //form query to cache
            List<RationalFraction> result = cache.Get(query, version); // trying to get from cache
            RationalFraction tmp = storage[0];
            
            if (result == null) // if cache returned no result then find answer
            {
                result = new List<RationalFraction>();
                foreach (RationalFraction fraction in storage)
                {
                    if (fraction < tmp)
                    {
                        tmp = fraction;
                    }
                }
                cache.Put(query, result, version); //put answer in cache
            }
            return tmp;
        }

        public List<RationalFraction> GetLessThan(RationalFraction src)
        {
            if (storage.Count == 0) // if collection is empty return null
            {
                return null;
            }
            
            Query query = new Query(QueryType.GET_LESS_THAN, 
                src.GetNumerator(), src.GetDenumerator()); //form query to cache
            List<RationalFraction> result = cache.Get(query, version); // trying to get from cache
            
            if (result == null) // if cache returned no result then find answer
            {
                result = new List<RationalFraction>();
                foreach (RationalFraction fraction in storage)
                {
                    if (fraction < src)
                    {
                        result.Add(fraction);
                    }
                }
                cache.Put(query, result, version); //put answer in cache
            }
            return result;
        }

        public List<RationalFraction> GetMoreThan(RationalFraction src)
        {
            if (storage.Count == 0) // if collection is empty return null
            {
                return null;
            }
            
            Query query = new Query(QueryType.GET_MORE_THAN, 
                src.GetNumerator(), src.GetDenumerator()); //form query to cache
            List<RationalFraction> result = cache.Get(query, version); // trying to get from cache
            
            if (result == null) // if cache returned no result then find answer
            {
                result = new List<RationalFraction>();
                foreach (RationalFraction fraction in storage)
                {
                    if (fraction > src)
                    {
                        result.Add(fraction);
                    }
                }
                cache.Put(query, result, version); //put answer in cache
            }
            return result;
        }
        
        public void Remove(int pos)
        {
            if (storage.Count <= pos)
            {
                throw new IndexOutOfRangeException();
            }
            Console.Out.WriteLine($"{storage[pos]} has been removed from collection ", "id");
            storage.Remove(storage[pos]);
            version++;
        }
        
        public void Add(RationalFraction src)
        {
            storage.Add(new RationalFraction(src));
            version++;
        }
        
        public CachedFractionList()
        {
            cache = new Cache();
            storage = new List<RationalFraction>();
        }
        
        public void PrintAll()
        {
            foreach (var fraction in storage)
            {
                Console.Out.Write($"{fraction} ");
            }

            Console.Out.WriteLine();
        }

        public List<RationalFraction> GetAll()
        {
            return new List<RationalFraction>(this.storage);
        }
    }
}