using System;

namespace Lab6_Patterns
{
    class CreditAccount : Account 
    {
        public double Limit { get; }

        public CreditAccount(Client client, double money, double limit, double fee)
        {
            if(fee > 100)
                throw new ArgumentException("Percents are not valid");
            StartMoney = money;
            Owner = client;
            Money = money;
            Limit = limit;
            Fee = fee;
        }


        protected override bool CheckMoney(double money)
        {
            return Money + Limit >= money;
        }
    }
}