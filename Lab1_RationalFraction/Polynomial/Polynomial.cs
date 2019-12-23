using System;
using System.Collections.Generic;
using static System.Math;

namespace Lab1_RationalFraction.Polynom
{
    public class Polynomial
    {
        private List<RationalFraction> storage;

        public Polynomial()
        {
            storage = new List<RationalFraction>();
        }

        public Polynomial(IFractionCollection src)
        {
            storage = src.GetAll();
        }

        public void add(Polynomial src)
        {
            int min = Min(src.storage.Count, this.storage.Count);
            for (int i = 0; i < min; ++i)
            {
                storage[i].Add(src.storage[i]);
            }

            if (src.storage.Count > min)
            {
                for (int i = min; i < src.storage.Count; i++)
                {
                    this.storage.Add(src.storage[i]);
                }
            }
        }
        
        
        
        
        public void PrintAll()
        {
            foreach (var fraction in storage)
            {
                Console.Out.Write($"{fraction} ");
            }

            Console.Out.WriteLine();
        }
    }
}