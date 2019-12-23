namespace Lab6_Patterns
{
    abstract class Handler
    {
        public Handler Successor;

        public abstract void PersentsHandle(Account acc, string name);

        public abstract void FeeHandle(Account acc, string name);
    }
}