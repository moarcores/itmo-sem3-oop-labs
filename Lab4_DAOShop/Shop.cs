namespace Lab4_DAOShop
{
    public class Shop
    {
        public string Name { get; }

        private readonly string _address;
        
        public Shop(string name, string address = null)
        {
            Name = name;
            _address = address;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}