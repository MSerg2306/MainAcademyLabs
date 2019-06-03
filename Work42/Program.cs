using System;
using System.Collections.Generic;

namespace Work42
{
    class Program
    {
        static void Main(string[] args)
        {
            int a;
            try
            {
                do
                {
                    Console.WriteLine(@"Please,  type the number:

                        Generics      Class Derived : Base<Derived>
                        1.  Static base constructor
                        2.  Protected base constructor (StackOverflow !)
                        3.  Static base constructor, public field
                        4.  Protected base constructor, static field

                        Generics      Delegats & List
                        5.  Generic delegates, extension methods, List  
                
                        ");
                    try
                    {                        
                        a = int.Parse(Console.ReadLine());
                        switch (a)
                        {
                            case 1:                             
                                Console.WriteLine("Step 1 ...");
                                Swap<Derived>();
                                Console.WriteLine("");
                                break;
                            case 2:
                                Console.WriteLine("Step 2 ...");
                                Swap<Derived_publ>();
                                Console.WriteLine("");
                                break;
                            case 3:
                                Console.WriteLine("Step 3 ...");
                                Base_public_field<Derived> base_Public_Field = new Base_public_field<Derived>();
                                Console.WriteLine($"Base_public_field created an instance of the class - {base_Public_Field.Instance}");
                                Console.WriteLine("");
                                break;
                            case 4:
                                Console.WriteLine("Step 4 ...");
                                Swap<Derived_public_field>();
                                Console.WriteLine("");
                                break;
                            case 5:
                                Console.WriteLine("Create currying ...");
                                Step2Func();
                                Console.WriteLine("");
                                break;
                           
                            default:
                                Console.WriteLine("Exit");
                                break;
                        }
                    }
                    catch (System.Exception e)
                    {
                        Console.WriteLine("Error");
                    }
                    finally
                    {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Press Spacebar to exit; press any key to continue");
                    Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                while (Console.ReadKey().Key != ConsoleKey.Spacebar);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void Swap<T>() where T : new()
        {
            T pizzle = new T();
            Console.WriteLine("");
        }

        static void Step2Func()
        {
            var source = new List<double> { 1.0, 2.4, 34.9, 9.02, 7.0 };
            var result = new List<double>();
            Func<double, double, double> f = (x, у) => x - у;

            var fBnd = f.Bnd()(2.0);

            Console.WriteLine("Source list");
            foreach (var item in source)
            {
                Console.Write("{0} ; ", item);
                result.Add(fBnd(item));
            }
            Console.WriteLine();
            Console.WriteLine("Result list");
            foreach (var item in result)
            {
                Console.Write("{0} ; ", item);
            }
        }
    }
}

