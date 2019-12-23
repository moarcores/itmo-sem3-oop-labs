namespace Lab6_Patterns
{
    class DipositHandler : Handler
    {
        public override void PersentsHandle(Account acc, string name)
        {
            if (name == "Deposit")
            {
                acc.Replenish(acc.StartMoney * (acc.Percents / 100));
            }
            else if (Successor != null)
                Successor.PersentsHandle(acc, name);
        }

        public override void FeeHandle(Account acc, string name)
        {
            if (name == "Deposit") { }
            else if (Successor != null)
                Successor.PersentsHandle(acc, name);
        }
    }
}