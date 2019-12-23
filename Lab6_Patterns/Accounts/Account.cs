namespace Lab6_Patterns
{
    public abstract class Account
    {
        public  virtual double Money { get; protected set; }

        public virtual double Fee { get; protected set; } = 0;

        public virtual double Percents { get; protected set; } = 2;

        public virtual Client Owner { get; protected set; }

        public virtual double StartMoney { get; protected set; }

        public virtual bool Withdraw(double money)
        {
            if (CheckMoney(money))
            {
                double k = 1;
                if (Money < 0)  
                    k += Fee / 100;
                this.Money -= money * k;
                return true;
            }

            return false;
        }

        public virtual void Replenish(double money)
        {
            double k = 1;
            if (Money < 0)
                k += Fee / 100;
            this.Money += money / k;
        }

        public virtual bool Transfer(double money, Account other)
        {
            if (Owner == other.Owner && CheckMoney(money))
            {
                double k = 1;
                if (Money < 0)
                    k += Fee / 100;
                money /= k;
                this.Money -= money;
                other.Money += money;
            }

            return false;
        }

        protected abstract bool CheckMoney(double money);
    }
}