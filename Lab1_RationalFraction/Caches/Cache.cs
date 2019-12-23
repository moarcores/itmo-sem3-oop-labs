using System;
using System.Collections.Generic;

namespace Lab1_RationalFraction
{
    public class Cache : ICache
    {
        private Dictionary<Query, Tuple<List<RationalFraction>, ulong>> cache;

        public Cache()
        {
            cache = new Dictionary<Query, Tuple<List<RationalFraction>, ulong>>();
        }

        public List<RationalFraction> Get(Query query, ulong version)
        {
            if (cache.ContainsKey(query) && cache[query].Item2 == version)
            {
                return new List<RationalFraction>(cache[query].Item1);
            }
            return null;
        }

        public void Put(Query query, List<RationalFraction> result, ulong version)
        {
            cache[query] = new Tuple<List<RationalFraction>, ulong>(new List<RationalFraction>(result), version); 
        }
    }
}