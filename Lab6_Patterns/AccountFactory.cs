using System;

namespace Lab6_Patterns
{
    class AccountFactory
    {
        private double percent = 3;
        public double Percents
        {
            get => percent;
            private set
            {
                if(value > 100)
                    throw  new ArgumentException("Percents are not valid");
                percent = value;
            }
        }
        public double Limit = 25000;

        private double fee = 1;

        public double Fee
        {
            get => fee;

            private set
            {
                if (value > 100)
                    throw new ArgumentException("Percents are not valid");
                fee = value;
            }
        }

        public uint Days = 90;

        public Account Create(Client client, double money, string acc)
        {
            switch (acc)
            {
                case "Current":
                    return new CurrentAccount(client, money, Percents);
                case "Dispose":
                    return new DepositAccount(client, money, Percents, Days);
                case "Credit":
                    return new CreditAccount(client, money, Limit, Fee);
                default: throw new FormatException("Incorrect input");
            }

        }

        public AccountFactory SetValues(double pers, double lim, double fee, uint days)
        {
            Percents = pers;
            Limit = lim;
            Fee = fee;
            Days = days;
            return this;
        }
    }
}