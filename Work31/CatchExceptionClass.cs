using System;

namespace Work31
{
    class CatchExceptionClass
    {
        public void CatchExceptionMethod(int[] arr)
        {
            try
            {
                Console.WriteLine("Input Array:");
                for (int i = 0; i < 4; i++)
                {
                    Console.WriteLine($"{arr[i]}");
                }
                MyArray ma = new MyArray();
                ma.Assign(arr, arr.Length);
            }
            // 8) catch all other exceptions here
            catch (Exception e)
            {
                // 9) print System.Exception properties:
                // HelpLink, Message, Source, StackTrace, TargetSite
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Exceptions occurred:");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("HelpLink - " + e.HelpLink);
                Console.WriteLine("Message - " + e.Message);
                Console.WriteLine("Source - " + e.Source);
                Console.WriteLine("StackTrace - " + e.StackTrace);
                Console.WriteLine("TargetSite - " + e.TargetSite);
            }
            // 10) add finally block, print some message
            // explain features of block finally
            finally
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("CatchExceptionMethod finished work.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadLine();
            }
        }
    }
}