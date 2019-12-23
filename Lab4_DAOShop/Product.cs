namespace Lab4_DAOShop
{
    public class Product
    {
        public string Name { get; }

        public Product(string name)
        {
            this.Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Product);
        }

        public bool Equals(Product other)
        {
            return other != null && this.Name == other.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}