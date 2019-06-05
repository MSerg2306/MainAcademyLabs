using System;
using static Work43.Animal;

namespace Work43
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Print animals with foreach operator for object of Animals");
                Animals animals = new Animals();
                foreach (Animal a in animals)
                    Console.WriteLine(a);
                
                Animal a1 = new Animal("elephant", 5000);
                Animal a2 = new Animal("lion", 800);
                Animal a3 = new Animal("buffalo", 1200);
                Animal a4 = new Animal("antelope", 75);
                Animal[] animal = new Animal[] { a1, a2, a3, a4 };

                Console.WriteLine("\nPrint results with foreach operator for array of Animal objects (CompareTo)");
                Array.Sort(animal);
                foreach (var a in animal)
                {
                    Console.WriteLine(a);
                }

                Console.WriteLine("\nPrint results with foreach operator for array of Animal objects (SortWeightAscendin)");
                SortWeightAscending(animal);
                foreach (var a in animal)
                {
                    Console.WriteLine(a);
                }

                Console.WriteLine("\nPrint results with foreach operator for array of Animal objects (SortGenusDescending)");
                SortGenusDescending(animal);
                foreach (var a in animal)
                {
                    Console.WriteLine(a);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
