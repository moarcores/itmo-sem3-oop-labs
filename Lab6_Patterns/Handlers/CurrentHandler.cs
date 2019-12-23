namespace Lab6_Patterns
{
    class CurrentHandler : Handler
    {
        public override void PersentsHandle(Account acc, string name)
        {
            if (name == "Current")
            {
                acc.Replenish(acc.Money * (acc.Percents / 100));
            }
            else if (Successor != null)
                Successor.PersentsHandle(acc, name);
        }

        public override void FeeHandle(Account acc, string name)
        {
            if (name == "Current") { }
            else if (Successor != null)
                Successor.PersentsHandle(acc, name);
        }
    }
}