using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work32
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Observation titmouse flight");
            Bird My_Bird = new Bird("Donald Dack", 20);
            bool menu = true;
            //1. Create the skeleton code with the  basic exception handling for the menu in the main method 
            //try - catch
            try
            {
                // 1. begin
                while (menu)
                {
                    //2. Create code for the nested special exception handling in the main method
                    Console.WriteLine("Select operation (any other digit - out)...");
                    Console.WriteLine("Menu:");
                    Console.WriteLine("1 code array overflow exception");
                    Console.WriteLine("2 code throw (new System.Exception(\"Oh!My system exception...\")");
                    Console.WriteLine("3 code the sequentially incrementing of Bird speed until to the exception");
                    //try - catch - catch - finally
                    // 2. begin
                    try
                    { 
                    //3. Create the menu for three options in the inner try block  
                    //In the second option throw the System.Exception
                    // 3. begin
                        switch (Int32.Parse(Console.ReadLine()))
                        {

                            //4. in case 1 code array overflow exception 
                            case 1:
                                {
                                    int i = My_Bird.FlySpeed[4];
                                    break;
                                }
                            //in case 2 code throw (new System.Exception("Oh! My system exception..."));
                            case 2:
                                {
                                    throw new System.Exception("Oh! My system exception...");
                                }
                            //in case 3  code the sequentially incrementing of Bird speed until to the exception
                            case 3:
                                {
                                    // 3. end
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
                     // 2. end
                    catch (IndexOutOfRangeException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            // 1. end
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadLine();
            }
        }
    }
}
