using System;

namespace Lab1_RationalFraction
{
    public class RationalFraction
    {
        private long numerator;
        private ulong denumerator;

        public RationalFraction(long num, ulong denum)
        {
            if (denum == 0)
            {
                throw new ArgumentException("zero division");
            }
            numerator = num;
            denumerator = denum;
            Refresh();
        }
        
        public RationalFraction(RationalFraction src)
        {
            numerator = src.numerator;
            denumerator = src.denumerator;
        }

        private static ulong FindGreatestCommonDivisor(ulong a, ulong b)
        {
            while (b != 0)
            {
                ulong temp = b;
                b = a % b;
                a = temp;
            }

            return a;
        }

        private static ulong FindLeastCommonMultiple(ulong a, ulong b)
        {
            return (a * b) / FindGreatestCommonDivisor(a, b);
        }
        
        private void Refresh()
        {
            ulong temp = FindGreatestCommonDivisor((ulong)Math.Abs(numerator), denumerator);
            if (temp != 0)
            {
                numerator = numerator / (long)temp;
                denumerator = denumerator / temp;
            }
        }

        public override string ToString()
        {
            return $"{numerator}/{denumerator}";
        }

//        public void SetNumerator(int src)
//        {
//            numerator = src;
//            Refresh();
//        }

        public long GetNumerator()
        {
            return this.numerator;
        }
       
//        public void SetDenumerator(uint src)
//        {
//            if (denumerator == 0)
//            {
//                throw new Exception("zero division");
//            }
//            denumerator = src;
//            Refresh();
//        }

        public ulong GetDenumerator()
        {
            return this.denumerator;
        }

        public void Add(RationalFraction rhs)
        {
            ulong temp = FindLeastCommonMultiple(denumerator, rhs.denumerator);
            numerator = numerator * (long)(temp / denumerator) + rhs.numerator * (long)(temp / rhs.denumerator);
            denumerator = temp;
            Refresh();
        }

        public static RationalFraction operator +(RationalFraction lhs, RationalFraction rhs)
        {
            RationalFraction result = new RationalFraction(lhs);
            result.Add(rhs);
            return result;
        }

        public static bool operator ==(RationalFraction lhs, RationalFraction rhs)
        {
            return (lhs.numerator == rhs.numerator && lhs.denumerator == rhs.denumerator);
        }

        public static bool operator !=(RationalFraction lhs, RationalFraction rhs)
        {
            return (lhs.numerator != rhs.numerator || lhs.denumerator != rhs.denumerator);
        }

        public static bool operator <(RationalFraction lhs, RationalFraction rhs)
        {
            ulong tmp = FindLeastCommonMultiple(lhs.denumerator, rhs.denumerator);
            long lhsNumerator = lhs.numerator * (long)(tmp / lhs.denumerator);
            long rhsNumerator = rhs.numerator * (long)(tmp / rhs.denumerator);
            return (lhsNumerator < rhsNumerator);
        }
        
        public static bool operator >(RationalFraction lhs, RationalFraction rhs)
        {
            ulong tmp = FindLeastCommonMultiple(lhs.denumerator, rhs.denumerator);
            long lhsNumerator = lhs.numerator * (long)(tmp / lhs.denumerator);
            long rhsNumerator = rhs.numerator * (long)(tmp / rhs.denumerator);
            return (lhsNumerator > rhsNumerator);
        }
    }
}