namespace Lab6_Patterns
{
    class AccountDec : Account
    {
        protected Account Acc;
        private double _maxSumm;

        public override double Money => Acc.Money;
        public override double Fee => Acc.Fee;
        public override Client Owner => Acc.Owner;
        public override double Percents => Acc.Percents;
        public override double StartMoney => Acc.StartMoney;

        public AccountDec(Account acc, double maxSum)
        {
            
            _maxSumm = maxSum;
            Acc = acc;
        }

        
        public override bool Withdraw(double money)
        {
            if(CheckMoney(money))
                return Acc.Withdraw(money);
            return false;
        }

        public override bool Transfer(double money, Account other)
        {
            if (CheckMoney(money))
                return Acc.Transfer(money, other);
            return false;

        }

        public override void Replenish(double money)
        {
            Acc.Replenish(money);
        }

        protected override bool CheckMoney(double money)
        {
            return Acc.Owner.Passport != null && Acc.Owner.Address != null || money <= _maxSumm;
        }
    }
}