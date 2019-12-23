using System;

namespace Lab6_Patterns
{
    class DepositAccount : Account
    {
        public double Percents { get; }
        public uint Days { get; }

        public DepositAccount(Client client, double money, double percents, uint days)
        {
            if (percents > 100)
                throw new ArgumentException("Percents are not valid");
            Owner = client;
            Money = money;
            StartMoney = money;
            Percents = percents;
            Days = days;
        }


        protected override bool CheckMoney(double money)
        {
            return Days == 0 && Money >= money;
        }
    }
}