using System;
using System.IO;
using Lab1_RationalFraction.Polynom;

namespace Lab1_RationalFraction
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            /*IFractionCollection myList = new CachedFractionList();
            
            myList.Add(new RationalFraction(1, 2));
            myList.Add(new RationalFraction(1, 3));
            myList.Add(new RationalFraction(1, 4));
            myList.Add(new RationalFraction(2, 2));
            myList.Add(new RationalFraction(-1, 2));

            Console.Out.WriteLine(myList.GetMax());
            Console.Out.WriteLine(myList.GetMin());
            foreach (var variable in myList.GetLessThan(new RationalFraction(1, 2)))
            {
                Console.Write($"{variable} ");
            }
            Console.Out.WriteLine();
            
            foreach (var variable in myList.GetMoreThan(new RationalFraction(1, 3)))
            {
                Console.Write($"{variable} ");
            }
            Console.Out.WriteLine();
            
            myList.PrintAll();
            Console.Out.WriteLine();
            
            myList.Remove(3);
            
            myList.PrintAll();
            Console.Out.WriteLine();
            
            Console.Out.WriteLine(myList.GetMax());
            Console.Out.WriteLine(myList.GetMin());
            foreach (var VARIABLE in myList.GetLessThan(new RationalFraction(1, 2)))
            {
                Console.Write($"{VARIABLE} ");
            }
            Console.Out.WriteLine();
            
            foreach (var VARIABLE in myList.GetMoreThan(new RationalFraction(1, 3)))
            {
                Console.Write($"{VARIABLE} ");
            }
            Console.Out.WriteLine();*/

            
            
            using (FractionsScanner scanner = new FractionsScanner("/Users/sergei/RiderProjects/sem3_OOPLabs/Lab1_RationalFraction/input"))
            {
                IFractionCollection collection = null;
                try
                {
                     collection = scanner.GetNextCollection();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                if (collection != null)
                {
                    Console.WriteLine(collection.GetMax());
                    Console.WriteLine(collection.GetMin());
                    collection.PrintAll();
                    Polynomial polynomial = new Polynomial(collection);
                    polynomial.PrintAll();

                    IFractionCollection collection1 = scanner.GetNextCollection();
                    Polynomial polynomial1 = new Polynomial(collection1);
                    polynomial1.PrintAll();
                    
                    polynomial1.add(polynomial);
                    polynomial1.PrintAll();
                }
            }
        }
    }
}
     