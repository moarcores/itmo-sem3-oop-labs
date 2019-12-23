using System.Collections.Generic;

namespace Lab1_RationalFraction
{
    public interface ICache
    { 
        List<RationalFraction> Get(Query query, ulong version);
        void Put(Query query, List<RationalFraction> result, ulong version);
    }
}