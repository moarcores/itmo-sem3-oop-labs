namespace Lab6_Patterns
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var client = new Client.Builder()
                .WithName("Name")
                .WithSurname("Surname")
                .WithPassport(123455)
                .WithAddress("Some Address")
                .Build();
        }
    }
}