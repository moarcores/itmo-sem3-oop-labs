using System;
using System.Data.SqlClient;

namespace Lab6_Patterns
{
    public class Client
    {
        public string Name = null;
        public string Surname = null;
        public string Address = null;
        public int? Passport = null;

        public class Builder
        {
            private Client newClient;

            public Builder()
            {
                newClient = new Client();
            }

            public Builder WithName(string name)
            {
                newClient.Name = name;
                return this;
            }

            public Builder WithSurname(string surname)
            {
                newClient.Surname = surname;
                return this;
            }

            public Builder WithAddress(string address)
            {
                newClient.Address = address;
                return this;
            }

            public Builder WithPassport(int passport)
            {
                newClient.Passport = passport;
                return this;
            }

            public Client Build()
            { 
                if (newClient.Name == null || newClient.Surname == null)
                {
                    throw new ArgumentException("Name and Surname are necessary fields");
                }
                return newClient;
            }
        }
        
        
    }
}