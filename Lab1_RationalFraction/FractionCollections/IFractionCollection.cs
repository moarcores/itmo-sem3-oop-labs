using System.Collections.Generic;

namespace Lab1_RationalFraction
{
    public interface IFractionCollection
    {
        RationalFraction GetMax();

        RationalFraction GetMin();

        List<RationalFraction> GetLessThan(RationalFraction src);

        List<RationalFraction> GetMoreThan(RationalFraction src);

        void Add(RationalFraction src);

        void Remove(int pos);

        void PrintAll();

        List<RationalFraction> GetAll();
    }
}