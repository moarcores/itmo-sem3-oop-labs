namespace Lab6_Patterns
{
    class CreditHandler : Handler
    {
        public override void PersentsHandle(Account acc, string name)
        {
            if (name == "Credit") { }
            else if (Successor != null)
                Successor.PersentsHandle(acc, name);
        }

        public override void FeeHandle(Account acc, string name)
        {
            if (name == "Credit")
            {
                if(acc.Money > 0)
                    acc.Withdraw(acc.Money * (acc.Fee / 100));
                else
                {
                    acc.Replenish(acc.Money * (acc.Fee / 100));
                }
            }
            else if (Successor != null)
                Successor.FeeHandle(acc, name);
        }
    }
}