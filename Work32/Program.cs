using System;

namespace Work32
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Observation titmouse flight");
            Bird My_Bird = new Bird("Donald Dack", 20);
            bool menu = true;
            try
            {
                while (menu)
                {
                    Console.WriteLine("Select operation (any other digit - out)...");
                    Console.WriteLine("Menu:");
                    Console.WriteLine("1 code array overflow exception");
                    Console.WriteLine("2 code throw (new System.Exception(\"Oh!My system exception...\")");
                    Console.WriteLine("3 code the sequentially incrementing of Bird speed until to the exception");
                    try
                    { 
                        switch (Int32.Parse(Console.ReadLine()))
                        {

                            case 1:
                                {
                                    int i = My_Bird.FlySpeed[4];
                                    break;
                                }
                            case 2:
                                {
                                    throw new System.Exception("Oh! My system exception...");
                                }
                            case 3:
                                {
                                    try
                                    { 
                                        for (int i=1;i<=20;i++)
                                            My_Bird.FlyAway(i);
                                    }
                                    catch (BirdFlewAwayException e)
                                    {
                                        Console.WriteLine(e.When);
                                        Console.WriteLine(e.Why);
                                        Console.WriteLine(e.Message);
                                        Console.WriteLine(e.HelpLink);
                                    }
                                    break;
                                }
                            default:
                                {
                                    menu = false;
                                    break;
                                }
                        }
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadLine();
            }
        }
    }
}
