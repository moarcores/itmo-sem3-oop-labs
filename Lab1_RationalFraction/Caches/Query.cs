namespace Lab1_RationalFraction
{
    public class Query
    {
        public QueryType type;
        private long numerator;
        private ulong denumerator;

        public Query(QueryType srcType, long srcNumerator = 0, ulong srcDenumerator = 1)
        {
            type = srcType;
            numerator = srcNumerator;
            denumerator = srcDenumerator;
        }

        public bool Equals(Query obj)
        {
            if (this == obj)
                return true;
            return (this.type == obj.type 
                    && this.numerator == obj.numerator 
                    && this.denumerator == obj.denumerator);
        }

        public override int GetHashCode()
        {
            return (int)type + (int)numerator + (int)denumerator;
        }
    }

    public enum QueryType
    {
        GET_MAX = 0,
        GET_MIN = 1,
        GET_LESS_THAN = 2,
        GET_MORE_THAN = 3
    }

}