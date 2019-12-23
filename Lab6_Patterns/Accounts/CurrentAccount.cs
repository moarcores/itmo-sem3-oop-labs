using System;

namespace Lab6_Patterns
{
    public class CurrentAccount : Account
    {
        public CurrentAccount(Client client, double money, double percents)
        {
            if (percents > 100)
                throw  new ArgumentException("Percents are not valid");
            StartMoney = money;
            Owner = client;
            Money = money;
            Percents = percents;
        }


        protected override bool CheckMoney(double money)
        {
            return Money >= money;
        }
    }
}